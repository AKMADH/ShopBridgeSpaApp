using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Configuration.IContext
{
    public interface ITenantProvider
    {
        IDBContext GetTenant();
    }
}
