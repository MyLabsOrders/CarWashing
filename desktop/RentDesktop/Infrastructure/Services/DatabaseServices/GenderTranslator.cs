using RentDesktop.Models.Informing;
using System;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class GenderTranslator
    {
        public static string FromDb(string gender) => gender switch
        {
            MALE => UserInfo.MALE,
            FEMALE => UserInfo.FEMALE,
            _ => throw new NotImplementedException(),
        };

        private const string MALE = "Male";

        public static string ToDb(string gender) => gender switch
        {
            UserInfo.MALE => MALE,
            UserInfo.FEMALE => FEMALE,
            _ => throw new NotImplementedException(),
        };

        private const string FEMALE = "Female";
    }
}
