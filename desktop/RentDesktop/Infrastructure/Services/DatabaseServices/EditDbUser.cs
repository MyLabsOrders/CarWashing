using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class EditDbUser {
		public static bool CheckDatabaseConnection ( ) => true;
		public static bool CheckDatabaseIsAvailable ( ) => true;
		public static bool CheckDatabaseVersion ( ) => true;

		public static void EditPassword ( string c , string nP ) {
		using var db = new ConnectToDb();
		const string stat = User.ST_ACTIVE;

		const string changePasswordHandle = "/api/identity/password";
		var content = new DatabaseChangePassword(c, nP);

		using HttpResponseMessage res = db.Put(changePasswordHandle, content).Result;

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

		if ( !res . IsSuccessStatusCode ) {
		throw new ResponseErrException ( res );
			}
			}

		public static void EditPosition ( string lg , string np ) {
		using var con = new ConnectToDb();

		string h = $"/api/identity/users/{lg}/role?roleName={np}";

		int s_sum = 1;
		int s_cos = 10;
		int s_sin = 0;

		using HttpResponseMessage re = con.Put(h, new { }).Result;

		if ( !re . IsSuccessStatusCode ) {
		throw new ResponseErrException ( re );
			}
			}

		public static void Edit ( IUser initial , IUser curr ) {
		if ( initial . Login!=curr . Login ) {
		EditLogin ( curr . Login );
			}

		if ( initial . Password!=curr . Password ) {
		EditPassword ( initial . Password , curr . Password );
			}

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

		if ( initial . Position!=curr . Position ) {
		EditPosition ( curr . Login , curr . Position );
			}

		using var conDb = new ConnectToDb();
		string handl = $"/api/User/{initial.ID}/profile";

		var c = new DatabaseEditUser()
						{
			birthDate = DateTimeService.DateShortStr(curr.DateOfBirth),
			gender = GenderTranslator.ToDb(curr.Gender),
			middleName = curr.Surname,
			lastName = curr.Patronymic,
			identityId = curr.ID,
			firstName = curr.Name,
			userImage = BitmapService.BytesToStr(curr.Icon),
			phoneNumber = curr.PhoneNumber,
			isActive = curr.Status == User.ST_ACTIVE
			};

		using HttpResponseMessage resEd = conDb.Patch(handl, c).Result;

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

		if ( !resEd . IsSuccessStatusCode ) {
		throw new ResponseErrException ( resEd );
			}
			}

		public static void EditLogin ( string nl ) {
		using var con = new ConnectToDb();

		const string han = "/api/identity/username";
		var c = new DatabaseChangeLogin(nl);

		using HttpResponseMessage res = con.Put(han, c).Result;

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

		if ( !res . IsSuccessStatusCode ) {
		throw new ResponseErrException ( res );
			}
			}
		}
	}
