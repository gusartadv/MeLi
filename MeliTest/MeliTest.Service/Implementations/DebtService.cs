using AutoMapper;
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
    /// Implementation of debt service
    /// </summary>
    public class DebtService : IDebtService
    {
        #region Injected Services

        private readonly ILogger<DebtService> _logger;
        private readonly LoanContext loanContext;

        #endregion Injected Services

        #region "Constructor"

        public DebtService(LoanContext loanContext, ILogger<DebtService> logger)
        {
            this.loanContext = loanContext;
            this._logger = logger;
        }

        #endregion "Constructor"

        #region "DebtService Member Actions"

        /// <summary>
        /// Get the list of loans using multiple filters
        /// </summary>
        /// <param name="debtRequest"></param>
        /// <returns>List DebtResponse</returns>
        public async Task<List<DebtResponse>> GetBalance(DebtRequest debtRequest)
        {
            var loanList = new List<Loan>();
            var debtResponse = new List<DebtResponse>();

            try
            {
                if (debtRequest.LoanId == 0 && debtRequest.Target == null)
                    loanList = loanContext.Loans.Where(x => x.Date <= debtRequest.DateTo).ToList();
                if (debtRequest.LoanId != 0)
                    loanList = loanContext.Loans.Where(x => x.Date <= debtRequest.DateTo && x.LoanId == debtRequest.LoanId).ToList();
                if (debtRequest.Target != null)
                    loanList = loanContext.Loans.Where(x => x.Date <= debtRequest.DateTo && x.Target == debtRequest.Target).ToList();

                if (loanList.Count == 0)
                {
                    throw new NotFoundException("No results were found for this search.");
                }

                debtResponse = CalculateBalance(loanList, debtRequest.DateTo);
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }                     

            return debtResponse;
        }

        #endregion "DebtService Member Actions"

        #region "Public methods"

        /// <summary>
        /// Calculate the balance of the list of loans
        /// </summary>
        /// <param name="loanList"></param>
        /// <param name="DateTo"></param>
        /// <returns>List DebtResponse</returns>
        public List<DebtResponse> CalculateBalance(List<Loan> loanList, DateTime DateTo)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Loan, DebtResponse>());
            var mapper = new Mapper(config);

            try
            {
                loanList.ToList().ForEach(
                   u =>
                   {
                       u.Paid = Math.Round((u.installment * u.Term) - Convert.ToInt32((((DateTo - u.Date).TotalDays) / 30)) * (u.installment), 2);
                       u.LoanId = u.LoanId;
                       u.Target = u.Target;
                   });


                return mapper.Map<List<Loan>, List<DebtResponse>>(loanList);
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }          
            
        }

        #endregion "Public methods"
    }
}