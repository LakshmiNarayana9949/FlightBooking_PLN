using InventoryService.Models;
using InventoryService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public readonly IInventoryInterface _inventory;
        public InventoryController(IInventoryInterface inventory)
        {
            _inventory = inventory;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllInventories")]
        public IActionResult GetAllInventories()
        {
            return Ok(_inventory.ShowInventories());
        }

        [Authorize]
        [HttpPost]
        [Route("GetAllInventoriesWithSearch")]
        public IActionResult GetAllInventoriesWithSearch(DateTime fromDate, DateTime toDate, string fromPlace, string toPlace)
        {
            return Ok(_inventory.ShowInventories().Where(a => a.StartDate >= fromDate && a.EndDate <= toDate &&
                                                                a.FromPlace.ToLower().Contains(fromPlace.ToLower()) &&
                                                                a.ToPlace.ToLower().Contains(toPlace.ToLower())));
        }

        [Authorize]
        [HttpPost]
        [Route("AddNewInventory")]
        public void AddNewInventory(Inventory inventory)
        {
            _inventory.PlanInventory(inventory);
        }
    }
}
