using AutoMapper;
using ExpenseTrackerApp.Backend.Expense.Contracts.Transactions;

namespace ExpenseTrackerApp.Backend.Expense.Services
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Source → Destination
            // Domain → DTO
            CreateMap<Domain.Entities.Transaction, TransactionReadDto>();
            CreateMap<TransactionCreateDto, Domain.Entities.Transaction>();
            CreateMap<TransactionUpdateDto, Domain.Entities.Transaction>();

        }
    }
}
