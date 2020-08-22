using Serilog;
using ShopBridge.Infrastructure.Utils.Response;
using ShopBridge.Modules.Inventory.DataAccess.IRepository;
using ShopBridge.Modules.Inventory.Logic.IProcessors;
using ShopBridge.Modules.Inventory.Model.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Modules.Inventory.Logic.Processors
{
    public class HotelProcessor: IHotelProcessor
    {
        private readonly IHotelRepository _repository;
        public HotelProcessor(IHotelRepository repository)
        {
            //_logger = logger;
            _repository = repository;
        }

        public async Task<Response<bool>> AddHotel(Model.Add.Hotel hotel)
        {

            var response =  _repository.AddHotel(hotel);
            if (response > 0)
            {
                return new Response<bool>(true);
            }
            if (response == -1)
            {
                return new Response<bool>(false, Infrastructure.Utils.Enums.ResponseCodes.AlreadyExist, "Hotel Already Exists");
            }
            return new Response<bool>(false, Infrastructure.Utils.Enums.ResponseCodes.BadRequest, "Saving of Hotel information failed.");
        }

        public async Task<Response<bool>> DeleteHotel(int Mhid)
        {
            var response = _repository.DeleteHotel(Mhid);
            if (response)
            {
                return new Response<bool>(true);
            }


            return new Response<bool>(false, Infrastructure.Utils.Enums.ResponseCodes.BadRequest, "Failed to Delete the data .");
    }

    public async Task<Response<List<Model.View.Hotel>>> GetAllHotel()
        {
            var response =  _repository.GetAllHotel();
            if (response != null)
            {
                Log.Information("");
                return new Response<List<Model.View.Hotel>>(response);
            }
            return new Response<List<Model.View.Hotel>>(null, Infrastructure.Utils.Enums.ResponseCodes.BadRequest, "Data fetching failed.");

        }

        public Response<Model.View.Hotel> GetHotelByMhid(int Mhid)
        {
            var response = _repository.GetHotelByMhid(Mhid);
            if (response != null)
            {
                return new Response<Model.View.Hotel>(response);
            }
            return new Response<Model.View.Hotel>(null, Infrastructure.Utils.Enums.ResponseCodes.BadRequest, "Data fetching failed.");

        }

        public Response<bool> UpdateHotel(int Mhid,Model.Add.Hotel item)
        {
            var response = _repository.UpdateHotel(Mhid,item);
            if (response)
            {
                return new Response<bool>(true);
            }
                    
            return new Response<bool>(false, Infrastructure.Utils.Enums.ResponseCodes.BadRequest, "Failed to Update the data .");
        }

    }
}
