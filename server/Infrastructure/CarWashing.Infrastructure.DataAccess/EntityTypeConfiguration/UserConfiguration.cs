using CarWashing.Domain.Core.Users;
using CarWashing.Infrastructure.DataAccess.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWashing.Infrastructure.DataAccess.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LastName);
        builder.Property(x => x.FirstName);
        builder.Property(x => x.MiddleName);
        builder.Property(x => x.BirthDate);
        builder.Property(x => x.Money);
        builder.Property(x => x.Image);
        builder.Property(x => x.PhoneNumber).HasConversion<PhoneNumberValueConverter>();
    }
}
