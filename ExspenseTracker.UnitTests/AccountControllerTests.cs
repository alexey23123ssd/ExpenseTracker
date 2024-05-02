using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Moq;
using WebAPI.Controllers;
namespace UnitTests
{
    public class Tests
    {
        private IAccountService _accountService;
        private IMapper _mapper;
#pragma warning disable NUnit1032
        private AccountController? _accountController;

        
            [Test]
            public void CreateAccount_ModelCreated_ModelStateIsValid()
            {
                //Arrange
                var account = new DTO.Account()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                };
                var serviceResponse = new ServiceDataResponse<Account>();
                serviceResponse.IsSuccess = true;
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.CreateAccountAsync(It.IsAny<Account>()))
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                var a = mapper.Setup(x => x.Map<Account>(It.IsAny<DTO.Account>()));
                _accountController = new AccountController(accountService.Object, mapper.Object);

                //Act
                var task = _accountController.CreateAccount(account);
                //Assert
                Assert.IsNotNull(task);
                Assert.IsInstanceOf<JsonResult>(task.Result);
                var jsonResult = task.Result as JsonResult;
                Assert.IsAssignableFrom<ServiceDataResponse<Account>>(jsonResult.Value);
                accountService.Verify();
            }

            [Test]
            public void CreateAccount_ReturnsBadRequest_ModelStateIsInvalid()
            {
                //Arrange
                var account = new DTO.Account()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                };
                var serviceResponse = new ServiceDataResponse<Account>();

                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.CreateAccountAsync(It.IsAny<Account>()))
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                mapper.Setup(x => x.Map<Account>(It.IsAny<DAL.Models.Account>()));
                _accountController = new AccountController(accountService.Object, mapper.Object);
                _accountController.ModelState.AddModelError("Name", "Name is required");
                //Act
                var result = _accountController.CreateAccount(account);
                //Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            }

            [Test]
            public void DeleteAccount_ModelDeleted_ModelStateIsValid()
            {
                //Arrange
                Guid id = Guid.NewGuid();
                var serviceResponse = new ServiceResponse();
                serviceResponse.IsSuccess = true;
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.DeleteAccountAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                _accountController = new AccountController(accountService.Object, mapper.Object);
                //Act
                var result = _accountController.DeleteAccount(id);
                //Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<OkObjectResult>(result.Result);
                var model = result.Result as OkObjectResult;
                Assert.IsAssignableFrom<ServiceDataResponse<Account>>(model.Value);
            }

            [Test]
            public void DeleteAccount_ModelNotDeleted_ModelStateIsInValid()
            {
                //Arrange
                Guid id = Guid.NewGuid();
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.DeleteAccountAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(new ServiceResponse())
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                _accountController = new AccountController(accountService.Object, mapper.Object);
                _accountController.ModelState.AddModelError("Id", "id doesnt valid");
                //Act
                var result = _accountController.DeleteAccount(id);
                //Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            }

            [Test]
            public void DeleteAccount_ModelNotDeleted_ModelStateIsNotFound()
            {
                //Arrange
                var serviceResult = new ServiceResponse();
                serviceResult.IsSuccess = false;
                Guid id = Guid.NewGuid();
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.DeleteAccountAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(serviceResult)
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                _accountController = new AccountController(accountService.Object, mapper.Object);
                //Act
                var result = _accountController.DeleteAccount(id);
                //Assert
                Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
                var notFoundObject = result.Result as NotFoundObjectResult;
                Assert.That(notFoundObject.Value.ToString(), Is.EqualTo("Account doesnt exist"));
                Assert.That(notFoundObject.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            }

            [Test]
            public void GetAccount_GetModel_ModelStateIsValid()
            {
                //Arrange
                Guid id = Guid.NewGuid();
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.GetAccountByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(new ServiceDataResponse<Account>())
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                _accountController = new AccountController(accountService.Object, mapper.Object);
                //Act
                var result = _accountController.GetAccount(id);
                //Assert
                Assert.IsInstanceOf<JsonResult>(result.Result);
                var model = result.Result as JsonResult;
                Assert.IsAssignableFrom<ServiceDataResponse<Account>>(model.Value);
            }

            [Test]
            public void GetAccount_DontGetModel_ModelStateIsInValid()
            {
                //Arrange
                Guid id = Guid.NewGuid();
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.GetAccountByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(new ServiceDataResponse<Account>())
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                _accountController = new AccountController(accountService.Object, mapper.Object);
                _accountController.ModelState.AddModelError("Id", "id doesnt valid");
                //Act
                var result = _accountController.GetAccount(id);
                //Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            }

            [Test]
            public void GetAccount_DontGetModel_ModelStateIsNull()
            {
                //Arrange
                ServiceDataResponse<Account> nullObj = null;
                Guid id = Guid.NewGuid();
                var accountService = new Mock<IAccountService>();
                accountService.Setup(x => x.GetAccountByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(nullObj)
                    .Verifiable();
                var mapper = new Mock<IMapper>();
                _accountController = new AccountController(accountService.Object, mapper.Object);
                //Act
                var result = _accountController.GetAccount(id);
                //Assert
                Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
                var notFoundObject = result.Result as NotFoundObjectResult;
                var a = notFoundObject.Value;
                Assert.That(notFoundObject.Value.ToString(), Is.EqualTo("Account with this Id doesnt exist"));
                Assert.That(notFoundObject.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            }


        [Test]
        public void GetAccounts_GetListOfAccs_ModelStateIsValid()
        {
            //Arrange
            var listOfAccs = new List<Account>
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",

                },
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test1",

                },
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test2",

                }
            };
            Guid id = Guid.NewGuid();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.GetAccountsAsync()).ReturnsAsync(new ServiceDataResponse<IEnumerable<Account>>());
            var mapper = new Mock<IMapper>();
            _accountController = new AccountController(accountService.Object, mapper.Object);
            //Act
            var result = _accountController.GetAccounts();
            //Asser
            Assert.IsInstanceOf<JsonResult>(result.Result);
            var model = result.Result as JsonResult;
            Assert.IsAssignableFrom<ServiceDataResponse<IEnumerable<Account>>>(model.Value);
        }
    }
}