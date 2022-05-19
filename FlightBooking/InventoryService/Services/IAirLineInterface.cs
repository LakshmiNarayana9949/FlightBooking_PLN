using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IAirLineInterface
    {
        void AddNewAirLine(AirLine airLine);
        List<AirLine> ShowAllAirLines();
        void EditAirLine(AirLine airLine);
    }
}
