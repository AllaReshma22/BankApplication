namespace BankApplication.API.DTOs.BankStaff
{
    public class UpdateBankDTO
    {
        public string? BankName { get; set; }
        public decimal? SameBankRtgsCharges { get; set; }
        public decimal? SameBankImpsCharges { get; set; }
        public decimal? OtherBankRtgsCharges { get; set; }
        public decimal? OtherBankImpsCharges { get; set; }
    }
}
