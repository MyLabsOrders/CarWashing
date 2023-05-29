namespace TechRental.Domain.Core.Abstractions;

public class Image {
    public Image(string image) {
        Value = image;
    }

    public string Value { get; }
}