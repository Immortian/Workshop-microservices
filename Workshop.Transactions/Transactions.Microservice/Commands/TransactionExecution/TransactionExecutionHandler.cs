using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.Microservice.Commands.TransactionExecution
{
    public class TransactionExecutionHandler : IRequestHandler<ConfirmedTransaction>
    {
        RabbitSender _rabbitSender;

        public TransactionExecutionHandler(RabbitSender rabbitSender)
        {
            _rabbitSender = rabbitSender;
        }

        public Task<Unit> Handle(ConfirmedTransaction request, CancellationToken cancellationToken)
        {
            _rabbitSender.Send(new MoneyTransferCommand
            {
                TransactionId = request.TransactionId,
                BuyerId = request.BuyerId,
                SellerId = request.SellerId,
                Price = request.Price,
                Success = null
            }, "Users.Transaction.MoneyTransfer", "MoneyTransfer");

            _rabbitSender.Send(new ItemTransferCommand
            {
                TransactionId = request.TransactionId,
                BuyerId = request.BuyerId,
                SellerId = request.SellerId,
                ItemId = request.ItemId,
                CollectionId = request.CollectionId,
                Success = null
            }, "Items.Transaction.ItemTransfer", "ItemTransfer");
        }
    }
}
