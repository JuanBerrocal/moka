using Microsoft.AspNetCore.Mvc;
using Moka.Models;
using Moka.Data;
using Moka.DTOs;
using Moka.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace Moka.Controllers
{
    [Controller]
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

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken)
        {

            _mokaLogger.LogInformation("Creating a new transaction.");

            var result = await _transactionService.CreateTransactionAsync(transactionDto, cancellationToken);

            return StatusCode(201, "Transaction created successfully.");
           
           
        }
        
    }
}
