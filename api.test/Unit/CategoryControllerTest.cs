using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        [Fact]
        public void GetById_WithUnexistingId_ReturnsNotfound()
        {
        CategoryInputModel categoryInputModel = new();
        Category category = new(){Id = 1,Name = "category"};
        _repoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => null);
        var expected = _sut.GetById(1).Result as NotFoundObjectResult;
        expected.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void GetById_WithExistingId_ReturnsOk()
        {
        CategoryInputModel categoryInputModel = new();
        Category category = new(){Id = 1,Name = "category"};
        _repoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(category);
        var expected = _sut.GetById(1).Result as OkObjectResult;
        expected.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOk()
        {
        List<Category> listCategory = new(){
            new Category(){Id = 1,Name = "category1"},
            new Category(){Id = 2,Name = "category2"},
            new Category(){Id = 3,Name = "category3"},
        };
        Category category = new(){Id = 1,Name = "category"};
        _repoMock.Setup(x => x.GetAll()).Returns(listCategory.AsQueryable());
        var expected = _sut.GetAll();
        Assert.IsType<OkObjectResult>(expected.Result as OkObjectResult);
        }
    }
}