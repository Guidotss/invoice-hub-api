using System;
using Microsoft.EntityFrameworkCore;
using Invoce_Hub.Models;

namespace Invoce_Hub.Data
{
    public class InvoiceHubDbContext : DbContext
    {
        public InvoiceHubDbContext(DbContextOptions<InvoiceHubDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<DocumentExtraction> DocumentExtractions => Set<DocumentExtraction>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<Tax> Taxes => Set<Tax>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasOne(u => u.Tenant)
                      .WithMany(t => t.Users)
                      .HasForeignKey(u => u.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasIndex(t => t.Name).IsUnique();

                entity.HasOne(t => t.Tax)
                      .WithMany()
                      .HasForeignKey(t => t.TaxId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(t => t.Documents)
                      .WithOne(d => d.Tenant)
                      .HasForeignKey(d => d.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasIndex(t => t.TaxType).IsUnique();
            });

            
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasIndex(s => s.Name).IsUnique();
            });


            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasOne(d => d.Tenant)
                      .WithMany(t => t.Documents)
                      .HasForeignKey(d => d.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Uploader)
                      .WithMany()
                      .HasForeignKey(d => d.UploaderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Status)
                      .WithMany()
                      .HasForeignKey(d => d.StatusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<DocumentExtraction>(entity =>
            {
                entity.HasOne(e => e.Document)
                      .WithMany(d => d.Extractions)
                      .HasForeignKey(e => e.DocumentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.RawOcr).HasColumnType("jsonb");
                entity.Property(e => e.Parsed).HasColumnType("jsonb");
                entity.Property(e => e.Validation).HasColumnType("jsonb");
            });
        }
    }
}
