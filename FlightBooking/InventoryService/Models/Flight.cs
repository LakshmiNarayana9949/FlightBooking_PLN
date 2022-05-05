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
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
    }
}
