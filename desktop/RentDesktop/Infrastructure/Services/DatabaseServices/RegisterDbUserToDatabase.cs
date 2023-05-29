using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class RegisterDbUserToDatabase {
		private static void UpdateInfo ( IUser user , ConnectToDb db ) {
		string profileHandle = $"/api/User/{user.ID}/profile";

		var content = new DatabaseUserProfile()
						{
			firstName = user.Name,
			middleName = user.Surname,
			lastName = user.Patronymic,
			phoneNumber = user.PhoneNumber,
			userImage = BitmapService.BytesToStr(user.Icon),
			birthDate = DateTimeService.ShortDateTimeToString(user.DateOfBirth),
			gender = GenderTranslator.ToDb(user.Gender)
			};

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

		using HttpResponseMessage profileResponse = db.Post(profileHandle, content).Result;

		if ( !profileResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( profileResponse );
			}
			}

		public static void Register ( IUser user ) {
		using var db = new ConnectToDb();

		const string registerHandle = "/api/identity/user/register";
		var content = new DatabaseRegister(user.Login, user.Password, user.Position);

		using HttpResponseMessage registerResponse = db.Post(registerHandle, content).Result;

		if ( !registerResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( registerResponse );
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

		DatabaseLoginResponseContent loginContent = LoginToDatabase.LogToSystem(user.Login, user.Password, db);

		db . SetAuth ( loginContent . token );
		user . ID=loginContent . userId;

		UpdateInfo ( user , db );
			}
		}
	}
