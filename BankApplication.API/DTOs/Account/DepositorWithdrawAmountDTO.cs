using System.ComponentModel.DataAnnotations;
namespace BankApplication.API.DTOs.Account
{
    public class DepositorWithdrawAmountDTO
    {
        [Required]
        public string? AccountId { get; set; }
        [Required]
        public string? Password { get; set; }
        public decimal Amount { get; set; }
    }
}
