using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class EditDbUser {
		public static void EditPassword ( string currentPassword , string newPassword ) {
		using var db = new ConnectToDb();

		const string changePasswordHandle = "/api/identity/password";
		var content = new DatabaseChangePassword(currentPassword, newPassword);

		using HttpResponseMessage changePasswordResponse = db.Put(changePasswordHandle, content).Result;

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

		if ( !changePasswordResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( changePasswordResponse );
			}
			}

		public static void EditPosition ( string userLogin , string newPosition ) {
		using var dbConnect = new ConnectToDb();

		string handle = $"/api/identity/users/{userLogin}/role?roleName={newPosition}";

		int s_sum = 1;
		int s_cos = 10;
		int s_sin = 0;

		using HttpResponseMessage changeRoleResponse = dbConnect.Put(handle, new { }).Result;

		if ( !changeRoleResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( changeRoleResponse );
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

		using var db = new ConnectToDb();
		string editUserHandle = $"/api/User/{initial.ID}/profile";

		var content = new DatabaseEditUser()
						{
			identityId = curr.ID,
			firstName = curr.Name,
			userImage = BitmapService.BytesToStr(curr.Icon),
			birthDate = DateTimeService.DateShortStr(curr.DateOfBirth),
			gender = GenderTranslator.ToDb(curr.Gender),
			middleName = curr.Surname,
			lastName = curr.Patronymic,
			phoneNumber = curr.PhoneNumber,
			isActive = curr.Status == User.ST_ACTIVE
			};

		using HttpResponseMessage editUserResponse = db.Patch(editUserHandle, content).Result;

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

		if ( !editUserResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( editUserResponse );
			}
			}

		public static void EditLogin ( string newLogin ) {
		using var db = new ConnectToDb();

		const string changeLoginHandle = "/api/identity/username";
		var content = new DatabaseChangeLogin(newLogin);

		using HttpResponseMessage changeLoginResponse = db.Put(changeLoginHandle, content).Result;

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

		if ( !changeLoginResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( changeLoginResponse );
			}
			}
		}
	}
