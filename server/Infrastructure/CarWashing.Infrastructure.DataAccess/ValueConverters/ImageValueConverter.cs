using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CarWashing.Domain.Core.Abstractions;

namespace CarWashing.Infrastructure.DataAccess.ValueConverters;

public class ImageValueConverter : ValueConverter<Image, string> {
    public ImageValueConverter()
        : base(
            x => x.Value,
            x => new Image(x)) {
    }
}