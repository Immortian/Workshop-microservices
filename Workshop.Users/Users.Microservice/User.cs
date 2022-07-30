using System;
using System.Collections.Generic;

namespace Users.Microservice
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public decimal WalletBalance { get; set; }
    }
}
