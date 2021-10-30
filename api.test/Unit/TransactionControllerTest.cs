using api.Controllers;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using api.Response;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api.test.Unit
{
    public class TransactionControllerTest
    {
        private readonly Mock<ITransactionRepository> _repoMock;
        private readonly Mock<IAccountRepository> _repoMockAccount;
        private readonly IMapper _mapper;
        private readonly TransactionController _sut;
        public TransactionControllerTest()
        {
            _repoMock = new Mock<ITransactionRepository>();
            _repoMockAccount = new Mock<IAccountRepository>();
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new AutoProfile())
            );
             _mapper = config.CreateMapper();
             _sut = new TransactionController(_repoMock.Object,_mapper,_repoMockAccount.Object);
        }

        [Fact]
        public void Create_WithMissingAttribute_ReturnsBadRequest()
        {
            TransactionInputModel transactionInputModel = new();
            _sut.ModelState.AddModelError("Balance","Required");
            var expected = _sut.Create(transactionInputModel).Result as BadRequestObjectResult;
            expected.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Create_WithRequiredAttribute_ReturnsCreated()
        {
            Account account = new(){ Name = "bank" ,Type = "type" ,Balance = 1000, UserId = 1};
            _repoMockAccount.Setup(x => x.GetById(1)).Returns(account);
            Transaction transaction = new(){ transactionType = Enums.TransactionType.Expense, Amount = 500,accountId = 1, Description = "description"};
            TransactionInputModel transactionInputModel = new(){ transactionType = Enums.TransactionType.Expense, Amount = 500,accountId = 1, Description = "description"};
            _repoMock.Setup(x => x.Insert(transaction)).Returns(transaction);
            var expected = _sut.Create(transactionInputModel).Result as CreatedResult;
            expected.Should().BeOfType<CreatedResult>();
        }

        [Fact(Skip = "por terminar")]
        public void Create_InsufficientAccountingBalance_ReturnsException()
        {
            Account account = new(){ Name = "bank" ,Type = "type" ,Balance = 100, UserId = 1};
            _repoMockAccount.Setup(x => x.GetById(1)).Returns(account);
            Transaction transaction = new(){ transactionType = Enums.TransactionType.Expense, Amount = 500,accountId = 1, Description = "description"};
            TransactionInputModel transactionInputModel = new(){ transactionType = Enums.TransactionType.Expense, Amount = 500,accountId = 1, Description = "description"};
            _repoMock.Setup(x => x.Insert(transaction)).Returns(transaction);
            var expected = _sut.Create(transactionInputModel).Value as response;
            Assert.Equal("insufficient accounting balance",expected.message);
        }

    }
}