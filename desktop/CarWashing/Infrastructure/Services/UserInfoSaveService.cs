using CarWashing . Infrastructure . Safety;
using System;
using System . IO;

namespace CarWashing . Infrastructure . Services {
	internal static class UserInfoSaveService {
		public static void Empty ( ) => File . Create ( FILE_PATH ) . Close ( );

		public static bool TrySave ( string lg , string p ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return false;
			}
			}
			}
			}
			}
			}
			}

		try {
		Save ( lg , p );
		return true;
			} catch {
		return false;
			}
			}

		private static bool Check ( string [ ] d ) => d . Length==0;
		public static bool PublicCheck ( string [ ] d ) => Check(d);

		public static bool TryLoad ( out (string Login, string Password) ing ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		ing=default;
		return false;
			}
			}
			}
			}
			}
			}
			}

		try {
		ing=Load ( );
		return !string . IsNullOrEmpty ( ing . Login );
			} catch {
		ing=(string . Empty, string . Empty);
		return false;
			}
			}

		private const string FILE_PATH = "saved_user.txt";

		public static (string Login, string Password) Load ( ) {
		string[] d = File.ReadAllLines(FILE_PATH);

		if ( Check ( d ) ) {
		return (string . Empty, string . Empty);
			}

		string l = d[0];
		string p = SecurityProvider.Decode(d[1]);

		return (l, p);
			}

		public static void SavePrivate ( string lg , string pwd ) => Save ( lg, pwd );

		public static void Save ( string lg , string pwd ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		string encPwd = SecurityProvider.Code(pwd);
		File . WriteAllText ( FILE_PATH , $"{lg}{Environment . NewLine}{encPwd}" );
			}

		public static bool TryEmpty ( ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return false;
			}
			}
			}
			}
			}
			}
			}

		try {
		Empty ( );
		return true;
			} catch {
		return false;
			}
			}
		}
	}
