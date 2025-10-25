namespace Invoce_Hub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    
    [Required, MaxLength(250)]
    public string PasswordHash { get; set; } = string.Empty;
    
    public Guid TenantId { get; set; }
    public Tenant? Tenant { get; set; }
   
   public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
   public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}