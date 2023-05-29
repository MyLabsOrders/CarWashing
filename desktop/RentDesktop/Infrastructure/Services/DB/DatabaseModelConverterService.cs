using RentDesktop.Models;
using RentDesktop.Models.DB;
using RentDesktop.Models.Informing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RentDesktop.Infrastructure.Services.DB
{
    internal static class DatabaseModelConverterService
    {
        public static UserInfo ConvertUser(DatabaseUser user, string position)
        {
            return new UserInfo()
            {
                ID = user.id,
                Login = string.Empty,
                Password = string.Empty,
                Name = user.firstName,
                Surname = user.middleName,
                Patronymic = user.lastName,
                PhoneNumber = user.number,
                Gender = GenderService.FromDatabaseFormat(user.gender),
                Position = position,
                Status = user.isActive ? UserInfo.ST_ACTIVE : UserInfo.ST_INACTIVE,
                Money = user.money,
                Icon = BitmapService.StringToBytes(user.image),
                DateOfBirth = DateTimeService.StringToShortDateTime(user.birthDate),
                Orders = new ObservableCollection<OrderModel>(ConvertOrders(user.orders ?? Array.Empty<DatabaseOrder>()))
            };
        }

        public static List<IUser> ConvertUsers(DatabaseUsers databaseUsers, IReadOnlyList<string> positions)
        {
            if (databaseUsers.users is null)
                return new List<IUser>();

            if (databaseUsers.users.Count() != positions.Count)
                throw new InvalidOperationException("The number of users does not match the number of their positions.");

            UserInfo UserConverter(DatabaseUser user, int index)
            {
                return ConvertUser(user, positions[index]);
            }

            return databaseUsers.users!
                .Select(UserConverter)
                .Select(t => t as IUser)
                .ToList();
        }

        public static IEnumerable<OrderModel> ConvertProducts(IEnumerable<DatabaseOrder> databaseOrders)
        {
            return databaseOrders.Select(t => new OrderModel(
                id: t.id,
                price: t.total,
                status: t.status,
                dateOfCreation: t.orderDate is null ? default : DateTimeService.StringToDateTime(t.orderDate),
                models: new[] { ConvertDbOrderToTransport(t) }
            ));
        }

        public static IEnumerable<OrderModel> ConvertOrders(IEnumerable<DatabaseOrder> databaseOrders)
        {
            var orders = new List<OrderModel>();
            IEnumerable<IGrouping<string?, DatabaseOrder>> grouppedDatabaseOrders = databaseOrders.GroupBy(t => t.orderDate);

            foreach (IGrouping<string?, DatabaseOrder> transportGroup in grouppedDatabaseOrders)
            {
                DatabaseOrder firstDatabaseOrder = transportGroup.First();
                IEnumerable<ProductModel> transports = transportGroup.Select(t => ConvertDbOrderToTransport(t));

                string status = OrderModel.RENTED_STATUS;
                string id = firstDatabaseOrder.id;
                double price = transportGroup.Sum(t => t.total * t.count!.Value * t.days!.Value);

                DateTime dateOfCreation = firstDatabaseOrder.orderDate is null
                    ? default
                    : DateTimeService.StringToDateTime(firstDatabaseOrder.orderDate);

                var order = new OrderModel(id, price, status, dateOfCreation, transports);
                orders.Add(order);
            }

            return orders;
        }

        public static ProductModel ConvertDbOrderToTransport(DatabaseOrder order)
        {
            byte[] imageBytes = BitmapService.StringToBytes(order.image);

            Avalonia.Media.Imaging.Bitmap? transportIcon = imageBytes.Length > 0
                ? BitmapService.BytesToBitmap(imageBytes)
                : null;

            DateTime creationDate = order.orderDate is null
                ? default
                : DateTimeService.StringToDateTime(order.orderDate);

            return new ProductModel(
                order.id,
                order.name,
                order.company,
                order.total,
                creationDate,
                transportIcon
            );
        }
    }
}
