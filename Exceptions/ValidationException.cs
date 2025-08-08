using System;
using System.Collections.Generic;
using EmsalEWayBillSystem.Models;

namespace EmsalEWayBillSystem.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationError> Errors { get; }
        
        public ValidationException(string message) : base(message)
        {
            Errors = new List<ValidationError>();
        }
        
        public ValidationException(string message, List<ValidationError> errors) : base(message)
        {
            Errors = errors ?? new List<ValidationError>();
        }
        
        public ValidationException(string message, ValidationResult validationResult) : base(message)
        {
            Errors = validationResult?.Errors ?? new List<ValidationError>();
        }
    }
}