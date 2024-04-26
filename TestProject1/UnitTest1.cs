using BL.Models;
namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Assert
            var account = new DTO.Account()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
            };
            var serviceResponse = new ServiceDataResponse<Account>();
            //Act

            //Arrange
        }
    }
}