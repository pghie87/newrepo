using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmsalEWayBillSystem.Interfaces
{
    public interface IEWayBillAPIClient
    {
        /// <summary>
        /// Authenticate with the E-Way Bill portal and get access token.
        /// </summary>
        /// <returns>Authentication token string</returns>
        /// <exception cref="AuthenticationException">If authentication fails</exception>
        Task<string> AuthenticateAsync();
        
        /// <summary>
        /// Generate E-Way Bill using the government API.
        /// </summary>
        /// <param name="data">E-Way Bill data in required format</param>
        /// <returns>API response with E-Way Bill details</returns>
        /// <exception cref="ApiException">If API call fails</exception>
        /// <exception cref="ValidationException">If API rejects data</exception>
        Task<Dictionary<string, object>> GenerateEwayBillAsync(Dictionary<string, object> data);
        
        /// <summary>
        /// Fetch details of an E-Way Bill by number.
        /// </summary>
        /// <param name="ewayBillNumber">E-Way Bill number to fetch</param>
        /// <returns>E-Way Bill details from API</returns>
        /// <exception cref="ApiException">If API call fails</exception>
        /// <exception cref="NotFoundException">If E-Way Bill doesn't exist</exception>
        Task<Dictionary<string, object>> GetEwayBillAsync(string ewayBillNumber);
        
        /// <summary>
        /// Extend an E-Way Bill validity.
        /// </summary>
        /// <param name="ewayBillNumber">E-Way Bill number to extend</param>
        /// <param name="data">Extension details</param>
        /// <returns>Updated E-Way Bill details</returns>
        /// <exception cref="ApiException">If API call fails</exception>
        /// <exception cref="ValidationException">If extension data is invalid</exception>
        Task<Dictionary<string, object>> ExtendEwayBillAsync(string ewayBillNumber, Dictionary<string, object> data);
        
        /// <summary>
        /// Cancel an E-Way Bill.
        /// </summary>
        /// <param name="ewayBillNumber">E-Way Bill number to cancel</param>
        /// <param name="data">Cancellation reason and details</param>
        /// <returns>Cancellation confirmation</returns>
        /// <exception cref="ApiException">If API call fails</exception>
        /// <exception cref="ValidationException">If cancellation is not allowed</exception>
        Task<Dictionary<string, object>> CancelEwayBillAsync(string ewayBillNumber, Dictionary<string, object> data);
    }
}