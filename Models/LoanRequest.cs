using System.ComponentModel.DataAnnotations;

namespace LoanCompareSite.Models
{
    public class LoanRequest
    {
        [Required]
        [Range(500, 100000000, ErrorMessage = "Enter a valid Amount (NGN500 - NGN100,000,000)")]
        public long amount { get; set; }
        [Required]
        [Range(1, 120, ErrorMessage = "Enter a valid number of months (1month - 120month)")]
        public int duration { get; set; }

    }
}