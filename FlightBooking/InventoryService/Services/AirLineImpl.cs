using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Models;
using InventoryService.Services;
using InventoryService.DBContext;

namespace InventoryService.Services
{
    public class AirLineImpl : IAirLineInterface
    {
        public readonly InventoryDbContext _inventoryDbContext;
        public AirLineImpl(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }
        public void AddNewAirLine(AirLine airLine)
        {
            _inventoryDbContext.AirLines.Add(airLine);
            save();
        }

        public List<AirLine> ShowAllAirLines()
        {
            return _inventoryDbContext.AirLines.ToList();
        }
        public void save()
        {
            _inventoryDbContext.SaveChanges();
        }
    }
}
