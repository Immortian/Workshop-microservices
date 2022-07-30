using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Users.Microservice
{
    public static class DBInitializer
    {
        public static void Initialize(workshopusersdbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}