using System;
using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public TransactionType transactionType { get; set; }
        public string Description { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal Amount { get; set; }
        public int accountId { get; set; }
        public Account account { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}