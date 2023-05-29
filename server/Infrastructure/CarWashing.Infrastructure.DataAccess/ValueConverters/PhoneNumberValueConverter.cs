using CarWashing.Domain.Core.ValueObject;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarWashing.Infrastructure.DataAccess.ValueConverters;

public class PhoneNumberValueConverter : ValueConverter<PhoneNumber, string> {
    public PhoneNumberValueConverter()
        : base(x => x.Value, x => new PhoneNumber(x)) { }
}