using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.DAL.Context.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(25);
            
            builder.Property(p=> p.Description)
                .HasMaxLength(300);

            builder.Property(p => p.Price)
                .IsRequired();
            
            builder.Property(p=> p.CategoryId)
                .IsRequired();

        }
    }
}
