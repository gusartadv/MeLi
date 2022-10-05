using DAL.Context;
using DAL.Models;
using MeliTest.Service.Exceptions;
using Microsoft.Extensions.Logging;
using Service.Contracts.Interfaces;
using Service.Contracts.Requests;
using Service.Contracts.Responses;

namespace MeliTest.Service.Implementations
{
    /// <summary>
    /// Implementation of the payment service
    /// </summary>
    public class PaymentService : IPaymentService
    {

        #region Injected Services

        private readonly ILogger<PaymentService> _logger;
        private readonly LoanContext loanContext;

        #endregion Injected Services

        #region "Constructor"

        public PaymentService(LoanContext loanContext, ILogger<PaymentService> logger)
        {
            this.loanContext = loanContext;
            this._logger = logger;
        }

        #endregion "Constructor"

        #region "PaymentService Member Actions"

        /// <summary>
        /// Register the payment
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns>PaymentResponse</returns>
        public async Task<PaymentResponse> RegisterPayment(PaymentRequest paymentRequest)
        {
            PaymentResponse paymentResponse = new PaymentResponse();
            double amount = 0;
            double debt = 0;

            try
            {
                if (ZeroValueValidation(paymentRequest))
                {
                    var loan = loanContext.Loans.First(x => x.LoanId == paymentRequest.LoanId);

                    if (ValidateDebt(loan) && ValidateAmount(loan, paymentRequest))
                    {
                        if (loan.Paid == 0)
                            amount = loan.Amount;
                        else
                            amount = loan.Paid;

                        debt = amount - paymentRequest.Amount;

                        //Realizar pago
                        Payment payment = new Payment { Amount = paymentRequest.Amount, LoanId = paymentRequest.LoanId };
                        loanContext.Add(payment);
                        await loanContext.SaveChangesAsync();

                        //Cada que se pague actualizar el campo de abonado
                        loan.Paid = debt;
                        loanContext.SaveChanges();

                        paymentResponse.Id = payment.PaymentId;
                        paymentResponse.LoanId = payment.LoanId;
                        paymentResponse.Debt = debt;

                    }
                }
                return paymentResponse;
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }

            
        }

        #endregion "PaymentService Member Actions"

        #region "Public methods"

        /// <summary>
        /// Validate Debt
        /// </summary>
        /// <param name="loan"></param>
        /// <param name="paymentRequest"></param>
        /// <returns>bool</returns>
        /// <exception cref="DataValidationException"></exception>
        public bool ValidateDebt(Loan loan)
        {
            if (loan == null)
            {
                throw new DataValidationException("Missing data to make the request.");
            }

            return true;
        }

        /// <summary>
        /// Validate Amount
        /// </summary>
        /// <param name="loan"></param>
        /// <param name="paymentRequest"></param>
        /// <returns>bool</returns>
        /// <exception cref="DataValidationException"></exception>
        public bool ValidateAmount(Loan loan, PaymentRequest paymentRequest)
        {
            if (loan.Paid < paymentRequest.Amount)
            {
                throw new DataValidationException("You are trying to pay more than the current debt");
            }
            return true;
        }

        /// <summary>
        /// Validates the zero value
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns>bool</returns>
        /// <exception cref="DataValidationException"></exception>
        public bool ZeroValueValidation(PaymentRequest paymentRequest)
        {
            if (paymentRequest.Amount <= 0)
            {
                throw new DataValidationException("The amount must be greater than zero.");
            }

            return true;
        }

        #endregion "Public methods"

    }
}