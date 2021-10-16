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
    public class AccountControllerTest
    {
        private readonly Mock<IAccountRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly AccountController _sut;
        public AccountControllerTest()
        {
            _repoMock = new Mock<IAccountRepository>();
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new AutoProfile())
            );
             _mapper = config.CreateMapper();
             _sut = new AccountController(_repoMock.Object,_mapper);
        }

        [Fact]
        public void Create_WithMissingAttribute_ReturnsBadRequest()
        {
        AccountInputModel accountInputModel = new();
        _sut.ModelState.AddModelError("Type","Required");
        var expected = _sut.Create(accountInputModel).Result as BadRequestObjectResult;
        expected.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Create_WithRequiredAttribute_ReturnsCreated()
        {
        Account account = new();
        AccountInputModel accountInputModel = new();
        _repoMock.Setup(x => x.Insert(account)).Returns(account);
        var expected = _sut.Create(accountInputModel).Result as CreatedResult;
        expected.Should().BeOfType<CreatedResult>();
        }
    }
}