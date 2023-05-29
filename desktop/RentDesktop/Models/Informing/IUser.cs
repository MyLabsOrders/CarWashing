using System;
using System . Collections . ObjectModel;

namespace RentDesktop . Models . Informing {
	public interface IUser {
		ObservableCollection<OrderModel> Orders { get; set; }
		double Money { get; set; }
		byte [ ] Icon { get; set; }
		string ID { get; set; }
		string Login { get; set; }
		string Position { get; set; }
		string Status { get; set; }
		string Password { get; set; }
		string Patronymic { get; set; }
		string PhoneNumber { get; set; }
		string Name { get; set; }
		string Surname { get; set; }
		string Gender { get; set; }
		DateTime DateOfBirth { get; set; }

		public string DateOfBirthPresenter { get; }

		bool IsTheUser ( );
		void CopyToOtherUser ( IUser other );
		bool IsTheAdmin ( );
		}
	}
