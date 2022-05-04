using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Models
{
    public class Bookings
    {
        [Key]
        public string TicketID { set; get; }
        public string BookingID { set; get; }
        public int FlightId { set; get; }
        public DateTime DateOfJourney { set; get; }
        public string FromPlace { set; get; }
        public string ToPlace { set; get; }
        public DateTime BoardingTime { set; get; }
       // public string EmailID { set; get; }
        public string UserName { set; get; }
        public string passportNumber { set; get; }
        public string Age { set; get; }
        public string SeatNumber { set; get; }
        public int Status { set; get; }
        public string Statusstr { set; get; }
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Updatedby { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
