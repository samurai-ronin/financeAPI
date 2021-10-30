using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository([FromServices]FinanceContext context) : base(context)
        {
        }
    }
}