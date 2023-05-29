using RentDesktop.Models.DB;
using RentDesktop.Models.Informing;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentDesktop.Infrastructure.Services.DB
{
    internal static class LoginService
    {
        public static IUser Login(string login, string password)
        {
            DatabaseConnectionService db = new();
            DatabaseLoginResponseContent loginContent = EnterSystem(login, password, db, true);

            return GetUserInfo(db, loginContent.userId, login, password);
        }

        public static DatabaseLoginResponseContent EnterSystem(string login, string password, DatabaseConnectionService? db = null,
            bool registerAuthorizationToken = false)
        {
            db ??= new DatabaseConnectionService();

            const string loginHandle = "/api/identity/login";
            var content = new DatabaseLogin(login, password);

            using HttpResponseMessage loginResponse = db.PostAsync(loginHandle, content).Result;

            if (!loginResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(loginResponse);

            DatabaseLoginResponseContent loginContent = loginResponse.Content.ReadFromJsonAsync<DatabaseLoginResponseContent>().Result
                ?? throw new IncorrectContentException(loginResponse.Content);

            if (registerAuthorizationToken)
            {
                DatabaseConnectionService.AuthorizationToken = loginContent.token;
                db.SetAuthorizationToken(loginContent.token);
            }

            return loginContent;
        }

        private static IUser GetUserInfo(DatabaseConnectionService db, string userId, string login, string password)
        {
            string profileHandle = $"/api/User/{userId}";
            using HttpResponseMessage profileResponse = db.GetAsync(profileHandle).Result;

            if (!profileResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(profileResponse);

            DatabaseUser? profileContent = profileResponse.Content.ReadFromJsonAsync<DatabaseUser>().Result
                ?? throw new IncorrectContentException(profileResponse.Content);

            string position = InfoService.CheckUserIsAdmin(login, db)
                ? UserInfo.POS_ADMIN
                : UserInfo.POS_USER;

            UserInfo userInfo = DatabaseModelConverterService.ConvertUser(profileContent, position);
            userInfo.Login = login;
            userInfo.Password = password;

            return userInfo;
        }
    }
}
