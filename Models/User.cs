using System;
using System.ComponentModel.DataAnnotations;

namespace EmsalEWayBillSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        public bool IsActive { get; set; }
        
        [StringLength(50)]
        public string Role { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime LastLogin { get; set; }
    }
}