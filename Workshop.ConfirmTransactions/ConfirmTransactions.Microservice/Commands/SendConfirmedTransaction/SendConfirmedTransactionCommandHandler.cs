using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfirmTransactions.Microservice.Commands.SendConfirmedTransaction
{
    public class SendConfirmedTransactionCommandHandler : IRequestHandler<SendConfirmedTransactionCommand>
    {
        RabbitSender _rabbitSender;

        public SendConfirmedTransactionCommandHandler(RabbitSender rabbitSender)
        {
            _rabbitSender = rabbitSender;
        }

        public async Task<Unit> Handle(SendConfirmedTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.AreChecksPassed == false)
                return Unit.Value;

            _rabbitSender.Send(request, "ToDo.Transaction", "TransactionResult");
            return Unit.Value;
        }
    }
}
