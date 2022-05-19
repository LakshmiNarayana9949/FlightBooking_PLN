using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class AirLine
    {
        public int AirlineId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
        public int Status { get; set; }
    }
}
