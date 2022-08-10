using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Transactions.Microservice
{
    public static class DBInitializer
    {
        public static void Initialize(workshoptransactionsdbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}