using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Configuration
{
    public class TenantProvider : ITenantProvider
    {
        public IDBContext GetTenant()
        {
            IDBContext dBContext = null;
            dBContext = ShopBridgeConfiguration.Tenant.DbContext;
            return dBContext;
        }
    }
}
