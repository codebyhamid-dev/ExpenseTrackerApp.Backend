namespace ExpenseTrackerApp.Backend.Expense.Domain.Enum
{
    public static class AppEnums
    {
        // 🔹 Whether a transaction or category is income or expense
        public enum CategoryType
        {
            Income = 0,
            Expense = 1
        }

        // 🔹 Used in Transaction to identify income or expense type
        public enum TransactionType
        {
            Income = 0,
            Expense = 1
        }

        // 🔹 Payment options
        public enum PaymentMode
        {
            Cash = 0,
            Bank = 1,
            Card = 2
        }
    }
}
