using System.Threading.Tasks;
using EmsalEWayBillSystem.Models;

namespace EmsalEWayBillSystem.Interfaces
{
    public interface IEWayBillValidator
    {
        /// <summary>
        /// Validate if invoice data is sufficient and valid for E-Way Bill generation
        /// </summary>
        /// <param name="invoice">Invoice to validate</param>
        /// <returns>Validation result with errors and warnings</returns>
        Task<ValidationResult> ValidateInvoiceDataAsync(Invoice invoice);
        
        /// <summary>
        /// Validate if trip data is sufficient and valid for E-Way Bill generation
        /// </summary>
        /// <param name="trip">Trip to validate</param>
        /// <returns>Validation result with errors and warnings</returns>
        Task<ValidationResult> ValidateTripDataAsync(Trip trip);
        
        /// <summary>
        /// Validate a GSTIN number format and checksum
        /// </summary>
        /// <param name="gstin">GSTIN to validate</param>
        /// <returns>True if valid, otherwise false</returns>
        bool ValidateGstin(string gstin);
        
        /// <summary>
        /// Validate an HSN code for format and existence
        /// </summary>
        /// <param name="hsnCode">HSN code to validate</param>
        /// <returns>True if valid, otherwise false</returns>
        bool ValidateHsnCode(string hsnCode);
        
        /// <summary>
        /// Validate a vehicle registration number
        /// </summary>
        /// <param name="vehicleNumber">Vehicle number to validate</param>
        /// <returns>True if valid, otherwise false</returns>
        bool ValidateVehicleNumber(string vehicleNumber);
        
        /// <summary>
        /// Validate transaction type code
        /// </summary>
        /// <param name="transactionType">Transaction type code</param>
        /// <returns>True if valid, otherwise false</returns>
        bool ValidateTransactionType(string transactionType);
        
        /// <summary>
        /// Validate distance between origin and destination
        /// </summary>
        /// <param name="distance">Distance in kilometers</param>
        /// <returns>True if valid, otherwise false</returns>
        bool ValidateDistance(decimal distance);
    }
}