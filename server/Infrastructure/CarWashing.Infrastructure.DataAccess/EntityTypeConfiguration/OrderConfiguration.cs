using CarWashing.Domain.Core.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWashing.Infrastructure.DataAccess.EntityTypeConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order> {
    public void Configure(EntityTypeBuilder<Order> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderDate).IsRequired(false);
        builder.Property(x => x.Price);
        builder.Property(x => x.Name);
        builder.Property(x => x.Company);
        builder.Property(x => x.Image);
        builder.Property(x => x.Amount);
        builder.Property(x => x.Period);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .IsRequired(false);
    }
}
