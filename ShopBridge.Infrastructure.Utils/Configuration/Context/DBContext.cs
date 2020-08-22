using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Configuration.Context
{
   public class DBContext : IDBContext
    {
        public string DataBaseConnectionString { get; set; }
    }
}
