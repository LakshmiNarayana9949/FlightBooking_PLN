using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Inventorys
    {
        [Key]
        public int INventoryId { get; set; }
        
        public int FlightNumber { get; set; }
        public int AirLineId { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ScheduledDays { get; set; } 
        public string Instrument { get; set; }
        public int BClassCount { get; set; }
        public int NBClassCount { get; set; }
        public int TicketCost { get; set; }
        public int Rows { get; set; }
        public int Meal { get; set; }
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Updatedby { set; get; }
        public DateTime UpdatedDate { set; get; }

    }
}
