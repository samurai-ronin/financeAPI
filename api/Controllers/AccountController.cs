using api.Dtos;
using api.Entities;
using api.Interfaces;
using api.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class AccountController:ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        public AccountController(IAccountRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("")]
        public ActionResult<response> Create([FromBody] AccountInputModel accountInputModel)
        {
            response Response =new();
            try
            {
                if (ModelState.IsValid)
                {
                    var account=_mapper.Map<Account>(accountInputModel);
                    _repository.Insert(account);
                    Response.data=_mapper.Map<AccountOutputModel>(account);
                    Response.message="Account created";
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
    }
}