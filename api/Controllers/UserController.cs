using api.Dtos;
using api.Entities;
using api.Interfaces;
using api.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserController([FromServices]IUserRepository repository,IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }
                
        [HttpGet("{id:int}")]
        public ActionResult<response> GetById(int id)
        {
            var Response=new response();
            try
            {
                var user = _repository.GetById(id);
                if (user == null)
                {
                    Response.sucess=false;
                    Response.message="User not found";
                    return NotFound(Response);
                }
                Response.data=_mapper.Map<UserOutputModel>(user);
                Response.message="List of user";
                return Ok(Response);
            }
            catch (System.Exception ex)
            {
                Response.message=ex.Message;
                Response.sucess=false;
                return Response;
            }
        }


        [HttpPost("")]
        public ActionResult<response> add([FromBody] UserInputModel userInputModel)
        {
            response Response =new();
            try
            {
                if (ModelState.IsValid)
                {
                    var user=_mapper.Map<User>(userInputModel);
                    user.password=BC.HashPassword(user.password);
                    _repository.Insert(user);
                    Response.data=_mapper.Map<UserOutputModel>(user);
                    Response.message="User created";
                    return Created("user",Response);
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