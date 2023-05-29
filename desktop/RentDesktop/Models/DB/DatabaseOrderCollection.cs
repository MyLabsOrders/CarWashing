using System.Collections.Generic;

namespace RentDesktop.Models.DB
{
#pragma warning disable IDE1006

    internal class DatabaseOrderCollection
    {
        public IEnumerable<DatabaseOrder>? orders { get; set; } = null;
        public int page { get; set; } = 1;
        public int totalPages { get; set; } = 0;
    }

#pragma warning restore IDE1006
}
