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
    public class UserControllerTest
    {
        private readonly Mock<IUserRepository> _repoMock;
        private readonly IMapper _mapper;
        public UserControllerTest()
        {
            _repoMock = new Mock<IUserRepository>();
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new AutoProfile())
            );
             _mapper = config.CreateMapper();
        }
    
        [Fact]
        public void CreateUser_WithAllAttribute_ReturnsCreated()
        {
        //Given
        User user = new(){id = 1,name = "Mauro",password = "123"};
        UserInputModel userDto = new(){name = "Mauro", password = "34"};
        _repoMock.Setup(x => x.Insert(user)).Returns(user);
        //When
        var userController = new UserController(_repoMock.Object,_mapper);
        var expected = userController.add(userDto).Result as CreatedResult;
        //Then
        Assert.Equal(201,expected.StatusCode);
        }

        [Fact]
        public void CreateUser_WithMissingAttribute_ReturnsBadRequest()
        {
        //Given
        UserInputModel userDto = new();
        //When
        var userController = new UserController(_repoMock.Object,_mapper);
        userController.ModelState.AddModelError("name","Required");
        var expected = userController.add(userDto).Result as BadRequestObjectResult;
        //Then
        Assert.Equal(400,expected.StatusCode);
        }

    }
}