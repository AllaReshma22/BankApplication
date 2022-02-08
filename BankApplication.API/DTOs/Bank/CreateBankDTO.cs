namespace BankApplication.API.DTOs.Bank
{
    public class CreateBankDTO
    {
        public string? BankName { get; set; }
        public decimal? SameBankRtgsCharges { get; set; }
        public decimal? SameBankImpsCharges { get; set; }
        public decimal? OtherBankRtgsCharges { get; set; }
        public decimal? OtherBankImpsCharges { get; set; }
    }
}
