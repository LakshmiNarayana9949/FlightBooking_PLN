using InventoryService.Models;
using InventoryService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Transactions;

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
        public ActionResult AddNewInventory(Inventory inventory)
        {
            try
            {
                inventory.CreatedBy = 2; //Need to save this in session once user login.            
                inventory.ModifiedBy = 2; //Need to save this in session once user login.
                inventory.CreatedOn = DateTime.Now;
                inventory.ModifiedOn = DateTime.Now;
                using (var scope = new TransactionScope())
                {
                    _inventory.PlanInventory(inventory);
                    scope.Complete();
                    return Ok("Inventory created successfully.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
