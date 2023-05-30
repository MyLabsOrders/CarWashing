using RentDesktop . Models . Base;
using System;

namespace RentDesktop . Models . Security {
	public class Secret : ModelBase, ISecret {
		public Secret ( int length = 5 ) {
		Len=length;
		UpdateText ( );
			}

		private int _length = 0;
		public int Len {
			get => _length;
			set {
			if ( value<=0 ) {
			throw new ArgumentException ( "Length must be greater than zero." , nameof ( value ) );
				}

			_=RaiseAndSetIfChanged ( ref _length , value );
				}
			}

		private string _text = string.Empty;
		public string Text {
			get => _text;
			private set => RaiseAndSetIfChanged ( ref _text , value );
			}

		private string _textWithoutSpace = string.Empty;
		public string TextWithoutSpace {
			get => _textWithoutSpace;
			private set => RaiseAndSetIfChanged ( ref _textWithoutSpace , value );
			}

		public void UpdateText ( ) {
		const string sb = "abcdefghijklmnopqrstuvwxyz0123456789";
		string t = string.Empty;

		var r = new Random();

		for ( int j = 0 ; j<_length ; ++j ) {
		int index = r.Next(0, sb.Length - 1);

		t+=index%2==0
				? sb [ index ]
				: char . ToUpper ( sb [ index ] );
			}

		Text=t;
			}
		}
	}
