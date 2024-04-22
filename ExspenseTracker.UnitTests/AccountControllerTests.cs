using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using WebAPI.Controllers;
namespace ExspenseTracker.UnitTests
{
    public class Tests
    {
        private IAccountService _accountService;
        private IMapper _mapper;
        private AccountController _accountController;


        [Test]
        public  void CreateAccount_ModelCreated_ModelStateIsValid()
        {
            //Arrange
            var account = new DAL.Models.Account()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.CreateAccountAsync(It.IsAny<Account>()))
                .Returns((Task<ServiceDataResponse<Account>>)Task.CompletedTask)
                .Verifiable();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<Account>(It.IsAny<DAL.Models.Account>()));
            _accountController = new AccountController(accountService.Object, mapper.Object);

            //Act
            var result = _accountController.CreateAccount(account);
            //Assert
            Assert.That();
            Assert.Pass();
        }

        [Test]
        public void CreateAccount_ReturnsBadRequest_ModelStateIsInvalid()
        {
            //Arrange
            var account = new DAL.Models.Account()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.CreateAccountAsync(It.IsAny<Account>()))
                .Returns((Task<ServiceDataResponse<Account>>)Task.CompletedTask)
                .Verifiable();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<Account>(It.IsAny<DAL.Models.Account>()));
            _accountController = new AccountController(accountService.Object, mapper.Object);
            _accountController.ModelState.AddModelError("Name", "Name is required");
            //Act
            var result = _accountController.CreateAccount(account);
            //Assert
            var badRequestResult = Assert.
        }

        [Test]
        public void DeleteAccount_ModelDeleted_ModelStateIsValid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.DeleteAccountAsync(It.IsAny<Guid>()))
                .Returns((Task<ServiceResponse>)Task.CompletedTask)
                .Verifiable();
            var mapper = new Mock<IMapper>();
            _accountController = new AccountController(accountService.Object,mapper.Object);
            //Act
            var result = _accountController.DeleteAccount(id);
            //Assert

        }
    }
}