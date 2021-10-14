using System.Collections.ObjectModel;
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
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryController([FromServices]ICategoryRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("")]
        public ActionResult<response> Add([FromBody] CategoryInputModel categoryInputModel)
        {
            response Response =new();
            try
            {
                if (ModelState.IsValid)
                {
                    var category=_mapper.Map<Category>(categoryInputModel);
                    _repository.Insert(category);
                    Response.data=_mapper.Map<CategoryOutputModel>(category);
                    Response.message="Category created";
                    return Created("category",Response);
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
        public ActionResult<response> GetById(int id)
        {
            var Response=new response();
            try
            {
                var category = _repository.GetById(id);
                if (category == null)
                {
                    Response.sucess=false;
                    Response.message="category not found";
                    return NotFound(Response);
                }
                Response.data=_mapper.Map<CategoryOutputModel>(category);
                return Ok(Response);
            }
            catch (System.Exception ex)
            {
                Response.message=ex.Message;
                Response.sucess=false;
                return Response;
            }
        }

        [HttpGet]
        public ActionResult<response> GetAll()
        {
            response Response=new();
            try
            {
                Response.data=_mapper.Map<Collection<CategoryOutputModel>>(_repository.GetAll());
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