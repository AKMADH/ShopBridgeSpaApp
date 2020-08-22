using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using ShopBridge.Modules.Inventory.Logic.IProcessors;
using ShopBridge.Modules.Inventory.Logic.Processors;
using ShopBridge.Modules.Inventory.Model.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ShopBridge.Modules.Inventory.ApiService.Controllers
{
    /// <summary>
    /// Post method to add hotel data to the inventory
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    
    public class HotelController : ControllerBase
    {
       private readonly IHotelProcessor _processor;
        /// <summary>
        /// constructor for initiliaze purpose
        /// </summary>
        /// 
        public HotelController(IHotelProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Post method to add hotel data to the inventory
        /// </summary>
        /// 
        [HttpPost("AddHotel")]
        public async Task<ActionResult> AddHotel(Model.Add.Hotel item)
        {
            Log.Information("Adding hotel information process started");
            
            var response = await _processor.AddHotel(item);
            if (response.ResponseCode == Infrastructure.Utils.Enums.ResponseCodes.OK)
            {
                Log.Information("Hotel information Added sucessfully");
                return Ok(response);
                
            }
            if (response.ResponseCode == Infrastructure.Utils.Enums.ResponseCodes.AlreadyExist)
            {
                Log.Warning("Hotel name Already exists");
                return Ok(response);

            }
            return BadRequest(response);
        }

        /// <summary>
        /// Get method to fetch all the hotel information
        /// </summary>
    
        [HttpGet]
        public async Task<ActionResult> GethotelList()
        {
            Log.Information("Fetching All hotel information method Started");
            var response = await _processor.GetAllHotel();
            if (response.ResponseCode == Infrastructure.Utils.Enums.ResponseCodes.OK)
            {
                Log.Information("All hotel information  fetched successfully");
                return Ok(response);
            }
            return BadRequest(response.Messages);
        }

        /// <summary>
        /// Get method to fetch hotel information by mhid 
        /// </summary>
    
        [HttpGet("{Mhid}")]
        public async Task<ActionResult> GetHotelByMhid(int Mhid)
        {
            Log.Information("Fetching  hotel information by mhid  method Started");
            var response =  _processor.GetHotelByMhid(Mhid);
            if (response.ResponseCode == Infrastructure.Utils.Enums.ResponseCodes.OK)
            {
                Log.Information("Hotel information by mhid  method fetched successfully");
                 return Ok(response);
            }
            return BadRequest(response.Messages);
        }

        /// <summary>
        /// Put method to update hotel data of a particular hotel based on Mhid to the inventory
        /// </summary>
        
        [HttpPut("updateHotelInfo/{Mhid:int}")]
        public async Task<ActionResult> UpdateHotelDetails(int Mhid,Model.Add.Hotel item)
        {
            Log.Information("Hotel information update processor  Started");
            var response = _processor.UpdateHotel(Mhid, item);
            if (response.ResponseCode == Infrastructure.Utils.Enums.ResponseCodes.OK)
            {
                Log.Information("Hotel information updated successfully");
                return Ok(response);
            }
            return BadRequest(response.Messages);
        }
        /// <summary>
        /// Delete method to Delete hotel data of a particular hotel based on Mhid to the inventory
        /// </summary>
        [HttpDelete("{Mhid}")]
        public async Task<ActionResult> DeleteHotelDetails(int Mhid)
        {
            Log.Information("Hotel information update processor  Started");
            var response = await _processor.DeleteHotel(Mhid);
            if (response.ResponseCode == Infrastructure.Utils.Enums.ResponseCodes.OK)
            {
                Log.Information("Hotel information Deleted successfully");
                 return Ok(response);
            }
            return BadRequest(response.Messages);
        }
    }
}
