using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Microservice.Commands.ItemTransfer
{
    public class ItemTransferCommandHandler : IRequestHandler<ItemTransferCommand>
    {
        workshopitemsdbContext _context;

        public ItemTransferCommandHandler(workshopitemsdbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ItemTransferCommand request, CancellationToken cancellationToken)
        {
            if(request.CollectionId is not null)
            {
                var collection = await _context.ItemCollections.Where(x => x.CollectionId == request.CollectionId).FirstOrDefaultAsync();
                collection.CollectionOwnerId = request.BuyerId;
                foreach(var item in _context.Items.Where(x=>x.ItemCollectionId == request.CollectionId))
                {
                    item.ItemOwnerId = request.BuyerId;
                }
            }
            else if(request.ItemId is not null)
            {
                var item = await _context.Items.Where(x => x.ItemId == request.ItemId).FirstOrDefaultAsync();
                item.ItemOwnerId = request.BuyerId;
            }
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
