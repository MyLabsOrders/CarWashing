using System;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal class ContentException : ApplicationException {
		public ContentException ( HttpContent content , string? message = null , Exception? innerException = null )
				: base ( message??$"Content is incorrect: {content . ReadAsStringAsync ( ) . Result}" , innerException ) => Content=content;

		public HttpContent Content { get; set; }
		}
	}
