using MediatR;

namespace Users.Microservice.Commands.CheckWalletBalance
{
    public class CheckWalletbalanceCommandHandler : IRequestHandler<CheckWalletbalanceCommand>
    {
        RabbitSender _rabbitSender;

        public CheckWalletbalanceCommandHandler(RabbitSender rabbitSender)
        {
            _rabbitSender = rabbitSender;
        }

        public async Task<Unit> Handle(CheckWalletbalanceCommand request, CancellationToken cancellationToken)
        {
            //workshopusersdbContext _context = new workshopusersdbContext();
            //var buyer = _context.Users.Where(x => x.UserId == request.BuyerId).FirstOrDefault();
            //if (buyer.WalletBalance >= request.Price)
                _rabbitSender.Send(
                    new CheckWalletBalanceResponseCommand 
                    {
                        TransactionId = request.TransactionId, 
                        IsOk = true
                    }, "Confirmed.Transaction.WalletBalance");
            //else
            //  _rabbitSender.Send(
            //      new CheckWalletBalanceResponseCommand
            //      {
            //      TransactionId = request.TransactionId,
            //      IsOk = true
            //      }, "Confirmed.Transaction.WalletBalance");
            return Unit.Value;
        }
    }
}
