using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechRental.Domain.Core.Abstractions;

namespace TechRental.Infrastructure.DataAccess.ValueConverters;

public class ImageValueConverter : ValueConverter<Image, string> {
    public ImageValueConverter()
        : base(
            x => x.Value,
            x => new Image(x)) {
    }
}