﻿namespace RentDesktop.Models.DatabaseModels
{
#pragma warning disable IDE1006

    internal class DatabaseUsername
    {
        public DatabaseUsername()
        {
        }

        public DatabaseUsername(string username)
        {
            this.username = username;
        }

        public string username { get; set; } = string.Empty;
    }

#pragma warning restore IDE1006
}
