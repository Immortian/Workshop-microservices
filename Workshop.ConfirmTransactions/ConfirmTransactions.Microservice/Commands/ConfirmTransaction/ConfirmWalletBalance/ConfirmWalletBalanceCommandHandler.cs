using MediatR;

namespace ConfirmTransactions.Microservice.Commands.ConfirmTransaction.ConfirmWalletBalance
{
    public class ConfirmWalletBalanceCommandHandler : IRequestHandler<ConfirmWalletBalanceCommand>
    {
        RabbitSender _rabbitSender;
        public ConfirmWalletBalanceCommandHandler(RabbitSender rabbitSender)
        {
            _rabbitSender = rabbitSender;
        }
        public async Task<Unit> Handle(ConfirmWalletBalanceCommand request, CancellationToken cancellationToken)
        {
            _rabbitSender.Send(request, "Users.Transaction.ConfirmWalletBalance", "ConfirmWalletBalance");
            return Unit.Value;
        }
    }
}
