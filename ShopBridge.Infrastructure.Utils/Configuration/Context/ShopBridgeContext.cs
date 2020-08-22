using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Configuration.Context
{
    public class ShopBridgeContext
    {
        public ShopBridgeContext()
        {

        }
        public string DataBaseConnectionString { get; set; }
        public IDBContext DbContext
        {

            get
            {
                return new DBContext() { DataBaseConnectionString = DataBaseConnectionString };
            }
        }
    }
}
