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
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Updatedby { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
