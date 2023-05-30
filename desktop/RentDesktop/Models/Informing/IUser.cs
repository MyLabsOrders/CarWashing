using System;
using System . Collections . ObjectModel;

namespace CarWashing . Models . Informing {
	public interface IUser {
		string HiddenName { get; set; }
		double Money { get; set; }
		byte [ ] Icon { get; set; }
		string HiddenPatronymic { get; set; }
		ObservableCollection<OrderModel> Orders { get; set; }
		string ID { get; set; }
		string Login { get; set; }

		string Surname { get; set; }
		string Gender { get; set; }
		string HiddenSurname { get; set; }
		DateTime DateOfBirth { get; set; }

		public string DateOfBirthPresenter { get; }

		bool IsTheUser ( );
		string Patronymic { get; set; }
		string HiddenPhoneNumber { get; set; }
		string PhoneNumber { get; set; }
		void CopyToOtherUser ( IUser other );
		string Position { get; set; }
		string HiddenLogin { get; set; }
		string Status { get; set; }
		string Password { get; set; }
		string Name { get; set; }
		double MoneyTotal { get; set; }
		bool IsTheAdmin ( );
		}
	}
