namespace RentDesktop . Models . Security {
	public interface ISecret {
		string Captcha { get; }
		int Len { get; set; }

		void UpdateText ( );
		}
	}