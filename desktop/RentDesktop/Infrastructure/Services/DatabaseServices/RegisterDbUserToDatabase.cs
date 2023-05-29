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

		using HttpResponseMessage profileResponse = db.Post(profileHandle, content).Result;

		if ( !profileResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( profileResponse );
			}
			}

		public static void Register ( IUser user ) {
		using var db = new ConnectToDb();

		const string registerHandle = "/api/identity/user/register";
		var content = new DatabaseRegister(user.Login, user.Password, user.Position);

		using HttpResponseMessage registerResponse = db.Post(registerHandle, content).Result;

		if ( !registerResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( registerResponse );
			}

		DatabaseLoginResponseContent loginContent = LoginToDatabase.LogToSystem(user.Login, user.Password, db);

		db . SetAuth ( loginContent . token );
		user . ID=loginContent . userId;

		UpdateInfo ( user , db );
			}
		}
	}
