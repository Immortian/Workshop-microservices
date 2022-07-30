using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfirmTransactions.Microservice
{
    public class DBInitializer
    {
        public static void Initialize(transactionconfirmationdbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
