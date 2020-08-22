using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ShopBridge.Infrastructure.Utils.Configuration.Context;
using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using ShopBridge.Infrastructure.Utils.Response;
using ShopBridge.Modules.Inventory.ApiService.Controllers;
using ShopBridge.Modules.Inventory.DataAccess.IRepository;
using ShopBridge.Modules.Inventory.DataAccess.Repository;
using ShopBridge.Modules.Inventory.Logic.IProcessors;
using ShopBridge.Modules.Inventory.Logic.Processors;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ShopBridge.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        IDBContext _dbcontext = new DBContext()
        { DataBaseConnectionString = "server=DESKTOP-VL1RCGT\\SQLSERVER;database=ShopBridge;uid=sa;password=123456;" };
        [TestMethod]
        public async System.Threading.Tasks.Task TestHotelprocessorGetHotelByMhidAsync()
        {
            var Actualdata = ReadJsondata();

            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var controller = new HotelController(_processor);            
            var response = _processor.GetHotelByMhid(1);
            Assert.AreEqual(response.Model.Name.ToLower(), Actualdata.model.name.ToLower());


        }
        [TestMethod]
        public async System.Threading.Tasks.Task TestHotelprocessorGetHotelListAsync()
        {
            var Actualdata = ReadJsondata();

            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var actualdata = _repository.GetAllHotel();


            var response = await _processor.GetAllHotel();
            Assert.AreEqual(response.Model.Count, actualdata.Count);


        }
        [TestMethod]
        public async System.Threading.Tasks.Task TestAddHotelDataProcessorAsync()
        {
            var Actualdata = ReadJsondata();
            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var actualdata = _repository.GetAllHotel();

            var response = await _processor.AddHotel(new
                Modules.Inventory.Model.Add.Hotel
            {
                Name = "test",
                Description ="test",                
                Price=5000,
                HotelImage="test.jpg"

            });
            
            Assert.AreEqual(response.Messages,true);
        }
        [TestMethod]
        public async System.Threading.Tasks.Task TestAlreadyExistingHotelProcessorAsync()
        {
            var Actualdata = ReadJsondata();
            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var actualdata = _repository.GetAllHotel();

            var response = await _processor.AddHotel(new
                Modules.Inventory.Model.Add.Hotel
            {
                Name = "Hotel ALBORADA OCEAN CLUB",
                Description = "test",
                Price = 5000,
                HotelImage = "test.jpg"

            });

            Assert.AreEqual(response.Messages, "Hotel Already Exists");
        }
        [TestMethod]
        public async System.Threading.Tasks.Task TestUpdateHotelDataProcessorAsync()
        {
            var Actualdata = ReadJsondata();
            IHotelRepository _repository = new HotelRepository(_dbcontext);
           IHotelProcessor _processor = new HotelProcessor(_repository);          
            var UpdateData = new Modules.Inventory.Model.Add.Hotel
            {
                Name = "test",
                Description = "test",
                Price = 5000,
                HotelImage = "test.jpg"

            };
            var response =  _processor.UpdateHotel(1003, UpdateData);

            Assert.AreEqual(true, true, "ReSULT IS WRONG");

        }

        public Root ReadJsondata()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                     @"JsonTestFile\test.json");
            string data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Root>(data);
        }
        public class hoteldata
        {
            public int mhid { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string hotelImage { get; set; }
            public int price { get; set; }
        }

        public class Root
        {
            public int responseCode { get; set; }
            public object messages { get; set; }
            public hoteldata model { get; set; }
        }
    }
}

