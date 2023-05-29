namespace RentDesktop.Models.DatabaseModels
{
#pragma warning disable IDE1006

    internal class DatabaseChangePassword
    {
        public DatabaseChangePassword()
        {
        }

        public DatabaseChangePassword(string currentPassword, string newPassword)
        {
            this.currentPassword = currentPassword;
            this.newPassword = newPassword;
        }

        public string currentPassword { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
    }

#pragma warning restore IDE1006
}
