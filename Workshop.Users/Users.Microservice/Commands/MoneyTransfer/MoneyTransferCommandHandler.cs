using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Microservice.Commands.MoneyTransfer
{
    public class MoneyTransferCommandHandler : IRequestHandler<MoneyTransferCommand>
    {
        workshopusersdbContext _context;

        public MoneyTransferCommandHandler(workshopusersdbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(MoneyTransferCommand request, CancellationToken cancellationToken)
        {
            var seller = await _context.Users.Where(x => x.UserId == request.SellerId).FirstOrDefaultAsync();
            var buyer = await _context.Users.Where(x => x.UserId == request.BuyerId).FirstOrDefaultAsync();
            buyer.WalletBalance -= request.Price;
            seller.WalletBalance += request.Price;
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
