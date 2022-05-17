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

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirLineController : ControllerBase
    {
        public readonly IAirLineInterface _iAirLineInterface;
        public AirLineController(IAirLineInterface iAirLineInterface)
        {
            _iAirLineInterface = iAirLineInterface;
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
                    return _iAirLineInterface.ShowAllAirLines().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
