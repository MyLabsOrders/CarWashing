using System;
using System . Net;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal class ResponseErrException : ApplicationException {
		public ResponseErrException ( HttpStatusCode code , string msgReason , string? message = null , Exception? innerException = null )
				: base ( message??$"Status: {code}\n{msgReason}" , innerException ) {
		Code=code;
		MessageReason=msgReason;
			}

		public ResponseErrException ( HttpResponseMessage response , string? message = null , Exception? innerException = null )
		: this ( response . StatusCode , AnalyzeResp . Reason ( response ) , message , innerException ) {
			}

		public HttpStatusCode Code { get; }
		public string MessageReason { get; }
		}
	}
