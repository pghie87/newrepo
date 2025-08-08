using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmsalEWayBillSystem.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }
        
        public int TripId { get; set; }
        
        [StringLength(15)]
        public string FromGstin { get; set; }
        
        [StringLength(15)]
        public string ToGstin { get; set; }
        
        [StringLength(200)]
        public string FromAddress { get; set; }
        
        [StringLength(200)]
        public string ToAddress { get; set; }
        
        [StringLength(50)]
        public string FromStateCode { get; set; }
        
        [StringLength(50)]
        public string ToStateCode { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxableAmount { get; set; }
        
        public decimal CgstAmount { get; set; }
        
        public decimal SgstAmount { get; set; }
        
        public decimal IgstAmount { get; set; }
        
        public int? EwayBillId { get; set; }
        
        public string EwayBillNumber { get; set; }
        
        public virtual ICollection<InvoiceItem> Items { get; set; }
        
        public virtual Trip Trip { get; set; }
        
        public virtual EWayBill EwayBill { get; set; }
    }
}