using Service.Contracts.Requests;
using Service.Contracts.Responses;

namespace Service.Contracts.Interfaces
{
    /// <summary>
    /// Debt contracts
    /// </summary>
    public interface IDebtService 
    {
        /// <summary>
        /// Get the list of loans using multiple filters
        /// </summary>
        /// <param name="debtRequest"></param>
        /// <returns></returns>
        Task<List<DebtResponse>> GetBalance(DebtRequest debtRequest);
    }
}
