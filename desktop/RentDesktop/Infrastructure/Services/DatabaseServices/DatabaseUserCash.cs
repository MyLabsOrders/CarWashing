using RentDesktop.Models;
using RentDesktop.Models.DatabaseModels;
using RentDesktop.Models.Informing;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class DatabaseUserCash
    {
        public static double CheckBalace(IUser userInfo)
        {
            using var db = new ConnectToDb();

            string getUserHandle = $"/api/User/{userInfo.ID}";
            using HttpResponseMessage getUserResponse = db.Get(getUserHandle).Result;

            DatabaseUser dbUser = getUserResponse.Content.ReadFromJsonAsync<DatabaseUser>().Result
                ?? throw new IncorrectContentException(getUserResponse.Content);

            return dbUser.money;
        }

        public static bool CanPay(IEnumerable<ProductRentModel> cart, IUser userInfo)
        {
            double price = cart.Sum(t => t.TotalPrice);
            return userInfo.Money >= price;
        }

        public static void IncreaseMoney(IUser userInfo, double sum, bool logIn = false)
        {
            using var db = new ConnectToDb();

            if (logIn)
            {
                DatabaseLoginResponseContent loginContent = LoginToDatabase.LogToSystem(userInfo.Login, userInfo.Password, db);
                db.SetAuth(loginContent.token);
            }

            string addCashHandle = $"/api/User/{userInfo.ID}/account";
            var content = new DatabaseCash(sum);

            using HttpResponseMessage addCashResponse = db.Put(addCashHandle, content).Result;

            if (!addCashResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(addCashResponse);
        }
    }
}
