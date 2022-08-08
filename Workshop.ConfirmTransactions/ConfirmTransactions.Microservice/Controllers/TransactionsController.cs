using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using ConfirmTransactions.Microservice.Commands.ConfirmTransaction.ConfirmWalletBalance;
using ConfirmTransactions.Microservice.Controllers.Base;
using ConfirmTransactions.Microservice.Controllers.Models;
using ConfirmTransactions.Microservice.Commands.ConfirmTransaction.ConfirmItemOwner;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfirmTransactions.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly transactionconfirmationdbContext _context;
        public TransactionsController(transactionconfirmationdbContext context)
        {
            _context = context;
        }
        // GET: api/<TransactionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransactionsController>
        /// <summary>
        /// Send topic exchange to rabbitMQ to confirm buier wallet balane
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task PostAsync([FromBody] Transaction value)
        {
            var currentTransaction = new TransactionConfirmation
            {
                BuyerId = value.BuyerId,
                SellerId = value.SellerId,
                CollectionId = value.CollectionId,
                ItemId = value.ItemId,
                Price = value.Price,
                IsItemOwnerOk = null,
                IsWalletBalanceOk = null
            };
            _context.TransactionConfirmations.Add(currentTransaction);
            await _context.SaveChangesAsync();
            var WalletBalanceRequest = new ConfirmWalletBalanceCommand
            {
                TransactionId = currentTransaction.TransactionId,
                BuyerId = currentTransaction.BuyerId,
                Price = currentTransaction.Price,
                Properties = null
            };

            await Mediator.Send(WalletBalanceRequest);

            var ItemOwnerRequest = new ConfirmItemOwnerCommand
            {
                TransactionId = currentTransaction.TransactionId,
                SellerId = currentTransaction.SellerId,
                CollectionId = currentTransaction.CollectionId,
                ItemId = currentTransaction.ItemId,
                Properties = null
            };

            await Mediator.Send(ItemOwnerRequest);
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TransactionConfirmation value)
        {
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
