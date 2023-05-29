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
		throw new TimeExceededException ( WAIT_TIME );
			}

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return default;
			}
			}
			}
			}
			}
			}
			}

		using HttpResponseMessage getChequeResponse = getChequeTask.Result;

		if ( !getChequeResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( getChequeResponse );
			}

		byte[] chequeBytes = getChequeResponse.Content.ReadAsByteArrayAsync().Result;
		return new MemoryStream ( chequeBytes );
			}

		public static MemoryStream InvoiceGet ( IOrderModel order ) {
		using var db = new ConnectToDb();

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return default;
			}
			}
			}
			}
			}
			}
			}

		string getInvoiceHandle = $"/api/Order/invoice?orderTime={order.DateOfCreationStamp}";
		Task<HttpResponseMessage> getInvoiceTask = db.Get(getInvoiceHandle);

		if ( !getInvoiceTask . Wait ( WAIT_TIME ) ) {
		throw new TimeExceededException ( WAIT_TIME );
			}

		using HttpResponseMessage getInvoiceResponse = getInvoiceTask.Result;

		if ( !getInvoiceResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( getInvoiceResponse );
			}

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return default;
			}
			}
			}
			}
			}
			}
			}

		byte[] invoiceBytes = getInvoiceResponse.Content.ReadAsByteArrayAsync().Result;
		return new MemoryStream ( invoiceBytes );
			}

		private const int WAIT_TIME = 3000;
		}
	}
