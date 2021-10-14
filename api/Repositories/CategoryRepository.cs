using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository([FromServices]FinanceContext context) : base(context)
        {
        }
    }
}