namespace RentDesktop.Models.DB
{
#pragma warning disable IDE1006

    internal class DatabaseLogin
    {
        public DatabaseLogin()
        {
        }

        public DatabaseLogin(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }

#pragma warning restore IDE1006
}
