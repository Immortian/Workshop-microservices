using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfirmTransactions.Microservice.Commands.ConfirmTransaction
{
    public class ConfirmationCommand
    {
        public int TransactionId { get; set; }
        public bool IsOk { get; set; }
    }
}
