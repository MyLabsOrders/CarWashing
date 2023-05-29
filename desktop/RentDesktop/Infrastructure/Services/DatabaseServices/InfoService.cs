using RentDesktop.Models.DatabaseModels;
using RentDesktop.Models.Informing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class InfoService
    {
        public static List<string> GetAllStatuses()
        {
            return new List<string>()
            {
                UserInfo.ST_ACTIVE,
                UserInfo.ST_INACTIVE
            };
        }

        public static List<string> GetAllPositions()
        {
            return new List<string>()
            {
                UserInfo.POS_USER,
                UserInfo.POS_ADMIN
            };
        }

        public static List<string> GetAllGenders()
        {
            return new List<string>()
            {
                UserInfo.MALE,
                UserInfo.FEMALE
            };
        }

        public static List<IUser> GetAllUsers()
        {
            var allUsers = new List<IUser>();
            using var db = new DatabaseConnectionService();

            int currentPage = 1;
            IEnumerable<DatabaseUser> currentOrder;

            do
            {
                string allUsersHandle = $"/api/User?page={currentPage++}";
                using HttpResponseMessage allUsersResponse = db.GetAsync(allUsersHandle).Result;

                if (!allUsersResponse.IsSuccessStatusCode)
                    throw new ErrorResponseException(allUsersResponse);

                DatabaseUsers? usersCollection = allUsersResponse.Content.ReadFromJsonAsync<DatabaseUsers>().Result;

                if (usersCollection is null || usersCollection.users is null)
                    throw new IncorrectContentException(allUsersResponse.Content);

                string[] positions = usersCollection.users
                    .Select(t => GetUserIdentityInfo(t.id, db).role)
                    .ToArray();

                List<IUser> currentUsers = DatabaseModelConverterService.ConvertUsers(usersCollection, positions);

                foreach (IUser user in currentUsers)
                {
                    user.Login = GetUserIdentityInfo(user.ID, db).username;
                    user.Password = UserInfo.HIDDEN;
                }

                allUsers.AddRange(currentUsers);
                currentOrder = usersCollection.users;
            }
            while (currentOrder.Any());

            return allUsers;
        }

        public static bool CheckUserIsAdmin(string login, DatabaseConnectionService? db = null)
        {
            db ??= new DatabaseConnectionService();

            string adminCheckHandle = "/api/identity/authorize-admin";
            var content = new DatabaseUsername(login);

            using HttpResponseMessage adminCheckResponse = db.PostAsync(adminCheckHandle, content).Result;

            return adminCheckResponse.StatusCode == HttpStatusCode.OK;
        }

        public static DatabaseIdentityInfo GetUserIdentityInfo(string userId, DatabaseConnectionService? db = null)
        {
            db ??= new DatabaseConnectionService();

            string getIdentityInfoHandle = $"/api/identity/{userId}";
            using HttpResponseMessage getIdentityInfoResponse = db.GetAsync(getIdentityInfoHandle).Result;

            return getIdentityInfoResponse.Content.ReadFromJsonAsync<DatabaseIdentityInfo>().Result
                ?? throw new IncorrectContentException(getIdentityInfoResponse.Content);
        }
    }
}
