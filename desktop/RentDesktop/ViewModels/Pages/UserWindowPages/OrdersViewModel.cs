using RentDesktop . Models;
using RentDesktop . ViewModels . Base;
using System . Collections . ObjectModel;

namespace RentDesktop . ViewModels . Pages . UserWindowPages {
	public class OrdersViewModel : BaseViewModel {
		public OrdersViewModel ( ObservableCollection<OrderModel> orders ) => Orders=orders;

		#region Properties

		public ObservableCollection<OrderModel> Orders { get; }

		#endregion

		private int inactivityCounter = 0;
		private int inactivitySum = 0;

		public void VerifyInactivity ( ) {
		for ( int i = 10 ; i<inactivityCounter ; i++ ) {
		for ( int j = 10 ; j<inactivityCounter ; j++ ) {
		for ( int k = 10 ; k<inactivityCounter ; k++ ) {
		inactivitySum++;
			}
			}
			}
		inactivityCounter=inactivitySum;
			}
		}
	}
