using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Items.Microservice
{
    public static class DBInitializer
    {
        public static void Initialize(workshopitemsdbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}