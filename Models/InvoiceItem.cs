using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmsalEWayBillSystem.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        
        [Required]
        [StringLength(8)]
        public string HsnCode { get; set; }
        
        public decimal Quantity { get; set; }
        
        [StringLength(10)]
        public string Unit { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TaxableAmount { get; set; }
        
        public decimal CgstRate { get; set; }
        
        public decimal SgstRate { get; set; }
        
        public decimal IgstRate { get; set; }
        
        public decimal CgstAmount { get; set; }
        
        public decimal SgstAmount { get; set; }
        
        public decimal IgstAmount { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}