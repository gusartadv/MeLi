using DAL.Context;
using MeliTest.Service.Implementations;
using Microsoft.Extensions.Logging;
using Service.Contracts.Requests;

namespace MeliTest.Test
{
    [TestClass]
    public class LoanServiceTest
    {
        private readonly LoanContext loanContext;
        private readonly ILogger<LoanService> _logger;

        [TestMethod]
        public void CalculateInstallment()
        {
            //Arrange
            LoanService loanService = new LoanService(loanContext, _logger);
            double installment = 0;

            //Act
            installment = Math.Round(loanService.CalculateInstallment(0.05, 12, 1000),2);

            //Assert
            Assert.AreEqual(85.61, installment);

        }

        [TestMethod]
        public void ValidateLoanRequestData()
        {
            //Arrange
            LoanService loanService = new LoanService(loanContext, _logger);
            LoanRequest loanRequest = new LoanRequest();
            bool result = false;

            //Act
            loanRequest.Amount = 1000;
            loanRequest.Term = 12;
            result = loanService.ValidateLoanRequestData(loanRequest);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void ValidateUser()
        {
            //Arrange
            LoanService loanService = new LoanService(loanContext, _logger);
            LoanRequest loanRequest = new LoanRequest();
            bool result = false;

            //Act
            loanRequest.UserId = 2;
            result = loanService.ValidateUser(loanRequest);

            //Assert
            Assert.IsTrue(result);

        }
    }
}