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
        /// <returns>LoanResponse</returns>
        Task<LoanResponse> LoanRequest(LoanRequest loanRequest);

        /// <summary>
        /// Get the list of loans
        /// </summary>
        /// <param name="loanListRequest"></param>
        /// <returns>List<LoanListResponse></returns>
        Task<List<LoanListResponse>> GetListOfLoans(LoanListRequest loanListRequest);
    }
}
