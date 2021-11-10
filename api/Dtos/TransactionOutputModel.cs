using System;
using api.Enums;

namespace api.Dtos
{
    public class TransactionOutputModel
    {
        public int Id { get; set; }
        public TransactionType transactionType { get; set; }
        public string Description { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public AccountOutputModel account { get; set; }
        public DateTime Date { get; set; } 
    }
}