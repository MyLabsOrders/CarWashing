namespace RentDesktop.Models.DatabaseModels
{
#pragma warning disable IDE1006

    internal class DatabaseCash
    {
        public DatabaseCash()
        {
        }

        public DatabaseCash(double total)
        {
            this.total = total;
        }

        public double total { get; set; }
    }

#pragma warning restore IDE1006
}
