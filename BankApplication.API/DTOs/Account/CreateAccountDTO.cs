using System.ComponentModel.DataAnnotations;
namespace BankApplication.API.DTOs.BankStaff
{
    public class CreateAccountDTO
    {
        public string? AccountHolderName { get; set; }
        [Required]
        public int Password { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
