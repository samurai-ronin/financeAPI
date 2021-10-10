using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository([FromServices]FinanceContext context) : base(context)
        {
        }
    }
}