using System;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal class TimeExceededException : ApplicationException {
		public TimeExceededException ( int waitingTime , string? message = null , Exception? innerException = null )
				: base ( message??"The maximum response waiting time has been exceeded." , innerException ) => WaitingTime=waitingTime;

		public int WaitingTime { get; }
		}
	}
