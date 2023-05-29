using RentDesktop.Models.DatabaseModels;
using RentDesktop.Models.Informing;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class LoginToDatabase
    {
        private static IUser UserInfoGetting(ConnectToDb db, string userId, string login, string password)
        {
            string profileHandle = $"/api/User/{userId}";
            using HttpResponseMessage profileResponse = db.Get(profileHandle).Result;

            if (!profileResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(profileResponse);

            DatabaseUser? profileContent = profileResponse.Content.ReadFromJsonAsync<DatabaseUser>().Result
                ?? throw new IncorrectContentException(profileResponse.Content);

            string position = InformationOfDb.IsAdmin(login, db)
                ? UserInfo.POS_ADMIN
                : UserInfo.POS_USER;

            UserInfo userInfo = ModelConverter.ConvertDbUser(profileContent, position);
            userInfo.Login = login;
            userInfo.Password = password;

            return userInfo;
        }

        public static IUser TryLogin(string login, string password)
        {
            ConnectToDb db = new();
            DatabaseLoginResponseContent loginContent = LogToSystem(login, password, db, true);

            return UserInfoGetting(db, loginContent.userId, login, password);
        }

        public static DatabaseLoginResponseContent LogToSystem(string login, string password, ConnectToDb? db = null,
            bool registerAuthorizationToken = false)
        {
            db ??= new ConnectToDb();

            const string loginHandle = "/api/identity/login";
            var content = new DatabaseLogin(login, password);

            using HttpResponseMessage loginResponse = db.Post(loginHandle, content).Result;

            if (!loginResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(loginResponse);

            DatabaseLoginResponseContent loginContent = loginResponse.Content.ReadFromJsonAsync<DatabaseLoginResponseContent>().Result
                ?? throw new IncorrectContentException(loginResponse.Content);

            if (registerAuthorizationToken)
            {
                ConnectToDb.AuthorizationToken = loginContent.token;
                db.SetAuth(loginContent.token);
            }

            return loginContent;
        }
    }
}
