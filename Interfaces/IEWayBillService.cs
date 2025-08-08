using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmsalEWayBillSystem.Models;

namespace EmsalEWayBillSystem.Interfaces
{
    public interface IEWayBillService
    {
        /// <summary>
        /// Generate an E-Way Bill for the specified invoice.
        /// </summary>
        /// <param name="invoiceId">ID of the invoice to generate E-Way Bill for</param>
        /// <returns>EWayBill object with generated details</returns>
        /// <exception cref="ValidationException">If invoice data is invalid for E-Way Bill generation</exception>
        /// <exception cref="ApiException">If E-Way Bill API call fails</exception>
        /// <exception cref="NotFoundException">If invoice with given ID doesn't exist</exception>
        Task<EWayBill> GenerateEwayBillAsync(int invoiceId);
        
        /// <summary>
        /// Validate if the invoice data is sufficient for E-Way Bill generation.
        /// </summary>
        /// <param name="invoiceId">ID of the invoice to validate</param>
        /// <returns>ValidationResult containing validation status and any errors/warnings</returns>
        /// <exception cref="NotFoundException">If invoice with given ID doesn't exist</exception>
        Task<ValidationResult> ValidateEwayBillDataAsync(int invoiceId);
        
        /// <summary>
        /// Extend the validity of an existing E-Way Bill.
        /// </summary>
        /// <param name="ewayBillId">ID of the E-Way Bill to extend</param>
        /// <param name="reason">Reason for extension</param>
        /// <param name="extensionDate">New validity date</param>
        /// <returns>Updated EWayBill object</returns>
        /// <exception cref="ValidationException">If extension is not allowed or data is invalid</exception>
        /// <exception cref="ApiException">If E-Way Bill API call fails</exception>
        /// <exception cref="NotFoundException">If E-Way Bill with given ID doesn't exist</exception>
        Task<EWayBill> ExtendEwayBillAsync(int ewayBillId, string reason, DateTime extensionDate);
        
        /// <summary>
        /// Cancel an existing E-Way Bill.
        /// </summary>
        /// <param name="ewayBillId">ID of the E-Way Bill to cancel</param>
        /// <param name="reason">Reason for cancellation</param>
        /// <returns>True if cancellation successful, False otherwise</returns>
        /// <exception cref="ValidationException">If cancellation is not allowed</exception>
        /// <exception cref="ApiException">If E-Way Bill API call fails</exception>
        /// <exception cref="NotFoundException">If E-Way Bill with given ID doesn't exist</exception>
        Task<bool> CancelEwayBillAsync(int ewayBillId, string reason);
        
        /// <summary>
        /// Get current status of an E-Way Bill.
        /// </summary>
        /// <param name="ewayBillId">ID of the E-Way Bill to check</param>
        /// <returns>Current status of the E-Way Bill</returns>
        /// <exception cref="NotFoundException">If E-Way Bill with given ID doesn't exist</exception>
        Task<EWayBillStatus> GetEwayBillStatusAsync(int ewayBillId);
        
        /// <summary>
        /// Get list of E-Way Bills based on filters.
        /// </summary>
        /// <param name="filters">Dictionary of filter criteria</param>
        /// <returns>List of matching EWayBill objects</returns>
        Task<List<EWayBill>> ListEwayBillsAsync(Dictionary<string, object> filters);
    }
}