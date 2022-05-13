using TicketBookingService.Models;
using TicketBookingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TicketBookingService.Common;
using TicketBookingService.Events;
using MassTransit.KafkaIntegration;


namespace TicketBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketBookingController : ControllerBase
    {
        const string TICKET_STATUS_BOOKED = "Booked";
        const string TICKET_STATUS_CANCELLED = "Cancelled";
        public readonly ITicketBookingInterface _iTicketBookingInterface;
        private ITopicProducer<TicketBookingEvent> _iTopicProducer;
        public TicketBookingController(ITicketBookingInterface iTicketBookingInterface, ITopicProducer<TicketBookingEvent> iTopicProducer)
        {
            _iTicketBookingInterface = iTicketBookingInterface;
            _iTopicProducer = iTopicProducer;
        }

        [Authorize]
        [HttpPost]
        [Route("BookTicket")]
        public async Task<ActionResult> BookTicket([FromBody] List<Ticket> tickets)
        {
            string bookingId = "";
            int numberOfTickets = tickets.Count();
            string flightNumber = tickets[0].FlightNumber;
            string fromPlace = tickets[0].FromPlace;
            string toPlace = tickets[0].ToPlace;
            int seatType = tickets[0].SeatType;
            try
            {
                bookingId = GenerateBookingID();
                foreach(Ticket ticket in tickets)
                {
                    string TicketId = GenerateticketID();
                    ticket.TicketID = string.Empty;
                    ticket.TicketID = TicketId;
                    ticket.BookingID = string.Empty;
                    ticket.BookingID = bookingId;
                    ticket.Status = (int)CommonEnums.BookingStatus.Booked;
                    ticket.StatusStr = TICKET_STATUS_BOOKED;
                    ticket.CreatedBy = 2; //Need to save this in session once user login.
                    ticket.ModifiedBy = 2; //Need to save this in session once user login.
                    ticket.CreatedOn = DateTime.Now;
                    ticket.ModifiedOn = DateTime.Now;
                    using (var scope = new TransactionScope())
                    {
                        _iTicketBookingInterface.BookNewTicket(ticket);

                        scope.Complete();                     
                    }
                }
                await _iTopicProducer.Produce(new TicketBookingEvent
                {
                    FlightNumber = flightNumber,
                    FromPlace = fromPlace,
                    ToPlace = toPlace,
                    NumberOfTickets = numberOfTickets,
                    SeatType = seatType
                });
                return Ok("Your ticket(s) booked successfully with booking id " + bookingId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("CancelTicket/{TicketID}")]
        public IActionResult CancelTicket(string TicketID)
        {
            try
            {
                IEnumerable<Ticket> tickets = _iTicketBookingInterface.GetAllTickets().ToList().Where(o => o.TicketID == TicketID).Take(1);
                foreach (Ticket ticket in tickets)
                {
                    ticket.Status = (int)CommonEnums.BookingStatus.Cancel;
                    ticket.StatusStr = TICKET_STATUS_CANCELLED;
                    ticket.ModifiedBy = 2; //Need to save this in session once user login.
                    ticket.ModifiedOn = DateTime.Now;
                    using (var scope = new TransactionScope())
                    {
                        _iTicketBookingInterface.UpdateTicket(ticket);
                        scope.Complete();

                    }
                }
                return new OkObjectResult(tickets);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetTicket/{TicketID}")]
        public IActionResult GetTicket(string TicketID)
        {
            try
            {
                IEnumerable<Ticket> tickets = _iTicketBookingInterface.GetAllTickets().ToList()
                                                .Where(o => o.TicketID.ToUpper() == TicketID.ToUpper());
                return new OkObjectResult(tickets);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTickets")]
        public IActionResult GetAllTickets()
        {
            try
            {
                var tickets = _iTicketBookingInterface.GetAllTickets().ToList();
                return new OkObjectResult(tickets);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }        

        private string GenerateticketID()
        {
            int count = _iTicketBookingInterface.GetAllTickets().ToList().Count();
            string strSecretCode = string.Empty;
            string strguid = string.Empty;
            string strYearCode = string.Empty;
            string TicketID = string.Empty;
            try
            {
                System.Guid guid = System.Guid.NewGuid();
                strguid = guid.ToString();
                strSecretCode = strguid.Substring(strguid.LastIndexOf("-") + 1);
                strSecretCode = strSecretCode.ToUpper().Replace('O', 'W').Replace('0', '4');
                strSecretCode = strSecretCode.Substring(0, 6);

                TicketID = "TICK" + strSecretCode.ToUpper() + count;

                return TicketID;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return TicketID;
            }

        }
        private string GenerateBookingID()
        {
            int count = _iTicketBookingInterface.GetAllTickets().ToList().Count();
            string strSecretCode = string.Empty;
            string strguid = string.Empty;
            string strYearCode = string.Empty;
            string TicketID = string.Empty;
            try
            {
                System.Guid guid = System.Guid.NewGuid();
                strguid = guid.ToString();
                strSecretCode = strguid.Substring(strguid.LastIndexOf("-") + 1);
                strSecretCode = strSecretCode.ToUpper().Replace('O', 'W').Replace('0', '4');
                strSecretCode = strSecretCode.Substring(0, 6);

                TicketID = "BOOK" + strSecretCode.ToUpper() + count;

                return TicketID;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return TicketID;
            }

        }
    }
}
