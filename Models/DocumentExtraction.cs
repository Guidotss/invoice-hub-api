namespace Invoce_Hub.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class DocumentExtraction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid DocumentId { get; set; }
    public Document? Document { get; set; }

    [Column(TypeName = "jsonb")]
    public string RawOcr { get; set; } = "{}";

    [Column(TypeName = "jsonb")]
    public string Parsed { get; set; } = "{}";

    [Column(TypeName = "jsonb")]
    public string Validation { get; set; } = "{}";

    public DateTime ExtractedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
