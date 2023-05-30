using Avalonia . Controls;
using MessageBox . Avalonia;
using MessageBox . Avalonia . Enums;
using CarWashing . Infrastructure . Helpers;
using System;

namespace CarWashing . Models . Messaging {
	public class MsgBox : IMsgBox {
		public MsgBox ( string m , string t = "" , ButtonEnum b = ButtonEnum . Ok , Icon i = Icon . None ) {
		MsgTitle=t;
		MsgButtons=b;
		MsgIcon=i;
		MsgMessage=m;
			}

		public string MsgTitle { get; }
		public string MsgTitleHeader { get; }
		public string MsgHeader { get; }
		public ButtonEnum MsgButtons { get; }
		public Icon MsgIcon { get; }
		public Icon MsgFirstIcon { get; }
		public string MsgMessage { get; }
		public string MsgContent { get; }

		public void Dialog ( Window? oW ) {
		if ( oW is null ) {
		return;
			}

		MessageBox . Avalonia . BaseWindows . Base . IMsBoxWindow<ButtonResult> w = MessageBoxManager.GetMessageBoxStandardWindow(MsgTitle, MsgMessage, MsgButtons, MsgIcon);
		System . Threading . Tasks . Task<ButtonResult> unused = w . ShowDialog ( oW );
			}

		public void ShowWithoutDialog ( Window? oW ) {
		if ( oW is null ) {
		return;
			}

		MessageBox . Avalonia . BaseWindows . Base . IMsBoxWindow<ButtonResult> w = MessageBoxManager.GetMessageBoxStandardWindow(MsgTitle, MsgMessage, MsgButtons, MsgIcon);
		System . Threading . Tasks . Task<ButtonResult> unused = w . Show ( oW );
			}

		public void ShowWithoutDialog ( Type w ) {
		Window? window = WindowSearcher.TpFin(w);
		ShowWithoutDialog ( window );
			}

		public static MsgBox InfoMsg ( string message ) => new ( message , "Информация" , ButtonEnum . Ok , Icon . Info );

		public void Dialog ( Type w ) {
		Window? window = WindowSearcher.TpFin(w);
		Dialog ( window );
			}

		public static MsgBox ErrorMsg ( string message ) => new ( message , "Ошибка" , ButtonEnum . Ok , Icon . Error );
		}
	}
