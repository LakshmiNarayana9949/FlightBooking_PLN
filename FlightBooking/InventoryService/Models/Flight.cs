using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Flight
    {
        public int FlightID { set; get; }
        public string FlightNumber { get; set; }
        public int AirLineId { get; set; }
        public string FlightName { get; set; }
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Updatedby { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
