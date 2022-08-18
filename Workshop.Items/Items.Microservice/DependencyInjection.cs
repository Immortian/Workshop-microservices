using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Shared.Models;

namespace Items.Microservice
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<workshopitemsdbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddAutoMapper(options =>
            {
                options.CreateMap(typeof(Item), typeof(ItemModel));
                options.CreateMap(typeof(ItemCollection), typeof(ItemCollectionModel));
            });

            return services;
        }
    }
}
