using Avalonia;
using Avalonia . Controls;
using Avalonia . Controls . ApplicationLifetimes;
using CarWashing . Views;
using System;
using System . Linq;

namespace CarWashing . Infrastructure . Helpers {
	internal static class WindowSearcher {
		public static Window? User ( ) => TpFin ( typeof ( UserWindow ) );

		public static Window? Admin ( ) => TpFin ( typeof ( AdminWindow ) );
		public static Window? Main ( ) => Application . Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime a
						? null
						: a . MainWindow;

		public static Window? WinSearch ( Func<Window , bool> predicate ) => Application . Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime a
						? null
						: a . Windows . FirstOrDefault ( predicate );

		public static Window? TpFin ( Type type ) => WinSearch ( t => t . GetType ( )==type );
		}
	}
