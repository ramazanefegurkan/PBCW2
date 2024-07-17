using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBW2.Data.Configuration
{
    public class ProducttConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Category).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.Stock).IsRequired(true);
        }
    }
}
