using ExpenseTrackerApp.Backend.Expense.Contracts.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionAppService _transactionAppService;

        public TransactionController(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }

        [HttpPost("add-transaction")]
        public async Task<TransactionReadDto> CreateTransaction([FromBody] TransactionCreateDto transactionCreateDto)
        {

            return await _transactionAppService.CreateTransactionAsync(transactionCreateDto);
        }
    }
}
