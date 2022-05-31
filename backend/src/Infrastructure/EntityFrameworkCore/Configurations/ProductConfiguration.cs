using Domain.Catalog.Entities;
using Domain.Catalog.Mementos;
using Domain.Catalog.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<ProductMemento>
{
    public void Configure(EntityTypeBuilder<ProductMemento> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(ProductName.MaximumLength);

        builder.Property(product => product.Price)
            .IsRequired();

        builder.Property(product => product.Stock)
            .IsRequired();
    }
}