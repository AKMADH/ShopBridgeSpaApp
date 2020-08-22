using Dapper;
using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ShopBridge.Modules.Inventory.Model;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ShopBridge.Modules.Inventory.DataAccess.IRepository;
using ShopBridge.Modules.Inventory.Model.Edit;
using Serilog;

namespace ShopBridge.Modules.Inventory.DataAccess.Repository
{
    public class HotelRepository:IHotelRepository
    {
        private readonly IDBContext _dbContext;
        public HotelRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Add hotel information into thew database
        public int? AddHotel(Model.Add.Hotel item)
        {

            using (var connection = new SqlConnection(_dbContext.DataBaseConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                bool IsExist = HotelExistsByName(item.Name);
                if (IsExist)
                {
                    Log.Warning("Hotel Alreday Exists");
                    return -1;
                }
                else
                {
                    parameters.Add("@Name", item.Name.ToUpper());
                    parameters.Add("@Description", item.Description);
                    parameters.Add("@HotelImage", item.HotelImage);
                    parameters.Add("@Price", item.Price);                  
                    parameters.Add("@Mhid", null, DbType.Int32, ParameterDirection.Output);
                    connection.Execute("InsertHotelDetails_V1", parameters, commandType: CommandType.StoredProcedure);
                    int response = parameters.Get<int>("@Mhid");
                    return response > 0 ? response : (int?)null;
                }
            }
        }

        //fetch  the list of hotels available in db
        public List<Model.View.Hotel> GetAllHotel()
        {
            List<Model.View.Hotel> lsthotel = new List<Model.View.Hotel>();

            using (IDbConnection con = new SqlConnection(_dbContext.DataBaseConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lsthotel = con.Query<Model.View.Hotel>("GetAllHotels_V1").ToList();
            }
            return lsthotel;
        }
        //fetch hotel info based on mhid
        public Model.View.Hotel GetHotelByMhid(int Mhid)
        {
            Model.View.Hotel hotel = null;
            using (var con = new SqlConnection(_dbContext.DataBaseConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Mhid", Mhid);
                hotel = con.Query<Model.View.Hotel>("GetHotelByMhid_V1", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return hotel;
        }
        //Update Hotel Information based on Mhid
        public  bool UpdateHotel(int mhid,Model.Add.Hotel item)
        {
            using (var con = new SqlConnection(_dbContext.DataBaseConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                              
                    parameters.Add("@name", item.Name);
                    parameters.Add("@description", item.Description);
                    parameters.Add("@hotelImage", item.HotelImage);
                    parameters.Add("@price", item.Price);
                    parameters.Add("@Mhid", mhid);
                    return con.Execute("UpdateHotelDetails_V1", parameters, commandType: CommandType.StoredProcedure) > 0;
                
            }
        }
        //Delete hotel Info based on mhid
        public bool DeleteHotel(int Mhid)
        {
            using (var con = new SqlConnection(_dbContext.DataBaseConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Mhid", Mhid);
                
                return con.Execute("DeleteHotelByMhid_V1", parameters, commandType: CommandType.StoredProcedure) > 0;
            }
        }
        //check hotel name is already exist in the table or not 
        public bool HotelExistsByName(string name)
        {
            using (var con = new SqlConnection(_dbContext.DataBaseConnectionString))
            {
                bool response = false;
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand("select * from Hotel where Name= @Name", con);
                cmd.Parameters.AddWithValue("@Name", name.ToLower());

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.HasRows == true)
                    {
                        response = true;
                    }
                    else
                    {
                        response = false;
                    }
                }
                return response;
            }
        }

        
    }
}

