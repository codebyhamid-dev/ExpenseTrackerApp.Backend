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
        [HttpGet("get-all-transactions")]
        public async Task<PagedResultDto<TransactionReadDto>> GetAllTransactions([FromQuery] TransactionInputDto inputDto)
        {
            return await _transactionAppService.GetAllTransactionsAsync(inputDto);
        }
        [HttpGet("get-transaction-by-id/{transactionId}")]
        public async Task<TransactionReadDto> GetTransactionById([FromRoute] Guid transactionId)
        {
            return await _transactionAppService.GetTransactionByIdAsync(transactionId);
        }
        [HttpPost("update-transaction/{transactionId}")]
        public async Task<TransactionReadDto> UpdateTransaction([FromRoute] Guid transactionId, [FromBody] TransactionUpdateDto transactionUpdateDto)
        {
            return await _transactionAppService.UpdateTransactionAsync(transactionId, transactionUpdateDto);
        }
        [HttpDelete("delete-transaction/{transactionId}")]
        public async Task DeleteTransaction([FromRoute] Guid transactionId)
        {
            await _transactionAppService.DeleteTransactionAsync(transactionId);
        }

        [HttpGet("get-transaction-dashboard-cards")]
        public async Task<TransactionDashboardCardsDto> GetTransactionDashboardCards()
        {
            return await _transactionAppService.GetTransactionDashboardCardsAsync();
        }
    }
}
