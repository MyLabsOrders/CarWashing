using RentDesktop.Models;
using RentDesktop.Models.DatabaseModels;
using RentDesktop.Models.Informing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class DatabaseOrders
    {
        private static OrderModel Register(List<Tuple<ProductRentModel, int>> productsInfo, IUser userInfo)
        {
            using var db = new ConnectToDb();

            string addOrderHandle = $"/api/User/{userInfo.ID}/orders";

            var content = productsInfo
                .Select(t => new DatabaseOrderProduct(t.Item1.Transport.ID, t.Item2, t.Item1.Days))
                .ToList();

            using HttpResponseMessage addOrderResponse = db.Put(addOrderHandle, content).Result;

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

        public static OrderModel Create(IEnumerable<ProductRentModel> cart, IUser userInfo)
        {
            var products = cart
                .GroupBy(t => t.Transport.ID)
                .Select(t => new Tuple<ProductRentModel, int>(t.First(), t.Count()))
                .ToList();

            return Register(products, userInfo);
        }
    }
}
