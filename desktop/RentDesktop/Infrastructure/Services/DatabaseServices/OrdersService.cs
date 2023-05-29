using RentDesktop.Models;
using RentDesktop.Models.DatabaseModels;
using RentDesktop.Models.Informing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class OrdersService
    {
        public static void ChangeOrderStatus(IOrderModel order, string newStatus)
        {
            using var db = new DatabaseConnectionService();

            const string changeOrderStatusHandle = "/api/Order/status";
            var content = new DatabaseOrderStatus(order.ID, newStatus);

            using HttpResponseMessage changeOrderStatusResponse = db.PutAsync(changeOrderStatusHandle, content).Result;

            if (!changeOrderStatusResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(changeOrderStatusResponse);

            order.Status = newStatus;
        }

        public static OrderModel CreateOrder(IEnumerable<ProductRentModel> cart, IUser userInfo)
        {
            var products = cart
                .GroupBy(t => t.Transport.ID)
                .Select(t => new Tuple<ProductRentModel, int>(t.First(), t.Count()))
                .ToList();

            return RegisterOrder(products, userInfo);
        }

        private static OrderModel RegisterOrder(List<Tuple<ProductRentModel, int>> productsInfo, IUser userInfo)
        {
            using var db = new DatabaseConnectionService();

            string addOrderHandle = $"/api/User/{userInfo.ID}/orders";

            var content = productsInfo
                .Select(t => new DatabaseOrderProduct(t.Item1.Transport.ID, t.Item2, t.Item1.Days))
                .ToList();

            using HttpResponseMessage addOrderResponse = db.PutAsync(addOrderHandle, content).Result;

            if (!addOrderResponse.IsSuccessStatusCode)
                throw new ErrorResponseException(addOrderResponse);

            string creationDateStamp = addOrderResponse.Content.ReadAsStringAsync().Result.Replace("\"", null);
            DateTime creationDate = DateTime.TryParse(creationDateStamp, out DateTime date) ? date : DateTime.Now;

            string status = OrderModel.RENTED_STATUS;
            string id = productsInfo[0].Item1.Transport.ID;
            double price = productsInfo.Sum(t => t.Item1.TotalPrice * t.Item2);
            IEnumerable<ProductModel> models = productsInfo.Select(t => t.Item1.Transport);

            userInfo.Money -= price;

            return new OrderModel(id, price, status, creationDate, models, creationDateStamp);
        }
    }
}
