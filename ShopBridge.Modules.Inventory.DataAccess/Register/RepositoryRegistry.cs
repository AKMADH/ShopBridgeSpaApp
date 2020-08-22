using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ShopBridge.Modules.Inventory.DataAccess.IRepository;

namespace ShopBridge.Modules.Inventory.DataAccess.Repository
{
    public class RepositoryRegistry
    {
        public static void IncludeRepositoryRegistry(IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();

           

        }
    }
}