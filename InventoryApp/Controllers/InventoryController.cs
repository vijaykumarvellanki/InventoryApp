using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleDapper.Contracts;
using SampleDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IInventoryRepository _inventoryRepo;
        public InventoryController(IInventoryRepository inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        [HttpGet("getallinventoryitems")]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var inventories = await _inventoryRepo.GetInventory();
                    return Ok(inventories);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("addinventory")]
        public async Task<IActionResult> AddInventory(Inventory inventory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = await _inventoryRepo.AddNewItem(inventory);
                    return Ok(id);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateinventory/{id}")]
        public async Task<IActionResult> UpdateInventory(int id, Inventory inventory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _inventoryRepo.UpdateInventory(id, inventory);
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteinventory/{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _inventoryRepo.DeleteInventoryById(id);
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    } 
}
