using CarWashing.Models;
using CarWashing.Models.Informing;

namespace CarWashing.Infrastructure.Services.DatabaseServices
{
    internal class DatabaseGenerationService : IDisposable
    {
        #region Default values

        private readonly User _admin = new()
        {
            Login = "root",
            Password = "AdmAdm123123!"
        };

        private readonly User[] _users = new[]
        {
            new User()
            {
                Login = "user1",
                Password = "User123$",
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                PhoneNumber = "8 (921) 123-4567",
                Gender = User.MALE,
                Position = User.POS_USER,
                Status = User.ST_ACTIVE,
                DateOfBirth = new DateTime(2000, 1, 21),
                Money = 100,
            },
            new User()
            {
                Login = "user2",
                Password = "User123$",
                Name = "Саша",
                Surname = "Егоров",
                Patronymic = "Александрович",
                PhoneNumber = "8 (863) 376-4567",
                Gender = User.MALE,
                Position = User.POS_USER,
                Status = User.ST_ACTIVE,
                DateOfBirth = new DateTime(2002, 4, 20),
                Money = 1000,
            },
            new User()
            {
                Login = "user3",
                Password = "User123$",
                Name = "Маша",
                Surname = "Петрова",
                Patronymic = "Евгеньевна",
                PhoneNumber = "8 (314) 198-4567",
                Gender = User.FEMALE,
                Position = User.POS_USER,
                Status = User.ST_ACTIVE,
                DateOfBirth = new DateTime(2005, 12, 3),
                Money = 5000,
            },
            new User()
            {
                Login = "user4",
                Password = "User123$",
                Name = "Петр",
                Surname = "Петров",
                Patronymic = "Петрович",
                PhoneNumber = "8 (953) 976-0088",
                Gender = User.MALE,
                Position = User.POS_ADMIN,
                Status = User.ST_ACTIVE,
                DateOfBirth = new DateTime(1990, 10, 10),
                Money = 100000,
            },
            new User()
            {
                Login = "user5",
                Password = "User123$",
                Name = "Таня",
                Surname = "Денисова",
                Patronymic = "Петровна",
                PhoneNumber = "8 (921) 999-1455",
                Gender = User.FEMALE,
                Position = User.POS_USER,
                Status = User.ST_ACTIVE,
                DateOfBirth = new DateTime(1980, 5, 5),
                Money = 400000,
            }
        };

        private readonly ProductModel[] _products = new[]
        {
            new ProductModel("", "D50", "PA", 9225, default, null),
            new ProductModel("", "DP002", "TOR", 28190, default, null),
            new ProductModel("", "FX 1914BPL", "HAWK", 95470, default, null),
            new ProductModel("", "HR 3500", "RAMEX", 29000, default, null),
            new ProductModel("", "MV925", "HAWK", 4975, default, null),
            new ProductModel("", "PA T46", "PA", 20260, default, null),
            new ProductModel("", "Panda423", "Panda", 63150, default, null),
            new ProductModel("", "PROCAR LT", "PROCAR", 31820, default, null),
            new ProductModel("", "TR-02", "TOR", 4920, default, null),
            new ProductModel("", "VIKAN 290", "VIKAN", 1960, default, null),
        };

        private readonly string[] _productsImages = new[]
        {
            Convert.ToBase64String(File.ReadAllBytes("Assets//D50.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//DP002.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//FX1914BPL.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//HR3500.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//MV925.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//PAT46.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//Panda423.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//PROCAR-LT.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//TR-02.jpg")),
            Convert.ToBase64String(File.ReadAllBytes("Assets//VIKAN290.jpg")),
        };

        #endregion

        private bool _isDisposed;
        private readonly string? _previousAuthorizationToken;

        public DatabaseGenerationService()
        {
            _previousAuthorizationToken = ConnectToDb . AuthToken;

            var loginContent = LoginToDatabase.LogToSystem(_admin.Login, _admin.Password, null, true);
            ConnectToDb.AuthToken = loginContent.token;
        }

        ~DatabaseGenerationService()
        {
            if (!_isDisposed)
                Dispose();
        }

        public void Dispose()
        {
		ConnectToDb . AuthToken = _previousAuthorizationToken;
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        public void Generate()
        {           
            AddUsers();
            AddProducts();
        }

        private void AddUsers()
        {
            foreach (var user in _users)
            {
                RegisterDbUserToDatabase.Register(user);

                if (user.Position != User.POS_ADMIN)
                    DatabaseUserCash.IncreaseMoney(user, user.Money, true);
            }
        }

        private void AddProducts()
        {
            for (int i = 0; i < _products.Length; i++)
            {
                ProductModel? product = _products[i];
                string image = _productsImages[i];

                Shop.AddProduct(product, image);
            }
        }
    }
}
