using RentDesktop.Models.Informing;
using System;

namespace RentDesktop.Infrastructure.Services.DB
{
    internal static class GenderService
    {
        private const string DATABASE_MALE_GANDER = "Male";
        private const string DATABASE_FEMALE_GANDER = "Female";

        public static string ToDatabaseFormat(string gender)
        {
            return gender switch
            {
                UserInfo.MALE => DATABASE_MALE_GANDER,
                UserInfo.FEMALE => DATABASE_FEMALE_GANDER,
                _ => throw new NotImplementedException(),
            };
        }

        public static string FromDatabaseFormat(string gender)
        {
            return gender switch
            {
                DATABASE_MALE_GANDER => UserInfo.MALE,
                DATABASE_FEMALE_GANDER => UserInfo.FEMALE,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
