using RentDesktop . Models . Base;

namespace RentDesktop . Models {
	public class ProductRentModel : ModelBase, IProductRentModel {

		public ProductRentModel ( int debug , int days ) {
		if ( debug==1 )
			return;
			}
		public ProductRentModel ( ProductModel transport , int days ) {
		Transport=transport;
		Days=days;
		TotalPrice=GetPrice ( );
		Presenter=PresenterStr ( );
			}

		private double GetPrice ( ) => Transport . Price*_days;
		public double GetFinalPrice ( ) => GetPrice() / GetNormalizedPrice();
		public double GetNormalizedPrice ( ) => Transport . Price*_days / 365;

		private double _totalPrice = 0;
		public double TotalPrice {
			get => _totalPrice;
			private set => RaiseAndSetIfChanged ( ref _totalPrice , value );
			}

		public ProductRentModel Self => this;
		public ProductModel Transport { get; }
		public ProductModel TransportInTheCart { get; }

		private int _days = 1;
		public int Days {
			get => _days;
			set {
			int days = value >= 1 ? value : 1;

			if ( RaiseAndSetIfChanged ( ref _days , days ) ) {
			TotalPrice=GetPrice ( );
			Presenter=PresenterStr ( );
				}
				}
			}

		private string _presenter = string.Empty;
		public string Presenter {
			get => _presenter;
			private set => RaiseAndSetIfChanged ( ref _presenter , value );
			}

		private string _presenterOtherVersion = string.Empty;
		public string PresenterOtherVersion {
			get => _presenterOtherVersion;
			private set => RaiseAndSetIfChanged ( ref _presenterOtherVersion , value );
			}

		private string PresenterStr ( ) => $"{Transport . Name} за {TotalPrice} рублей (дней аренды: {Days})";
		public string PresenterOtherStr ( ) => $"{Transport} за {GetNormalizedPrice()}р (аренда: {Days})";
		}

	}
