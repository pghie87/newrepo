using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmsalEWayBillSystem.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string TripNumber { get; set; }
        
        public DateTime TripDate { get; set; }
        
        [StringLength(20)]
        public string VehicleNumber { get; set; }
        
        [StringLength(15)]
        public string TransporterId { get; set; }
        
        [StringLength(100)]
        public string TransporterName { get; set; }
        
        [Required]
        [StringLength(200)]
        public string OriginAddress { get; set; }
        
        [Required]
        [StringLength(200)]
        public string DestinationAddress { get; set; }
        
        [Required]
        [StringLength(50)]
        public string OriginState { get; set; }
        
        [Required]
        [StringLength(50)]
        public string DestinationState { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal DistanceKm { get; set; }
        
        public DateTime? ExpectedDeliveryDate { get; set; }
        
        [StringLength(50)]
        public string DocumentType { get; set; }
        
        [StringLength(50)]
        public string DocumentNumber { get; set; }
        
        public DateTime? DocumentDate { get; set; }
        
        public virtual Invoice Invoice { get; set; }
    }
}