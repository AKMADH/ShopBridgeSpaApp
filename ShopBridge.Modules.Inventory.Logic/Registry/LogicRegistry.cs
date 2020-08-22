using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Modules.Inventory.DataAccess.Repository;
using ShopBridge.Modules.Inventory.Logic.IProcessors;
using ShopBridge.Modules.Inventory.Logic.Processors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Modules.Inventory.Logic.Registry
{
    public class LogicRegistry
    {
        public static void IncludeLogicRegistry(IServiceCollection services)
        {
            RepositoryRegistry.IncludeRepositoryRegistry(services);
           services.AddScoped<IHotelProcessor, HotelProcessor>();

        }
    }
}
