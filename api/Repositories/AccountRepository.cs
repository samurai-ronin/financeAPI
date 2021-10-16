using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository([FromServices]FinanceContext context) : base(context)
        {
        }
    }
}