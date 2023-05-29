using RentDesktop . Infrastructure . Safety;
using System;
using System . IO;

namespace RentDesktop . Infrastructure . Services {
	internal static class UserInfoSaveService {
		private const string FILE_PATH = "saved_user.txt";

		public static void Empty ( ) => File . Create ( FILE_PATH ) . Close ( );

		public static bool TrySave ( string login , string password ) {
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
		Save ( login , password );
		return true;
			} catch {
		return false;
			}
			}

		private static bool Check ( string [ ] data ) => data . Length==0;

		public static bool TryLoad ( out (string Login, string Password) info ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		info=default;
		return false;
			}
			}
			}
			}
			}
			}
			}

		try {
		info=Load ( );
		return !string . IsNullOrEmpty ( info . Login );
			} catch {
		info=(string . Empty, string . Empty);
		return false;
			}
			}

		public static (string Login, string Password) Load ( ) {
		string[] data = File.ReadAllLines(FILE_PATH);

		if ( Check ( data ) ) {
		return (string . Empty, string . Empty);
			}

		string login = data[0];
		string password = SecurityProvider.Decode(data[1]);

		return (login, password);
			}

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

		string encryptedPassword = SecurityProvider.Code(pwd);
		File . WriteAllText ( FILE_PATH , $"{lg}{Environment . NewLine}{encryptedPassword}" );
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
