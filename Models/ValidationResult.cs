using System.Collections.Generic;
using System.Linq;

namespace EmsalEWayBillSystem.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; private set; }
        public List<ValidationWarning> Warnings { get; private set; }
        
        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<ValidationError>();
            Warnings = new List<ValidationWarning>();
        }
        
        public void AddError(string field, string message)
        {
            Errors.Add(new ValidationError { Field = field, Message = message });
            IsValid = false;
        }
        
        public void AddWarning(string field, string message)
        {
            Warnings.Add(new ValidationWarning { Field = field, Message = message });
        }
        
        public bool HasErrors()
        {
            return Errors.Any();
        }
        
        public bool HasWarnings()
        {
            return Warnings.Any();
        }
        
        public List<string> GetErrorMessages()
        {
            return Errors.Select(e => $"{e.Field}: {e.Message}").ToList();
        }
        
        public List<string> GetWarningMessages()
        {
            return Warnings.Select(w => $"{w.Field}: {w.Message}").ToList();
        }
    }
    
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
    
    public class ValidationWarning
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}