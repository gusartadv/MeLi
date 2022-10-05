using DAL.Context;
using DAL.Models;
using MeliTest.Service.Implementations;
using Microsoft.Extensions.Logging;
using Service.Contracts.Responses;

namespace MeliTest.Test
{
    [TestClass]
    public class DebtServiceTest
    {
        private readonly LoanContext loanContext;
        private readonly ILogger<DebtService> _logger;

        [TestMethod]
        public void CalculateBalanceTest()
        {
            //Arrange
            DebtService debtService = new DebtService(loanContext, _logger);
            List<Loan> loanList = new List<Loan>();
            List<DebtResponse> debtResponseExpected = new List<DebtResponse>();
            List<DebtResponse> debtResponseActual = new List<DebtResponse>();
            DateTime DateTo = DateTime.Now.AddMonths(4);

            loanList.Add(new Loan
            {
                LoanId = 1,
                UserId = 8,
                Amount = 110000,
                Term = 12,
                installment = 90.26,
                Rate = 0.15,
                Target = "NEW",
                Date = Convert.ToDateTime("2022-10-03 09:31:05.1365694"),
                Paid = 0
            });

            debtResponseExpected.Add(new DebtResponse
            {
                Paid = 722.08,
                LoanId = 1,
                Target = "NEW"
            });

            //Act
            debtResponseActual = debtService.CalculateBalance(loanList, DateTo);

            //Assert
            Assert.AreEqual(debtResponseExpected.FirstOrDefault().Paid, debtResponseActual.FirstOrDefault().Paid);
        }
    }
}
