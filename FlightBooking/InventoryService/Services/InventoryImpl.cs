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

        public void PlanInventory(Inventorys inventory)
        {
            _inventoryDbContext.tblInventories.Add(inventory);
            save();
        }

        public List<Inventorys> ShowInventories()
        {
            return _inventoryDbContext.tblInventories.ToList();
        }
        public void save()
        {
            _inventoryDbContext.SaveChanges();
        }
    }
}
