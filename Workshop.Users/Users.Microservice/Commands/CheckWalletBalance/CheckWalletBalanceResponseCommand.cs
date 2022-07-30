using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Microservice.Commands.CheckWalletBalance
{
    public class CheckWalletBalanceResponseCommand
    {
        public int TransactionId { get; set; }
        public bool IsOk { get; set; }
    }
}
