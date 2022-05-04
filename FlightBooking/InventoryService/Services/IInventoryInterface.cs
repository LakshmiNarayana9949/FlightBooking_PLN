using InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Services
{
    public interface IInventoryInterface
    {
        void PlanInventory(Inventorys inventory);
        List<Inventorys> ShowInventories();
        void CancelInventory(int id);
    }
}
