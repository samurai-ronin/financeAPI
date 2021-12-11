using api.Dtos;
using api.Entities;

namespace api.Interfaces
{
    public interface ITransactionRepository:IRepository<Transaction>
    {
        bool AddTransaction(TransactionInputModel transactionInputModel);
    }
}