using RentDesktop.Models.DB;
using RentDesktop.Models.Informing;
using System.Net.Http;

namespace RentDesktop.Infrastructure.Services.DB
{
    internal static class UserRegisterService
    {
        public static void RegisterUser(IUser userInfo)
        {
            using var db = new DatabaseConnectionService();

            const string registerHandle = "/api/identity/user/register";
            var content = new DatabaseRegister(userInfo.Login, userInfo.Password, userInfo.Position);

            using HttpResponseMessage registerResponse = db.PostAsync(registerHandle, content).Result;

            if (!registerResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(registerResponse);

            DatabaseLoginResponseContent loginContent = LoginService.EnterSystem(userInfo.Login, userInfo.Password, db);

            db.SetAuthorizationToken(loginContent.token);
            userInfo.ID = loginContent.userId;

            SetUserInfo(userInfo, db);
        }

        private static void SetUserInfo(IUser userInfo, DatabaseConnectionService db)
        {
            string profileHandle = $"/api/User/{userInfo.ID}/profile";

            var content = new DatabaseUserProfile()
            {
                firstName = userInfo.Name,
                middleName = userInfo.Surname,
                lastName = userInfo.Patronymic,
                phoneNumber = userInfo.PhoneNumber,
                userImage = BitmapService.BytesToString(userInfo.Icon),
                birthDate = DateTimeService.ShortDateTimeToString(userInfo.DateOfBirth),
                gender = GenderService.ToDatabaseFormat(userInfo.Gender)
            };

            using HttpResponseMessage profileResponse = db.PostAsync(profileHandle, content).Result;

            if (!profileResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(profileResponse);
        }
    }
}
