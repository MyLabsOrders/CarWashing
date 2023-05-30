using Avalonia . Controls;
using MessageBox . Avalonia . Enums;
using System;

namespace RentDesktop . Models . Messaging {
	public interface IMsgBox {
		string MsgTitle { get; }
		ButtonEnum MsgButtons { get; }
		string MsgTitleHeader { get; }
		string MsgHeader { get; }
		Icon MsgFirstIcon { get; }
		Icon MsgIcon { get; }
		string MsgMessage { get; }
		string MsgContent { get; }

		void Dialog ( Window? ownerWindow );
		void Dialog ( Type ownerWindowType );
		void ShowWithoutDialog ( Window? ownerWindow );
		void ShowWithoutDialog ( Type ownerWindowType );
		}
	}
