using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ShopBridge.Infrastructure.Utils.Configuration.Context;
using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using ShopBridge.Modules.Inventory.Api.Controllers;
using ShopBridge.Modules.Inventory.DataAccess.IRepository;
using ShopBridge.Modules.Inventory.DataAccess.Repository;
using ShopBridge.Modules.Inventory.Logic.IProcessors;
using ShopBridge.Modules.Inventory.Logic.Processors;
using System;
using System.IO;
using System.Reflection;

namespace ShopBridge.UnitTestCase
{
    [TestClass]
    public class UnitTest
    {
        IDBContext _dbcontext = new DBContext()
        { DataBaseConnectionString = "server=DESKTOP-VL1RCGT\\SQLSERVER;database=ShopBridge;uid=sa;password=123456;" };
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestHotelprocessorGetHotelByMhidAsync()
        {
            //replace with  inserted hotel name
            string name = "Hotel ALBORADA OCEAN CLUB";
            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var response = _processor.GetHotelByMhid(1);
            if (response?.Model != null)
            {
                Assert.AreEqual(response.Model.Name.ToLower(), name.ToLower());
            }


        }
        //Test Hotellist total records count is corect or not
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestHotelprocessorGetHotelListAsync()
        {           

            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var actualdata = _repository.GetAllHotel();


            var response = await _processor.GetAllHotel();
            Assert.AreEqual(response.Model.Count, 7);


        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestAddHotelDataProcessorAsync()
        {
           
            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var actualdata = _repository.GetAllHotel();

            var response = await _processor.AddHotel(new
                Modules.Inventory.Model.Add.Hotel
            {
                Name = "test",
                Description = "test",
                Price = 5000,
                HotelImage = "test.jpg"

            });

            Assert.AreEqual(response.Messages, true);
        }
        //Test Already Existing Hotel name 
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestAlreadyExistingHotelProcessorAsync()
        {
          
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
        //Test Update Hotel Data 
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestUpdateHotelDataProcessorAsync()
        {
           
            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var UpdateData = new Modules.Inventory.Model.Add.Hotel
            {
                Name = "test",
                Description = "test",
                Price = 5000,
                HotelImage = "test.jpg"

            };
            var response = _processor.UpdateHotel(1003, UpdateData);

            Assert.AreEqual(response.Messages, true);

        }
        //Test Update Hotel Data Failed case
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestUpdateHotelDataFailedProcessorAsync()
        {
            
            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            var UpdateData = new Modules.Inventory.Model.Add.Hotel
            {
                Name = "test",
                Description = "test",
                Price = 5000
               

            };
            int Mhid = 1003;
            var response = _processor.UpdateHotel(Mhid, UpdateData);

            Assert.AreEqual(response.Messages, "Failed to Update the data");

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task TestDeleteHotelDataProcessorAsync()
        {

            IHotelRepository _repository = new HotelRepository(_dbcontext);
            IHotelProcessor _processor = new HotelProcessor(_repository);
            //change Mhid you want to delete
            int Mhid = 1003;
            var response = _processor.DeleteHotel(Mhid);

            Assert.AreEqual(response, true);

        }


       
    }
}

