using RentDesktop.Infrastructure.Safety;
using System;
using System.IO;

namespace RentDesktop.Infrastructure.Services
{
    internal static class UserInfoSaveService
    {
        private const string FILE_PATH = "saved_user.txt";

        public static void Empty()
        {
            File.Create(FILE_PATH).Close();
        }

        public static bool TrySave(string login, string password)
        {
            try
            {
                Save(login, password);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool Check(string[] data)
        {
            return data.Length == 0;
        }

        public static bool TryLoad(out (string Login, string Password) info)
        {
            try
            {
                info = Load();
                return !string.IsNullOrEmpty(info.Login);
            }
            catch
            {
                info = (string.Empty, string.Empty);
                return false;
            }
        }

        public static (string Login, string Password) Load()
        {
            string[] data = File.ReadAllLines(FILE_PATH);

            if (Check(data))
                return (string.Empty, string.Empty);

            string login = data[0];
            string password = SecurityProvider.Decode(data[1]);

            return (login, password);
        }

        public static void Save(string lg, string pwd)
        {
            string encryptedPassword = SecurityProvider.Code(pwd);
            File.WriteAllText(FILE_PATH, $"{lg}{Environment.NewLine}{encryptedPassword}");
        }

        public static bool TryEmpty()
        {
            try
            {
                Empty();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
