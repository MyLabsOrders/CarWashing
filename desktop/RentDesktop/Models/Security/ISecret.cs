namespace RentDesktop . Models . Security {
	public interface ISecret {
		string Text { get; }
		int Len { get; set; }

		void UpdateText ( );
		}
	}