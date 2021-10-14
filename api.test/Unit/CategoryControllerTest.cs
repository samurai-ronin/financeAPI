using api.Controllers;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api.test.Unit
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly CategoryController _sut;
        public CategoryControllerTest()
        {
            _repoMock = new Mock<ICategoryRepository>();
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new AutoProfile())
            );
             _mapper = config.CreateMapper();
             _sut = new CategoryController(_repoMock.Object,_mapper);
        }

        [Fact]
        public void AddCategory_WithMissingAttribute_ReturnsBadRequest()
        {
        CategoryInputModel categoryInputModel = new();
        _sut.ModelState.AddModelError("name","Required");
        var expected = _sut.Add(categoryInputModel).Result as BadRequestObjectResult;
        expected.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void AddCategory_WithRequiredAttribute_ReturnsCreated()
        {
        CategoryInputModel categoryInputModel = new();
        Category category = new(){Id = 1,Name = "category"};
        _repoMock.Setup(x => x.Insert(category)).Returns(category);
        var expected = _sut.Add(categoryInputModel).Result as CreatedResult;
        expected.Should().BeOfType<CreatedResult>();
        }
    }
}