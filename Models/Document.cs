namespace Invoce_Hub.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class Document
{
        public Guid Id { get; set; } = Guid.NewGuid();
    
        [Required, MaxLength(200)]
        public string FileName { get; set; } = string.Empty;
    
        [Required, MaxLength(50)]
        public string ContentType { get; set; } = "application/pdf";

        public long FileSize { get; set; }
        public int Pages { get; set; }
    
        [Required]
        public string StorageUrl { get; set; } = string.Empty;
    
        [MaxLength(50)]
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
        
        public float AdavancedPercentage { get; set; } = 0.0f;
    
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExtractedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        
        public Guid TenantId { get; set; }
        public Tenant? Tenant { get; set; }
    
        public Guid UploaderId { get; set; }
        public User? Uploader { get; set; }
    
        public ICollection<DocumentExtraction> Extractions { get; set; } = new List<DocumentExtraction>(); 
}

