﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }        
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
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }

    }
}