using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace EmsalEWayBillSystem.Models
{
    public class EWayBill
    {
        [Key]
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
        
        [Required]
        [StringLength(12)]
        public string EwayBillNumber { get; set; }
        
        [Required]
        public DateTime GeneratedDate { get; set; }
        
        [Required]
        public DateTime ValidFrom { get; set; }
        
        [Required]
        public DateTime ValidUntil { get; set; }
        
        [Required]
        public EWayBillStatus Status { get; set; }
        
        [StringLength(15)]
        public string TransporterId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string VehicleNumber { get; set; }
        
        [Required]
        [StringLength(15)]
        public string FromGstin { get; set; }
        
        [Required]
        [StringLength(15)]
        public string ToGstin { get; set; }
        
        [Column(TypeName = "jsonb")]
        public string JsonPayload { get; set; }
        
        [Column(TypeName = "jsonb")]
        public string JsonResponse { get; set; }
        
        public int CreatedById { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        
        // Helper methods
        public bool IsActive()
        {
            return Status == EWayBillStatus.ACTIVE || Status == EWayBillStatus.EXTENDED;
        }
        
        public bool IsExpired()
        {
            return Status == EWayBillStatus.EXPIRED || 
                   (IsActive() && DateTime.UtcNow > ValidUntil);
        }
        
        public bool CanExtend()
        {
            // Can extend if active and not already extended more than the maximum allowed times
            // Business rule: Can extend only if within 4 hours of expiry or up to 8 hours after expiry
            if (!IsActive())
                return false;
                
            TimeSpan remainingValidity = ValidUntil - DateTime.UtcNow;
            TimeSpan expiredTime = DateTime.UtcNow - ValidUntil;
            
            return (remainingValidity.TotalHours <= 4 && remainingValidity.TotalHours > 0) || 
                   (expiredTime.TotalHours <= 8 && expiredTime.TotalHours > 0);
        }
        
        public bool CanCancel()
        {
            // Business rule: Can cancel only within 24 hours of generation and if still active
            if (!IsActive())
                return false;
                
            TimeSpan timeSinceGeneration = DateTime.UtcNow - GeneratedDate;
            return timeSinceGeneration.TotalHours <= 24;
        }
        
        public TimeSpan GetRemainingValidity()
        {
            if (!IsActive() || IsExpired())
                return TimeSpan.Zero;
                
            return ValidUntil - DateTime.UtcNow;
        }
        
        // Serialization helpers
        public Dictionary<string, object> GetJsonPayloadAsDictionary()
        {
            if (string.IsNullOrEmpty(JsonPayload))
                return new Dictionary<string, object>();
                
            return JsonSerializer.Deserialize<Dictionary<string, object>>(JsonPayload);
        }
        
        public Dictionary<string, object> GetJsonResponseAsDictionary()
        {
            if (string.IsNullOrEmpty(JsonResponse))
                return new Dictionary<string, object>();
                
            return JsonSerializer.Deserialize<Dictionary<string, object>>(JsonResponse);
        }
        
        public void SetJsonPayload(Dictionary<string, object> payload)
        {
            JsonPayload = JsonSerializer.Serialize(payload);
        }
        
        public void SetJsonResponse(Dictionary<string, object> response)
        {
            JsonResponse = JsonSerializer.Serialize(response);
        }
    }
}