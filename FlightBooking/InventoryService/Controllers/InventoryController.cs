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
using InventoryService.DBContext;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryInterface _inventory;
        private readonly InventoryDbContext _inventoryDbContext;

        public InventoryController(IInventoryInterface inventory, InventoryDbContext inventoryDbContext)
        {
            _inventory = inventory;
            _inventoryDbContext = inventoryDbContext;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllInventories")]
        public IActionResult GetAllInventories()
        {
            var inventories = (from i in _inventoryDbContext.Inventories
                             join a in _inventoryDbContext.AirLines
                             on i.AirLineId equals a.AirlineId
                             select new {inventoryId = i.InventoryId,
                                        flightNumber = i.FlightNumber,
                                        airLineId =  a.AirlineId,
                                        airLineName = a.Name,
                                        fromPlace = i.FromPlace,
                                        toPlace = i.ToPlace,
                                        startDate = i.StartDate,
                                        endDate = i.EndDate,
                                        scheduledDays = i.ScheduledDays,
                                        instrument = i.Instrument,
                                        bClassCount = i.BClassCount,
                                        nBClassCount = i.NBClassCount,
                                        ticketCost = i.TicketCost,
                                        rows = i.Rows,
                                        mealType = i.MealType}).ToList();
            return Ok(inventories);
        }

        [Authorize]
        [HttpGet]
        [Route("GetInventory/{inventoryId}")]
        public IActionResult GetInventory(int inventoryId)
        {
            var inventories = (from i in _inventoryDbContext.Inventories
                               join a in _inventoryDbContext.AirLines
                               on i.AirLineId equals a.AirlineId
                               where i.InventoryId == inventoryId
                               select new
                               {
                                   inventoryId = i.InventoryId,
                                   flightNumber = i.FlightNumber,
                                   airLineId = a.AirlineId,
                                   airLineName = a.Name,
                                   fromPlace = i.FromPlace,
                                   toPlace = i.ToPlace,
                                   startDate = i.StartDate,
                                   endDate = i.EndDate,
                                   scheduledDays = i.ScheduledDays,
                                   instrument = i.Instrument,
                                   bClassCount = i.BClassCount,
                                   nBClassCount = i.NBClassCount,
                                   ticketCost = i.TicketCost,
                                   rows = i.Rows,
                                   mealType = i.MealType
                               }).ToList();
            if (inventories.Count() > 0)
                return Ok(inventories[0]);
            else
                return null;
        }

        //[Authorize]
        [HttpPost]
        [Route("GetAllInventoriesWithSearch")]
        public IActionResult GetAllInventoriesWithSearch(DateTime fromDate, string fromPlace, string toPlace)
        {
            var inventories = (from i in _inventoryDbContext.Inventories
                               join a in _inventoryDbContext.AirLines
                               on i.AirLineId equals a.AirlineId
                               where i.StartDate <= fromDate && fromDate <= i.EndDate &&
                                     i.FromPlace.ToLower().Contains(fromPlace.ToLower()) &&
                                     i.ToPlace.ToLower().Contains(toPlace.ToLower())
                               select new
                               {
                                   inventoryId = i.InventoryId,
                                   flightNumber = i.FlightNumber,
                                   airLineId = a.AirlineId,
                                   airLineName = a.Name,
                                   fromPlace = i.FromPlace,
                                   toPlace = i.ToPlace,
                                   startDate = i.StartDate,
                                   endDate = i.EndDate,
                                   scheduledDays = i.ScheduledDays,
                                   instrument = i.Instrument,
                                   bClassCount = i.BClassCount,
                                   nBClassCount = i.NBClassCount,
                                   ticketCost = i.TicketCost,
                                   rows = i.Rows,
                                   mealType = i.MealType
                               }).ToList();
            return Ok(inventories);
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
