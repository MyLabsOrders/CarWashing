using RentDesktop . Models;
using System . IO;
using System . Net . Http;
using System . Threading . Tasks;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class FileDownloadService {
		public static MemoryStream ChequeGet ( IOrderModel order ) {
		using var db = new ConnectToDb();

		string getChequeHandle = $"/api/Order/cheque?orderTime={order.DateOfCreationStamp}";
		Task<HttpResponseMessage> getChequeTask = db.Get(getChequeHandle);

		if ( !getChequeTask . Wait ( WAIT_TIME ) ) {
		throw new ResponseWaitingTimeExceededException ( WAIT_TIME );
			}

		using HttpResponseMessage getChequeResponse = getChequeTask.Result;

		if ( !getChequeResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( getChequeResponse );
			}

		byte[] chequeBytes = getChequeResponse.Content.ReadAsByteArrayAsync().Result;
		return new MemoryStream ( chequeBytes );
			}

		public static MemoryStream InvoiceGet ( IOrderModel order ) {
		using var db = new ConnectToDb();

		string getInvoiceHandle = $"/api/Order/invoice?orderTime={order.DateOfCreationStamp}";
		Task<HttpResponseMessage> getInvoiceTask = db.Get(getInvoiceHandle);

		if ( !getInvoiceTask . Wait ( WAIT_TIME ) ) {
		throw new ResponseWaitingTimeExceededException ( WAIT_TIME );
			}

		using HttpResponseMessage getInvoiceResponse = getInvoiceTask.Result;

		if ( !getInvoiceResponse . IsSuccessStatusCode ) {
		throw new ErrorResponseException ( getInvoiceResponse );
			}

		byte[] invoiceBytes = getInvoiceResponse.Content.ReadAsByteArrayAsync().Result;
		return new MemoryStream ( invoiceBytes );
			}

		private const int WAIT_TIME = 3000;
		}
	}
