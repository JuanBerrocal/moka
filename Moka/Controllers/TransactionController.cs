using Microsoft.AspNetCore.Mvc;
using Moka.Models;
using Moka.Data;
using Moka.DTOs;
using Moka.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace Moka.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TransactionsController : ControllerBase
    {

        private readonly MokaDbContext _mokaDbContext;
        private readonly ILogger<TransactionsController> _mokaLogger;
        private readonly TransactionService _transactionService;

        public TransactionsController(MokaDbContext mokaDbContext, ILogger<TransactionsController> mokaLogger, TransactionService transactionService)
        {
            _mokaDbContext = mokaDbContext;

            _mokaLogger = mokaLogger;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactions(CancellationToken cancellationToken)
        {
            _mokaLogger.LogInformation("Retrieving all transactions.");

            var transactions = await _transactionService.GetTransactionsAsync(cancellationToken);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody]CreateTransactionDto transactionDto, CancellationToken cancellationToken)
        {

            _mokaLogger.LogInformation("Creating a new transaction.");

            var result = await _transactionService.CreateTransactionAsync(transactionDto, cancellationToken);

            return StatusCode(201, "Transaction created successfully.");
           
           
        }
        
    }
}
