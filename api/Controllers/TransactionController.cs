using System.Collections.ObjectModel;
using System.Linq;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using api.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class TransactionController:ControllerBase
    {
        private readonly ITransactionRepository _repository;
        private readonly IAccountRepository _repositoryAccount;
        private readonly IMapper _mapper;
        public TransactionController([FromServices]ITransactionRepository repository,IMapper mapper,[FromServices]IAccountRepository repositoryAccount)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryAccount = repositoryAccount;
        }

        [HttpPost("")]
        public ActionResult<response> Create([FromBody] TransactionInputModel transactionInputModel)
        {
            response Response =new();
            try
            {
                if (ModelState.IsValid)
                {
                    var transaction=_mapper.Map<Transaction>(transactionInputModel);
                    var account = _repositoryAccount.GetById(transaction.accountId);
                    if (account.Balance < transaction.Amount)
                    {
                        Response.message = "insufficient accounting balance";
                        Response.sucess = false;
                        return Ok(Response);
                    }
                    transaction.PreviousBalance = account.Balance;
                    switch (transaction.transactionType)
                    {
                        case Enums.TransactionType.Expense:
                            account.Balance -= transaction.Amount;
                        break;
                        case Enums.TransactionType.Revenues:
                            account.Balance += transaction.Amount;
                        break;
                        case Enums.TransactionType.Transfer:
                            account.Balance -= transaction.Amount;
                        break;
                    }
                    transaction.Balance = account.Balance;
                    _repositoryAccount.Update(account);
                    _repository.Insert(transaction);
                    Response.message="transaction created";
                    return Created("account",Response);  
                }
                return BadRequest(ModelState);
            }
            catch (System.Exception ex)
            {
                Response.message=ex.Message;
                Response.sucess=false;
                return Response;
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<response> GetByAccount(int id)
        {
            response Response =new();
            try
            {
                Response.message = $"all transaction from account {id}";
                Response.data = _mapper.Map<Collection<TransactionOutputModel>>(_repository.GetAll().Include(x =>x.account).Where(x => x.accountId==id).ToList());
                return Ok(Response);
            }
            catch (System.Exception ex)
            {
                Response.message=ex.Message;
                Response.sucess=false;
                return Response;
            }
        }
    }
}