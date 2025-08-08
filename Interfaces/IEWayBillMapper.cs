using System.Collections.Generic;
using EmsalEWayBillSystem.Models;

namespace EmsalEWayBillSystem.Interfaces
{
    public interface IEWayBillMapper
    {
        /// <summary>
        /// Convert invoice and trip data to E-Way Bill API request format
        /// </summary>
        /// <param name="invoice">Invoice entity</param>
        /// <param name="trip">Trip entity</param>
        /// <returns>Dictionary with data formatted for the E-Way Bill API</returns>
        Dictionary<string, object> InvoiceToEwayBillData(Invoice invoice, Trip trip);
        
        /// <summary>
        /// Convert API response to EWayBill entity
        /// </summary>
        /// <param name="response">API response data</param>
        /// <param name="invoiceId">ID of the invoice</param>
        /// <returns>EWayBill entity populated with API response data</returns>
        EWayBill ApiResponseToEwayBill(Dictionary<string, object> response, int invoiceId);
    }
}