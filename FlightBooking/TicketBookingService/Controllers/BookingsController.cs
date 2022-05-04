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

namespace TicketBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        public readonly IBookingInterface _userRepository;
        public BookingsController(IBookingInterface userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("BookTicket")]
        public IActionResult BookTicket([FromBody] List<Bookings> bookings)
        {
            try
            {
                string BookingId = GenerateBookingID();
                foreach(Bookings booking in bookings)
                {
                    string TicketId = GenerateticketID();
                    booking.TicketID = string.Empty;
                    booking.TicketID = TicketId;
                    booking.BookingID = string.Empty;
                    booking.BookingID = BookingId;
                    using (var scope = new TransactionScope())
                    {
                        _userRepository.Insert(booking);
                        scope.Complete();
                       
                    }
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        public string GenerateticketID()
        {
            int count = _userRepository.GetBookings().ToList().Count();
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

                TicketID = "TIC" + strSecretCode.ToUpper()+ count;

                return TicketID;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return TicketID;
            }
           
        }
        public string GenerateBookingID()
        {
            int count = _userRepository.GetBookings().ToList().Count();
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

                TicketID = "BOK" + strSecretCode.ToUpper() + count;

                return TicketID;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return TicketID;
            }

        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTickets")]
        public IActionResult GetAllTickets()
        {
            try
            {
                var users = _userRepository.GetBookings().ToList();
                return new OkObjectResult(users);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("CancelTicket/{TicketID}")]
        public IActionResult CancelTicket(string TicketID)
        {
            try
            {
                IEnumerable<Bookings> bookings = _userRepository.GetBookings().ToList().Where(o => o.TicketID == TicketID).Take(1);
                foreach (Bookings booking in bookings)
                {
                    booking.Status = 0;
                    booking.Statusstr = "Canceled";
                    using (var scope = new TransactionScope())
                    {
                        _userRepository.UpdateBooking(booking);
                        scope.Complete();

                    }
                }
                return new OkObjectResult(bookings);
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
                IEnumerable<Bookings> bookings = _userRepository.GetBookings().ToList()
                                                .Where(o => o.TicketID.ToUpper()== TicketID.ToUpper());
                return new OkObjectResult(bookings);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
