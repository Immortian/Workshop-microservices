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
        workshoptransactionsdbContext _context;

        public TransactionExecutionHandler(RabbitSender rabbitSender, workshoptransactionsdbContext context)
        {
            _rabbitSender = rabbitSender;
            _context = context;
        }

        public async Task<Unit> Handle(ConfirmedTransaction request, CancellationToken cancellationToken)
        {
            _rabbitSender.Send(new MoneyTransferCommand
            {
                TransactionId = request.TransactionId,
                BuyerId = request.BuyerId,
                SellerId = request.SellerId,
                Price = request.Price
            }, "Users.Transaction.MoneyTransfer", "MoneyTransfer");

            _rabbitSender.Send(new ItemTransferCommand
            {
                TransactionId = request.TransactionId,
                BuyerId = request.BuyerId,
                SellerId = request.SellerId,
                ItemId = request.ItemId,
                CollectionId = request.CollectionId
            }, "Items.Transaction.ItemTransfer", "ItemTransfer");

            await _context.TransactionInfos.AddAsync(new TransactionInfo
            {
                BuyerId = request.BuyerId,
                SellerId = request.SellerId,
                ItemId = request.ItemId,
                CollectionId = request.CollectionId,
                Price = request.Price,
                TransactionDatetime = DateTime.Now
            });
            await _context.SaveChangesAsync();

            _rabbitSender.Send(new ConfirmationCommand
            {
                TransactionId = request.TransactionId,
                IsOk = true
            }, request.Properties.ReplyTo, request.Properties.CorrelationId);

            return Unit.Value;
        }
    }
}
