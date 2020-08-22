using ShopBridge.Infrastructure.Utils.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Modules.Inventory.Logic.IProcessors
{
    public interface IHotelProcessor
    {
        Task<Response<bool>>  AddHotel(Model.Add.Hotel hotel);
        Task<Response<List<Model.View.Hotel>>>  GetAllHotel();
        Response<Model.View.Hotel> GetHotelByMhid(int Mhid);
        Response<bool> UpdateHotel(int Mhid, Model.Add.Hotel item);
        Task<Response<bool>> DeleteHotel(int Mhid);
    }
}
