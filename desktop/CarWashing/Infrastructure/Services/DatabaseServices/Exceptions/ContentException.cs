using System;
using System . Net . Http;

namespace CarWashing . Infrastructure . Services . DatabaseServices {
	internal class ContentException : ApplicationException {
		public ContentException ( HttpContent c , string? m = null , Exception? i = null )
				: base ( m??$"Content is incorrect: {c . ReadAsStringAsync ( ) . Result}" , i ) => HttpContentObject=c;

		public HttpContent HttpContentObject { get; set; }
		public string Reason { get; set; }
		public string Result { get; set; }
		public string Error { get; set; }	
		}
	}
