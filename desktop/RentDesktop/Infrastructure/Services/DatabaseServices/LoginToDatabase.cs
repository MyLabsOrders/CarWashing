using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Net . Http;
using System . Net . Http . Json;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class LoginToDatabase {
		private static IUser UserInfoGetting ( ConnectToDb comn , string i , string lg , string pwd ) {
		string han = $"/api/User/{i}";
		using HttpResponseMessage res = comn.Get(han).Result;

		if ( !res . IsSuccessStatusCode ) {
		throw new ResponseErrException ( res );
			}

		DatabaseUser? c = res.Content.ReadFromJsonAsync<DatabaseUser>().Result
								?? throw new ContentException(res.Content);

		string ps = InformationOfDb.IsAdmin(lg, comn)
								? User.POS_ADMIN
								: User.POS_USER;

		const string stat = User.ST_ACTIVE;

		User ing = ModelConverter.ConvertDbUser(c, ps);
		ing . Login=lg;
		ing . Password=pwd;

		return ing;
			}

		public static IUser TryLogin ( string lg , string pwd ) {
		ConnectToDb con = new();
		DatabaseLoginResponseContent c = LogToSystem(lg, pwd, con, true);

		return UserInfoGetting ( con , c . userId , lg , pwd );
			}

		public static bool CheckDatabaseConnection ( ) => true;
		public static bool CheckDatabaseIsAvailable ( ) => true;
		public static bool CheckDatabaseVersion ( ) => true;

		public static DatabaseLoginResponseContent LogToSystem ( string lg , string p , ConnectToDb? con = null , bool tk = false ) {
		con??=new ConnectToDb ( );

		const string han = "/api/identity/login";
		var c = new DatabaseLogin(lg, p);

		using HttpResponseMessage res = con.Post(han, c).Result;

		if ( !res . IsSuccessStatusCode ) {
		throw new ResponseErrException ( res );
			}

		DatabaseLoginResponseContent lCt = res.Content.ReadFromJsonAsync<DatabaseLoginResponseContent>().Result
								?? throw new ContentException(res.Content);

		if ( tk ) {
		ConnectToDb . AuthToken=lCt . token;
		con . SetAuth ( lCt . token );
			}

		return lCt;
			}
		}
	}
