using System.ComponentModel.DataAnnotations;
namespace BankApplication.API.DTOs.BankStaff
{
    public class UpdateAccountDTO
    {
        public string? AccountHolderName { get; set; }
        public string? AccountId { get; set; }
        public int? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public int ConfirmPassword { get; set; }
    }
}

