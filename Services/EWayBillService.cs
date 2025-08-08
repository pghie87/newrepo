using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EmsalEWayBillSystem.Data;
using EmsalEWayBillSystem.Exceptions;
using EmsalEWayBillSystem.Interfaces;
using EmsalEWayBillSystem.Models;

namespace EmsalEWayBillSystem.Services
{
    public class EWayBillService : IEWayBillService
    {
        private readonly IEWayBillAPIClient _apiClient;
        private readonly IEWayBillValidator _validator;
        private readonly IEWayBillMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<EWayBillService> _logger;
        
        public EWayBillService(
            IEWayBillAPIClient apiClient,
            IEWayBillValidator validator,
            IEWayBillMapper mapper,
            ApplicationDbContext dbContext,
            ILogger<EWayBillService> logger)
        {
            _apiClient = apiClient;
            _validator = validator;
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<EWayBill> GenerateEwayBillAsync(int invoiceId)
        {
            _logger.LogInformation($"Starting E-Way Bill generation for invoice ID: {invoiceId}");
            
            // Get invoice and related trip data
            var invoice = await GetInvoiceAsync(invoiceId);
            var trip = await GetTripForInvoiceAsync(invoice);
            
            // Validate data
            var invoiceValidationResult = await _validator.ValidateInvoiceDataAsync(invoice);
            var tripValidationResult = await _validator.ValidateTripDataAsync(trip);
            
            // Combine validation results
            foreach (var error in tripValidationResult.Errors)
            {
                invoiceValidationResult.AddError(error.Field, error.Message);
            }
            
            foreach (var warning in tripValidationResult.Warnings)
            {
                invoiceValidationResult.AddWarning(warning.Field, warning.Message);
            }
            
            // Check if there are validation errors
            if (invoiceValidationResult.HasErrors())
            {
                string errorMessages = string.Join(", ", invoiceValidationResult.GetErrorMessages());
                _logger.LogWarning($"Validation failed for invoice ID {invoiceId}: {errorMessages}");
                throw new ValidationException($"Invalid data for E-Way Bill generation: {errorMessages}", invoiceValidationResult);
            }
            
            // Map invoice and trip data to E-Way Bill format
            var ewayBillData = _mapper.InvoiceToEwayBillData(invoice, trip);
            
            try
            {
                // Call API to generate E-Way Bill
                _logger.LogInformation($"Calling E-Way Bill API for invoice {invoiceId}");
                var apiResponse = await _apiClient.GenerateEwayBillAsync(ewayBillData);
                
                // Convert response to EWayBill entity and save
                var ewayBill = _mapper.ApiResponseToEwayBill(apiResponse, invoiceId);
                
                // Retrieve the current user ID from the context (in a real app, get from authentication context)
                int currentUserId = 1; // For demonstration, assuming user ID 1
                ewayBill.CreatedById = currentUserId;
                
                // Set timestamps
                ewayBill.CreatedAt = DateTime.UtcNow;
                ewayBill.UpdatedAt = DateTime.UtcNow;
                
                // Save the entity
                _dbContext.EWayBills.Add(ewayBill);
                await _dbContext.SaveChangesAsync();
                
                // Update invoice with E-Way Bill reference
                await UpdateInvoiceWithEwayBillAsync(invoice, ewayBill);
                
                _logger.LogInformation($"Successfully generated E-Way Bill {ewayBill.EwayBillNumber} for invoice {invoiceId}");
                
                return ewayBill;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex,