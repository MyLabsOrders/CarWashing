using TechRental.Domain.Core.ValueObject;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TechRental.Infrastructure.DataAccess.ValueConverters;

public class PhoneNumberValueConverter : ValueConverter<PhoneNumber, string> {
    public PhoneNumberValueConverter()
        : base(x => x.Value, x => new PhoneNumber(x)) { }
}