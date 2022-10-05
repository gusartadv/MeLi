using DAL.Context;
using DAL.Models;
using MeliTest.Service.Implementations;
using Microsoft.Extensions.Logging;
using Service.Contracts.Requests;

namespace MeliTest.Test
{
    [TestClass]
    public class PaymentServiceTest
    {
        private readonly LoanContext loanContext;
        private readonly ILogger<PaymentService> _logger;

        [TestMethod]
        public void ValidateDebtTest() 
        {
            //Arrange
            PaymentService paymentnService = new PaymentService(loanContext, _logger);
            Loan loan = new Loan();

            //Act
            var result = paymentnService.ValidateDebt(loan);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateAmountTest()
        {
            //Arrange
            PaymentService paymentnService = new PaymentService(loanContext, _logger);
            Loan loan = new Loan();
            loan.Paid = 100000;
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Amount = 80000;

            //Act
            var result = paymentnService.ValidateAmount(loan,paymentRequest);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ZeroValueValidationTest()
        {
            //Arrange
            PaymentService paymentnService = new PaymentService(loanContext, _logger);
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Amount = 100000;

            //Act
            var result = paymentnService.ZeroValueValidation(paymentRequest);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
