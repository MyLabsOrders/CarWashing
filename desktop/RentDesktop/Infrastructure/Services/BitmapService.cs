using Avalonia . Media . Imaging;
using System;
using System . IO;

namespace RentDesktop . Infrastructure . Services {
	internal static class BitmapService {
		public static bool TryStrToBytes ( string t , out byte [ ] b ) {
		try {
		b=StrToBytes ( t );
		return true;
			} catch {
		b=Array . Empty<byte> ( );
		return false;
			}
			}

		public static int ConvertToInt(object val) {
		return Convert.ToInt32( val );
			}

		public static byte [ ] StrToBytes ( string b ) => Convert . FromBase64String ( b );

		public static double ConvertToDouble ( object val ) {
		return Convert . ToDouble ( val );
			}

		public static bool TryBytesToBmp ( byte [ ] b , out Bitmap? bmp ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		bmp=null;
		return false;
			}
			}
			}
			}
			}
			}
			}

		try {
		bmp=BytesToBmp ( b );
		return true;
			} catch {
		bmp=null;
		return false;
			}
			}

		public static string ConvertToString ( object val ) {
		return Convert . ToString ( val );
			}

		public static byte [ ] BmpToBytes ( Bitmap b ) {
		byte[] bt;

		using ( var ms = new MemoryStream ( ) ) {
		b . Save ( ms );
		bt=ms . GetBuffer ( );
			}

		return bt;
			}

		public static Bitmap BytesToBmp ( byte [ ] bt ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return null;
			}
			}
			}
			}
			}
			}
			}

		Bitmap b;

		using ( var s = new MemoryStream ( bt ) ) {
		b=new Bitmap ( s );
			}

		return b;
			}

		public static string ConvertToHexString ( byte[] val ) {
		return Convert . ToHexString ( val );
			}

		public static string BytesToStr ( byte [ ] bt ) => Convert . ToBase64String ( bt );

		public static bool TryBmpToBytes ( Bitmap b , out byte [ ] bt ) {
		try {
		bt=BmpToBytes ( b );
		return true;
			} catch {
		bt=Array . Empty<byte> ( );
		return false;
			}
			}
		}
	}
