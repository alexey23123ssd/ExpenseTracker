using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using Moq;
using WebAPI.Controllers;
namespace ExspenseTracker.UnitTests
{
    public class Tests
    {
        private IAccountService _accountService;
        private IMapper _mapper;
        private AccountController _accountController;
        [SetUp]
        public void Setup()
        {
            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.CreateAccountAsync(It.IsAny<Account>()));
            _accountService = Mock.Of<IAccountService>();
            _mapper = Mock.Of<IMapper>();
            _accountController = new AccountController(_accountService,_mapper);
        }

        [Test]
        public void CreateAccount_WithValidModel_ModelCreated()
        {
            //Arrange
            
            //Act
            
            //Assert
            Assert.Pass();
        }
    }
}