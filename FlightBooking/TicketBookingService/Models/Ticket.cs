using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string BookingID { set; get; }
        public string TicketID { set; get; }        
        public string FlightNumber { set; get; }
        public DateTime DateOfJourney { set; get; }
        public string FromPlace { set; get; }
        public string ToPlace { set; get; }
        public DateTime BoardingTime { set; get; }
       // public string EmailID { set; get; }
        public string PassengerName { set; get; }
        public string PassportNumber { set; get; }
        public int Age { set; get; }
        public string SeatNumber { set; get; }
        public int Status { set; get; }
        public string StatusStr { set; get; }
        public int SeatType { get; set; }
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
    }
}
