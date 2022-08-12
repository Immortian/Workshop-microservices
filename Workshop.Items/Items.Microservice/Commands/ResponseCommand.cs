using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Microservice.Commands
{
    public class ResponseCommand
    {
        public int TransactionId { get; set; }
        public bool IsOk { get; set; }
    }
}
