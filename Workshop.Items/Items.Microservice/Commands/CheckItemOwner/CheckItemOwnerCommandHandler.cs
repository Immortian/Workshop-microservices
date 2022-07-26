﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Microservice.Commands.CheckItemOwner
{
    public class CheckItemOwnerCommandHandler : IRequestHandler<CheckItemOwnerCommand>
    {
        RabbitSender _rabbitSender;
        workshopitemsdbContext _context;
        public CheckItemOwnerCommandHandler(RabbitSender rabbitSender, workshopitemsdbContext context)
        {
            _rabbitSender = rabbitSender;
            _context = context;
        }

        public async Task<Unit> Handle(CheckItemOwnerCommand request, CancellationToken cancellationToken)
        {
            bool IsOk = true;

            if (request.CollectionId != null)
            {
                var collection = await _context.ItemCollections.Where(x => x.CollectionId == request.CollectionId).FirstOrDefaultAsync();
                if (collection is null || collection.CollectionOwnerId != request.SellerId)
                    IsOk = false;

                foreach (var item in _context.Items.Where(x => x.ItemCollectionId == request.CollectionId))
                {
                    if (item.ItemOwnerId != request.SellerId)
                        IsOk = false;
                }
            }
            else if (request.ItemId != null)
            {
                var item = await _context.Items.Where(x => x.ItemId == request.ItemId).FirstOrDefaultAsync();
                if (item is null || item.ItemOwnerId != request.SellerId)
                    IsOk = false;
            }
            else
                IsOk = false;

            _rabbitSender.Send(new ResponseCommand
            {
                TransactionId = request.TransactionId,
                IsOk = IsOk
            }, request.Properties.ReplyTo, request.Properties.CorrelationId);

            return Unit.Value;
        }
    }
}
