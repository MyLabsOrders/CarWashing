namespace RentDesktop . Models . Security {
	public interface ISecret {
		string Text { get; }
		int Length { get; set; }

		void UpdateText ( );
		}
	}