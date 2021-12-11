using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos
{
    public class TransactionInputModel
    {
        [Required]
        public TransactionType transactionType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int accountId { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}