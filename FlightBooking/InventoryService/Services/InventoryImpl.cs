using InventoryService.DBContext;
using InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Services
{
    public class InventoryImpl : IInventoryInterface
    {
        public InventoryDbContext _inventoryDbContext;
        public InventoryImpl(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }
        public void CancelInventory(int id)
        {

        }

        public void PlanInventory(Inventory inventory)
        {
            _inventoryDbContext.Inventories.Add(inventory);
            save();
        }

        public List<Inventory> ShowInventories()
        {
            return _inventoryDbContext.Inventories.ToList();
        }
        public void save()
        {
            _inventoryDbContext.SaveChanges();
        }
    }
}
