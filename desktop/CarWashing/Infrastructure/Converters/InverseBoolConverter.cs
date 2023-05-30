using Avalonia . Data . Converters;
using System;
using System . Globalization;

namespace CarWashing . Infrastructure . Converters {
	internal class InverseBoolConverter : IValueConverter {
		public object? Convert ( object? value , Type targetType , object? parameter , CultureInfo culture ) => targetType!=typeof ( bool )||value is not bool boolValue
						? throw new InvalidOperationException ( "Incorrect type." )
						: !boolValue;

		public object? ConvertBack ( object? value , Type targetType , object? parameter , CultureInfo culture ) => throw new NotSupportedException ( );
		}
	}
