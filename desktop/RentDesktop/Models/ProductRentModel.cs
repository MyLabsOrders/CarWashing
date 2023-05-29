using RentDesktop.Models.Base;

namespace RentDesktop.Models
{
    public class ProductRentModel : ModelBase, IProductRentModel
    {
        public ProductRentModel(ProductModel transport, int days)
        {
            Transport = transport;
            Days = days;
            TotalPrice = CalcTotalPrice();
            Presenter = GetPresenterString();
        }

        public ProductRentModel Self => this;
        public ProductModel Transport { get; }

        private int _days = 1;
        public int Days
        {
            get => _days;
            set
            {
                int days = value >= 1 ? value : 1;

                if (RaiseAndSetIfChanged(ref _days, days))
                {
                    TotalPrice = CalcTotalPrice();
                    Presenter = GetPresenterString();
                }
            }
        }

        private double _totalPrice = 0;
        public double TotalPrice
        {
            get => _totalPrice;
            private set => RaiseAndSetIfChanged(ref _totalPrice, value);
        }

        private string _presenter = string.Empty;
        public string Presenter
        {
            get => _presenter;
            private set => RaiseAndSetIfChanged(ref _presenter, value);
        }

        private double CalcTotalPrice()
        {
            return Transport.Price * _days;
        }

        private string GetPresenterString()
        {
            return $"{Transport.Name} за {TotalPrice} рублей (дней аренды: {Days})";
        }
    }
}
