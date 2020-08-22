using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Modules.Inventory.DataAccess.IRepository
{
   public interface IHotelRepository
    {
        int? AddHotel(Model.Add.Hotel item);
        List<Model.View.Hotel> GetAllHotel();
        Model.View.Hotel GetHotelByMhid(int Mhid);
        bool UpdateHotel(int mhid, Model.Add.Hotel item);
        bool DeleteHotel(int Mhid);
    }
}
