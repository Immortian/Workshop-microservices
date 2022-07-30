using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using ConfirmTransactions.Microservice.Commands.ConfirmTransaction.ConfirmWalletBalance;
using ConfirmTransactions.Microservice.Controllers.Base;

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
        public async Task PostAsync([FromBody] TransactionConfirmation value)
        {
            _context.TransactionConfirmations.Add(value);
            var request = new ConfirmWalletBalanceCommand
            {
                TransactionId = value.TransactionId,
                BuyerId = value.BuyerId,
                Price = value.Price
            };
            await Mediator.Send(request);
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
