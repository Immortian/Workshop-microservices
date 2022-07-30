using MediatR;

namespace Users.Microservice.Commands.CheckWalletBalance
{
    public class CheckWalletbalanceCommand : IRequest
    {
        public int TransactionId { get; set; }
        public int BuyerId { get; set; }
        public decimal Price { get; set; }
    }
}
