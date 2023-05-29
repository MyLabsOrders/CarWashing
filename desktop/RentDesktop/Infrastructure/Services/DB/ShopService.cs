using RentDesktop.Models;
using RentDesktop.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentDesktop.Infrastructure.Services.DB
{
    internal static class ShopService
    {
        public static List<ProductModel> GetTransports()
        {
            var transports = new List<ProductModel>();
            using var db = new DatabaseConnectionService();

            int currentPage = 1;
            IEnumerable<DatabaseOrder> currentOrder;

            do
            {
                string getOrdersHandle = $"/api/Order?page={currentPage++}";
                using HttpResponseMessage getOrdersResponse = db.GetAsync(getOrdersHandle).Result;

                if (!getOrdersResponse.IsSuccessStatusCode)
                    throw new ErrorResponseException(getOrdersResponse);

                DatabaseOrderCollection? orderCollection = getOrdersResponse.Content.ReadFromJsonAsync<DatabaseOrderCollection>().Result;

                if (orderCollection is null || orderCollection.orders is null)
                    throw new IncorrectContentException(getOrdersResponse.Content);

                IEnumerable<DatabaseOrder> orders = orderCollection.orders.Where(t => t.orderDate is null);

                IEnumerable<IReadOnlyList<ProductModel>> transportsCollection = DatabaseModelConverterService.ConvertProducts(orders)
                    .Select(t => t.Models);

                foreach (IReadOnlyList<ProductModel>? currTransports in transportsCollection)
                    transports.AddRange(currTransports);

                currentOrder = orderCollection.orders;
            }
            while (currentOrder.Any());

            return transports;
        }

        public static void AddTransport(IProductModel transport)
        {
            using var db = new DatabaseConnectionService();

            const string addOrderHandle = "/api/Order";

            byte[] transportIconBytes = transport.Icon is null
                ? Array.Empty<byte>()
                : BitmapService.BitmapToBytes(transport.Icon);

            var content = new DatabaseCreateProduct()
            {
                name = transport.Name,
                company = transport.Company,
                status = OrderModel.AVAILABLE_STATUS,
                price = transport.Price,
                orderImage = BitmapService.BytesToString(transportIconBytes)
            };

            using HttpResponseMessage addOrderResponse = db.PostAsync(addOrderHandle, content).Result;

            if (!addOrderResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(addOrderResponse);
        }
    }
}
