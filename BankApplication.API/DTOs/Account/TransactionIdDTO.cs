namespace BankApplication.API.DTOs.Account
{
    public class TransactionIdDTO
    {
        public string? transactionId { get; set; }
        public string? sourceAccountId { get; set; }
        public string? receiverAccountId { get; set; }
        public decimal amount { get; set; }
        public string? transactionType { get; set; }

    }
}
