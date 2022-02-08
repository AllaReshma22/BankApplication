namespace BankApplication.API.DTOs.Account
{
    public class TransferAmountDTO
    {
        public string? receiverAccountId { get; set; }
        public decimal amount { get; set; }
        public string? senderAccountId { get; set; }
        public string? paymentMode { get; set; }
    }
}
