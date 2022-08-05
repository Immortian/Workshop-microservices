using MediatR;

namespace Users.Microservice.Commands.CheckWalletBalance
{
    public class CheckWalletbalanceCommandHandler : IRequestHandler<CheckWalletbalanceCommand>
    {
        RabbitSender _rabbitSender;
        workshopusersdbContext _context;

        public CheckWalletbalanceCommandHandler(RabbitSender rabbitSender, workshopusersdbContext context)
        {
            _rabbitSender = rabbitSender;
            _context = context;
        }

        public async Task<Unit> Handle(CheckWalletbalanceCommand request, CancellationToken cancellationToken)
        {
            //var buyer = _context.Users.Where(x => x.UserId == request.BuyerId).FirstOrDefault();
            var IsOk = false;
            //if (buyer.WalletBalance >= request.Price)
                IsOk = true;
            
            _rabbitSender.Send(
            new CheckWalletBalanceResponseCommand
            {
                TransactionId = request.TransactionId,
                IsOk = IsOk
            }, request.Properties.ReplyTo, request.Properties.CorrelationId);

            return Unit.Value;
        }
    }
}
