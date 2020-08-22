using Newtonsoft.Json;
using Serilog;
using ShopBridge.Infrastructure.Utils.Configuration.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Configuration
{
   public class ShopBridgeConfiguration
    {
        public static readonly ShopBridgeContext tenant = LoadTenant();
        public ShopBridgeConfiguration()
        {

        }
        public static ShopBridgeContext Tenant
        {
            get
            {

                return tenant;
            }
        }

        private static ShopBridgeContext LoadTenant()
        {
            Log.Information("fetching Database configuration file");
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"ConfigFile\ShopBridge.json");
                string data = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<ShopBridgeContext>(data);
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message,ex.StackTrace, "Failed to fetch information from config file");
                throw ex;
            }
            
        }
    }
}
