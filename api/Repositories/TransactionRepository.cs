using api.Data;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly FinanceContext _context;
        public TransactionRepository([FromServices]FinanceContext context) : base(context)
        {
            _context = context;
        }

        public bool AddTransaction(TransactionInputModel transactionInputModel)
        {
            throw new System.NotImplementedException();
        }
    }
}