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
    /// Implementation of the loan service
    /// </summary>
    public class LoanService : ILoanService
    {

        #region Injected Services

        private readonly LoanContext loanContext;
        private readonly ILogger<LoanService> _logger;

        #endregion Injected Services

        #region "Constructor"

        public LoanService(LoanContext loanContext, ILogger<LoanService> logger)
        {
            this.loanContext = loanContext;
            this._logger = logger;
        }

        #endregion "Constructor"

        #region "LoanService Member Actions"

        /// <summary>
        /// Returns the values for the loan
        /// </summary>
        /// <param name="loanRequest"></param>
        /// <returns>LoanResponse</returns>
        public async Task<LoanResponse> LoanRequest(LoanRequest loanRequest)
        {
            var result = new LoanResponse();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LoanRequest, Loan>());
            var mapper = new Mapper(config);

            try
            {
                if (ValidateLoanRequestData(loanRequest) && ValidateUser(loanRequest))
                {

                    //Number of user loans
                    int numberOfLoans = loanContext.Loans.Count(x => x.UserId == loanRequest.UserId);
                    double amountTotal = loanContext.Loans.Where(x => x.Date >= DateTime.Now.AddYears(-1) && x.Date <= DateTime.Now).Sum(a => a.Amount);

                    //Get user target
                    var userTarget = GetTargetValues(numberOfLoans, amountTotal);

                    //Update the user's target if it is the case
                    var user = loanContext.Users.Find(loanRequest.UserId);

                    if (user.TargetId != userTarget.TargetId)
                    {
                        user.TargetId = userTarget.TargetId;
                        await loanContext.SaveChangesAsync();
                    }

                    //Get the installment
                    loanRequest.installment = Math.Round(CalculateInstallment(userTarget.rate, loanRequest.Term, loanRequest.Amount), 2);

                    loanRequest.Rate = userTarget.rate;
                    loanRequest.Date = DateTime.Now;
                    loanRequest.Target = userTarget.name;

                    Loan loanObject = mapper.Map<Loan>(loanRequest);

                    loanContext.Add(loanObject);
                    await loanContext.SaveChangesAsync();

                    result.LoanId = loanObject.LoanId;
                    result.Installment = loanRequest.installment;

                }

                return result;
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }

        }

        /// <summary>
        /// Get the list of loans
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns>List LoanListResponse</returns>
        public async Task<List<LoanListResponse>> GetListOfLoans(DateTime dateFrom, DateTime dateTo)
        {
            var loanList = new List<Loan>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Loan, LoanListResponse>());
            var mapper = new Mapper(config);

            try
            {
                loanList = loanContext.Loans.Where(x => x.Date >= dateFrom && x.Date <= dateTo).ToList();

                List<LoanListResponse> loanListResponse = mapper.Map<List<Loan>, List<LoanListResponse>>(loanList);

                return loanListResponse;
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }

        }

        #endregion "LoanService Member Actions"

        #region "Public methods"

        /// <summary>
        /// Calculate the value of the installment
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="term"></param>
        /// <param name="amount"></param>
        /// <returns>double</returns>
        public double CalculateInstallment(double rate, int term, double amount)
        {
            return ((rate / term) + (rate / term) / (Math.Pow((1 + (rate / term)), term) - 1)) * amount;
        }

        /// <summary>
        /// Perform data validations
        /// </summary>
        /// <param name="loanRequest"></param>
        /// <returns>bool</returns>
        /// <exception cref="DataValidationException"></exception>
        public bool ValidateLoanRequestData(LoanRequest loanRequest)
        {
            if (loanRequest.Amount <= 0 || loanRequest.Term <= 0)
            {
                throw new DataValidationException("The amount and term must be greater than zero.");
            }

            return true;
        }

        /// <summary>
        /// Check if the user exists 
        /// </summary>
        /// <param name="loanRequest"></param>
        /// <returns>bool</returns>
        /// <exception cref="NotFoundException"></exception>
        public bool ValidateUser(LoanRequest loanRequest)
        {
            var user = loanContext.Users.Find(loanRequest.UserId);

            if (user == null)
            {
                throw new NotFoundException("The user is not registered in the database.");
            }

            return true;
        }

        /// <summary>
        /// Get the target corresponding to the user
        /// </summary>
        /// <param name="numberOfLoans"></param>
        /// <returns>double,double,string,name,int</returns>
        public (double rate, double amount, string name, int TargetId) GetTargetValues(int numberOfLoans, double amountTotal)
        {
            var targets = loanContext.Targets.Where(

                                                            x => x.LowerQuantityLimit <= numberOfLoans &&
                                                            (x.UpperQuantityLimit >= numberOfLoans || x.UpperQuantityLimit == 0)


                                                        ||

                                                            x.LowerAmountLimit <= amountTotal &&
                                                            (x.UpperAmountLimit >= numberOfLoans || x.UpperAmountLimit == 0)

                                                    );

            var maxValue = targets.Max(x => x.Max);
            Target target = targets.First(x => x.Max == maxValue);
            
            return (target.Rate, target.LowerQuantityLimit, target.Name, target.TargetId);

        }

        #endregion "Public methods"

    }
}