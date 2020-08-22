using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Configuration.IContext
{
    public interface IDBContext
    {
        string DataBaseConnectionString { get; set; }
    }
}
