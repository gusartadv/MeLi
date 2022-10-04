using Service.Contracts.Requests;
using Service.Contracts.Responses;

namespace Service.Contracts.Interfaces
{
    /// <summary>
    /// Loan contracts
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Returns the values for the loan
        /// </summary>
        /// <param name="loanRequest"></param>
        /// <returns></returns>
        Task<LoanResponse> LoanRequest(LoanRequest loanRequest);

        /// <summary>
        /// Get the list of loans
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        Task<List<LoanListResponse>> GetListOfLoans(DateTime dateFrom, DateTime dateTo);
    }
}
