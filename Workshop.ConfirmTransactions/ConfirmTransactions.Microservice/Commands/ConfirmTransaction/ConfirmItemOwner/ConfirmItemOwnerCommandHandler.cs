using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfirmTransactions.Microservice.Commands.ConfirmTransaction.ConfirmItemOwner
{
    public class ConfirmItemOwnerCommandHandler : IRequestHandler<ConfirmItemOwnerCommand>
    {
        RabbitSender _rabbitSender;
        public ConfirmItemOwnerCommandHandler(RabbitSender rabbitSender)
        {
            _rabbitSender = rabbitSender;
        }
        public async Task<Unit> Handle(ConfirmItemOwnerCommand request, CancellationToken cancellationToken)
        {
            _rabbitSender.Send(request, "Confirm.Transaction.ItemOwner", "ItemOwnerConfirmation");
            return Unit.Value;
        }
    }
}
