using Avalonia.Media.Imaging;
using RentDesktop.Models;
using RentDesktop.Models.DatabaseModels;
using RentDesktop.Models.Informing;
using System;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal class DatabaseGenerationService : IDisposable
    {
        #region Default values

        private const string DEFAULT_ICON_LADA7_PATH = "D://Testing//TechRental//lada7.jpg";
        private const string DEFAULT_ICON_LADA10_PATH = "D://Testing//TechRental//lada10.jpg";
        private const string DEFAULT_ICON_LADA15_PATH = "D://Testing//TechRental//lada15.jpg";
        private const string DEFAULT_ICON_NIVA_PATH = "D://Testing//TechRental//niva.jpg";
        private const string DEFAULT_ICON_UAZ_PATH = "D://Testing//TechRental//uaz.jpg";

        private readonly UserInfo _defaultAdmin = new()
        {
            Login = "admin",
            Password = "Admin123!"
        };

        private readonly UserInfo[] _defaultUsers = new[]
        {
            new UserInfo()
            {
                Login = "user1",
                Password = "User123$",
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                PhoneNumber = "8 (921) 123-4567",
                Gender = UserInfo.MALE,
                Position = UserInfo.POS_USER,
                Status = UserInfo.ST_ACTIVE,
                DateOfBirth = new DateTime(2000, 1, 21),
                Money = 100,
            },
            new UserInfo()
            {
                Login = "user2",
                Password = "User123$",
                Name = "Саша",
                Surname = "Сашов",
                Patronymic = "Иванович",
                PhoneNumber = "8 (863) 376-4567",
                Gender = UserInfo.MALE,
                Position = UserInfo.POS_USER,
                Status = UserInfo.ST_ACTIVE,
                DateOfBirth = new DateTime(2002, 4, 20),
                Money = 1000,
            },
            new UserInfo()
            {
                Login = "user3",
                Password = "User123$",
                Name = "Маша",
                Surname = "Машова",
                Patronymic = "Машович",
                PhoneNumber = "8 (314) 198-4567",
                Gender = UserInfo.FEMALE,
                Position = UserInfo.POS_USER,
                Status = UserInfo.ST_ACTIVE,
                DateOfBirth = new DateTime(2005, 12, 3),
                Money = 5000,
            },
            new UserInfo()
            {
                Login = "user4",
                Password = "User123$",
                Name = "Петр",
                Surname = "Петров",
                Patronymic = "Петрович",
                PhoneNumber = "8 (953) 976-0088",
                Gender = UserInfo.MALE,
                Position = UserInfo.POS_ADMIN,
                Status = UserInfo.ST_ACTIVE,
                DateOfBirth = new DateTime(1990, 10, 10),
                Money = 100000,
            },
            new UserInfo()
            {
                Login = "user5",
                Password = "User123$",
                Name = "Таня",
                Surname = "Танина",
                Patronymic = "Татьянович",
                PhoneNumber = "8 (921) 999-1455",
                Gender = UserInfo.FEMALE,
                Position = UserInfo.POS_USER,
                Status = UserInfo.ST_ACTIVE,
                DateOfBirth = new DateTime(1980, 5, 5),
                Money = 400000,
            }
        };

        private readonly ProductModel[] _defaultTransports = new[]
        {
            new ProductModel("", "Lada 7", "Lada", 10000, default, new Bitmap(DEFAULT_ICON_LADA7_PATH)),
            new ProductModel("", "Lada 10", "Lada", 5000, default, new Bitmap(DEFAULT_ICON_LADA10_PATH)),
            new ProductModel("", "Lada 15", "Lada", 7000, default, new Bitmap(DEFAULT_ICON_LADA15_PATH)),
            new ProductModel("", "Niva", "Chevrolet", 25000, default, new Bitmap(DEFAULT_ICON_NIVA_PATH)),
            new ProductModel("", "UAZ", "Ulianovka", 30000, default, new Bitmap(DEFAULT_ICON_UAZ_PATH)),
        };

        #endregion

        private bool _isDisposed;
        private readonly string? _previousAuthorizationToken;

        public DatabaseGenerationService()
        {
            _previousAuthorizationToken = DatabaseConnectionService.AuthorizationToken;

            DatabaseLoginResponseContent loginContent = LoginService.EnterSystem(_defaultAdmin.Login, _defaultAdmin.Password, null, true);
            DatabaseConnectionService.AuthorizationToken = loginContent.token;
        }

        ~DatabaseGenerationService()
        {
            if (!_isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            DatabaseConnectionService.AuthorizationToken = _previousAuthorizationToken;
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        public void Generate()
        {
            AddUsers();
            AddTransports();
        }

        private void AddUsers()
        {
            foreach (UserInfo user in _defaultUsers)
            {
                UserRegisterService.RegisterUser(user);

                if (user.Position != UserInfo.POS_ADMIN)
                    UserCashService.AddCash(user, user.Money, true);
            }
        }

        private void AddTransports()
        {
            foreach (ProductModel transport in _defaultTransports)
            {
                ShopService.AddTransport(transport);
            }
        }
    }
}
