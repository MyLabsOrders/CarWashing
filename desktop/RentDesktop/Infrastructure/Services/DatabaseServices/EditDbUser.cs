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

		if ( !changePasswordResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( changePasswordResponse );
			}
			}

		public static void EditPosition ( string userLogin , string newPosition ) {
		using var db = new ConnectToDb();

		string changeRoleHandle = $"/api/identity/users/{userLogin}/role?roleName={newPosition}";
		var content = new { };

		using HttpResponseMessage changeRoleResponse = db.Put(changeRoleHandle, content).Result;

		if ( !changeRoleResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( changeRoleResponse );
			}
			}

		public static void Edit ( IUser initial , IUser curr ) {
		if ( initial . Login!=curr . Login ) {
		EditLogin ( curr . Login );
			}

		if ( initial . Password!=curr . Password ) {
		EditPassword ( initial . Password , curr . Password );
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
			middleName = curr.Surname,
			lastName = curr.Patronymic,
			phoneNumber = curr.PhoneNumber,
			userImage = BitmapService.BytesToStr(curr.Icon),
			birthDate = DateTimeService.ShortDateTimeToString(curr.DateOfBirth),
			gender = GenderTranslator.ToDb(curr.Gender),
			isActive = curr.Status == UserInfo.ST_ACTIVE
			};

		using HttpResponseMessage editUserResponse = db.Patch(editUserHandle, content).Result;

		if ( !editUserResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( editUserResponse );
			}
			}

		public static void EditLogin ( string newLogin ) {
		using var db = new ConnectToDb();

		const string changeLoginHandle = "/api/identity/username";
		var content = new DatabaseChangeLogin(newLogin);

		using HttpResponseMessage changeLoginResponse = db.Put(changeLoginHandle, content).Result;

		if ( !changeLoginResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( changeLoginResponse );
			}
			}
		}
	}
