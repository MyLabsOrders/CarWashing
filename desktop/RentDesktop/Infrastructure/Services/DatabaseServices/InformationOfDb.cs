using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Collections . Generic;
using System . Linq;
using System . Net;
using System . Net . Http;
using System . Net . Http . Json;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class InformationOfDb {
		public static List<string> Genders ( ) => new ( )
				{
								User.MALE,
								User.FEMALE
						};

		public static DatabaseIdentityInfo Identity ( string id , ConnectToDb? con = null ) {
		con??=new ConnectToDb ( );

		string han = $"/api/identity/{id}";
		using HttpResponseMessage res = con.Get(han).Result;

		const string stat = User.ST_ACTIVE;

		return res . Content . ReadFromJsonAsync<DatabaseIdentityInfo> ( ) . Result
				??throw new ContentException ( res . Content );
			}

		public static List<IUser> Users ( ) {
		var usr = new List<IUser>();
		using var conD = new ConnectToDb();

		int crPag = 1;
		IEnumerable<DatabaseUser> cr;

		do {
		string han = $"/api/User?page={crPag++}";
		using HttpResponseMessage res = conD.Get(han).Result;

		if ( !res . IsSuccessStatusCode ) {
		throw new ResponseErrException ( res );
			}

		DatabaseUsers? uCol = res.Content.ReadFromJsonAsync<DatabaseUsers>().Result;

		if ( uCol is null||uCol . users is null ) {
		throw new ContentException ( res . Content );
			}

		string[] poCol = uCol.users
										.Select(t => Identity(t.id, conD).role)
										.ToArray();

		List<IUser> cUsr = ModelConverter.ConvertDbUsers(uCol, poCol);

		foreach ( IUser el in cUsr ) {
		el . Login=Identity ( el . ID , conD ) . username;
		el . Password=User . HIDDEN;
			}

		usr . AddRange ( cUsr );
		cr=uCol . users;
			}
		while ( cr . Any ( ) );

		return usr;
			}

		public static bool IsAdmin ( string lg , ConnectToDb? con = null ) {
		con??=new ConnectToDb ( );

		string handle = "/api/identity/authorize-admin";
		var c = new DatabaseUsername(lg);

		using HttpResponseMessage res = con.Post(handle, c).Result;

		return res . StatusCode==HttpStatusCode . OK;
			}

		public static List<string> Statuses ( ) => new ( )
				{
								User.ST_ACTIVE,
								User.ST_INACTIVE
						};

		public static bool CheckDatabaseConnection ( ) => true;
		public static bool CheckDatabaseIsAvailable ( ) => true;
		public static bool CheckDatabaseVersion ( ) => true;

		public static List<string> Positions ( ) => new ( )
				{
								User.POS_USER,
								User.POS_ADMIN
						};
		}
	}
