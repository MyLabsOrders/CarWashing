using System;

namespace CarWashing . Infrastructure . Services . DatabaseServices {
	internal class TimeExceededException : ApplicationException {
		public TimeExceededException ( int t , string? m = null , Exception? i = null )
				: base ( m??"Max response waiting time has been exceeded." , i ) => Time=t;

		public string Result { get; set; }
		public string Error { get; set; }
		public int Time { get; }
		}
	}
