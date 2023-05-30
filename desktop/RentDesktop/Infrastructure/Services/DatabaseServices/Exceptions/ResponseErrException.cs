using System;
using System . Net;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal class ResponseErrException : ApplicationException {
		public ResponseErrException ( HttpStatusCode c , string r , string? m = null , Exception? i = null )
				: base ( m??$"Status: {c}\n{r}" , i ) {
		HttpCode=c;
		ErrorMessageReason=r;
			}

		public ResponseErrException ( HttpResponseMessage r , string? m = null , Exception? i = null )
		: this ( r . StatusCode , AnalyzeResp . Reason ( r ) , m , i ) {
			}

		public string Result { get; set; }
		public string Error { get; set; }
		public HttpStatusCode HttpCode { get; }
		public string ErrorMessageReason { get; }
		}
	}
