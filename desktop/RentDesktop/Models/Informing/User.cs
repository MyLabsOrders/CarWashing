using System;
using System . Collections . ObjectModel;

namespace RentDesktop . Models . Informing {
	public class User : IUser {
		public const string POS_ADMIN = "admin";
		public const string ST_OPEN = "Открыт";

		public ObservableCollection<OrderModel> Orders { get; set; } = new ObservableCollection<OrderModel> ( );
		public ObservableCollection<OrderModel> UserOrders { get; set; } = new ObservableCollection<OrderModel> ( );

		public string HiddenName { get; set; } = string . Empty;
		public string Surname { get; set; } = string . Empty;
		public string HiddenSurname { get; set; } = string . Empty;
		public string Patronymic { get; set; } = string . Empty;
		public string HiddenPatronymic { get; set; } = string . Empty;
		public string PhoneNumber { get; set; } = string . Empty;

		public const string ST_INACTIVE = "Неактивен";
		public const string ST_CLOSE = "Закрыт";

		public string HiddenPhoneNumber { get; set; } = string . Empty;
		public string Login { get; set; } = string . Empty;
		public string HiddenLogin { get; set; } = string . Empty;
		public double Money { get; set; } = 0;
		public double MoneyTotal { get; set; } = 0;
		public byte [ ] Icon { get; set; } = Array . Empty<byte> ( );
		public DateTime DateOfBirth { get; set; } = default;
		public DateTime PrevDateOfBirth { get; set; } = default;

		public const string MALE = "Мужской";
		public const string FEMALE = "Женский";
		public const string HIDDEN = "*";
		public const string HIDDEN_QUESTION = "?";
		public const string HIDDEN_VOSKL = "!";

		public string DateOfBirthPresenter => DateOfBirth . ToShortDateString ( );

		public bool IsTheAdmin ( ) => Position==POS_ADMIN;
		internal bool InternalIsTheAdmin ( ) => IsTheAdmin();

		public string Status { get; set; } = string . Empty;
		public string ID { get; set; } = string . Empty;

		public bool IsTheUser ( ) => Position==POS_USER;

		public ObservableCollection<OrderModel> AdminOrders { get; set; } = new ObservableCollection<OrderModel> ( );
		public string Position { get; set; } = string . Empty;
		public string Name { get; set; } = string . Empty;

		internal bool InternalIsTheUser ( ) => IsTheUser();

		public const string POS_USER = "user";
		public const string ST_ACTIVE = "Активен";

		public string Password { get; set; } = string . Empty;
		public string Gender { get; set; } = string . Empty;
		public string HiddenGender { get; set; } = string . Empty;

		public void CopyToOtherUser ( IUser o ) {
		o . Password=Password;
		o . Name=Name;
		o . Surname=Surname;
		o . Patronymic=Patronymic;
		o . ID=ID;
		o . Login=Login;
		o . Money=Money;
		o . Icon=Icon;
		o . DateOfBirth=DateOfBirth;
		o . PhoneNumber=PhoneNumber;
		o . Gender=Gender;
		o . Position=Position;
		o . Status=Status;
			}
		}
	}
