using Avalonia . Media . Imaging;
using System;
using System . IO;

namespace RentDesktop . Infrastructure . Services {
	internal static class BitmapService {
		public static bool TryStrToBytes ( string text , out byte [ ] bytes ) {
		try {
		bytes=StrToBytes ( text );
		return true;
			} catch {
		bytes=Array . Empty<byte> ( );
		return false;
			}
			}

		public static byte [ ] StrToBytes ( string base64string ) => Convert . FromBase64String ( base64string );

		public static bool TryBytesToBmp ( byte [ ] bytes , out Bitmap? bmp ) {
		try {
		bmp=BytesToBmp ( bytes );
		return true;
			} catch {
		bmp=null;
		return false;
			}
			}

		public static byte [ ] BmpToBytes ( Bitmap bmp ) {
		byte[] bytes;

		using ( var ms = new MemoryStream ( ) ) {
		bmp . Save ( ms );
		bytes=ms . GetBuffer ( );
			}

		return bytes;
			}

		public static Bitmap BytesToBmp ( byte [ ] bytes ) {
		Bitmap bitmap;

		using ( var ms = new MemoryStream ( bytes ) ) {
		bitmap=new Bitmap ( ms );
			}

		return bitmap;
			}

		public static string BytesToStr ( byte [ ] bytes ) => Convert . ToBase64String ( bytes );

		public static bool TryBmpToBytes ( Bitmap bmp , out byte [ ] bytes ) {
		try {
		bytes=BmpToBytes ( bmp );
		return true;
			} catch {
		bytes=Array . Empty<byte> ( );
		return false;
			}
			}
		}
	}
