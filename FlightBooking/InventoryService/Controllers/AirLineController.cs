using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Services;
using InventoryService.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using InventoryService.Common;
using InventoryService.DBContext;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirLineController : ControllerBase
    {
        public readonly IAirLineInterface _iAirLineInterface;
        private readonly InventoryDbContext _inventoryDbContext;
        public AirLineController(IAirLineInterface iAirLineInterface, InventoryDbContext inventoryDbContext)
        {
            _iAirLineInterface = iAirLineInterface;
            _inventoryDbContext = inventoryDbContext;
        }

        [Authorize]
        [HttpPost]
        [Route("AddNewAirLine")]
        public IActionResult AddNewAirLine(AirLine airLine)
        {
            //airLine.CreatedBy = 2; //Need to save this in session once user login.            
            //airLine.ModifiedBy = 2; //Need to save this in session once user login.
            airLine.CreatedOn = DateTime.Now;
            airLine.ModifiedOn = DateTime.Now;
            airLine.Status = (int)CommonEnums.Status.Active;
            try
            {
                using(var scope = new TransactionScope())
                {
                    _iAirLineInterface.AddNewAirLine(airLine);
                    scope.Complete();
                    return Ok("AirLine " + airLine.Name + " created successfully.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }            
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAirlines")]
        public List<AirLine> GetAllAirlines()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    return _iAirLineInterface.ShowAllAirLines().ToList().Where(a => a.Status == (int)CommonEnums.Status.Active).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAirLine/{airLineId}")]
        public AirLine GetAirLine(int airLineId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                   List<AirLine> airLines = _iAirLineInterface.ShowAllAirLines().ToList().Where(a => a.Status == (int)CommonEnums.Status.Active && a.AirlineId == airLineId).ToList();
                    if(airLines.Count() > 0)
                    {
                        return airLines[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateAirLine")]
        public ActionResult UpdateAirLine(AirLine airLine)
        {
            try
            {
                airLine.Status = (int)CommonEnums.Status.Active;
                airLine.ModifiedOn = DateTime.Now;
                _iAirLineInterface.EditAirLine(airLine);
                return Ok("Inventory updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("RemoveAirLine/{id}")]
        public ActionResult RemoveAirLine(int id)
        {
            try
            {
                List<Inventory> inventories = _inventoryDbContext.Inventories.ToList().Where(a => a.AirLineId == id && a.Status == (int)CommonEnums.Status.Active).ToList();
                if (inventories.Count() > 0)
                {
                    return Ok("Can not delete this airline as a Inventory is scheduled for this AirLine");
                }
                else
                {
                    AirLine airLine = _iAirLineInterface.ShowAllAirLines().Where(a => a.AirlineId == id).ToList()[0];
                    if (airLine != null)
                    {
                        airLine.Status = (int)CommonEnums.Status.Inactive;
                        airLine.ModifiedOn = DateTime.Now;
                        _iAirLineInterface.EditAirLine(airLine);
                    }

                    return Ok("Inventory removed successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
