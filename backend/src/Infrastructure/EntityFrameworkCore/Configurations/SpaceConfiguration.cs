using Domain.Spaces.Entities;
using Domain.Spaces.Mementos;
using Domain.Spaces.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Configurations;

internal sealed class SpaceConfiguration : IEntityTypeConfiguration<SpaceMemento>
{
    public void Configure(EntityTypeBuilder<SpaceMemento> builder)
    {
        builder.ToTable(nameof(Space));
        builder.HasKey(space => space.Id);

        builder.Property(space => space.Name)
            .IsRequired()
            .HasMaxLength(SpaceName.MaximumLength);
    }
}