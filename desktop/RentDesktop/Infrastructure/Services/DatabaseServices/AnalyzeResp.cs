using Newtonsoft . Json;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class AnalyzeResp {
		public static string Reason ( HttpResponseMessage response ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return null;
			}
			}
			}
			}
			}
			}
			}
		object? jobj = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
		return JsonConvert . SerializeObject ( jobj , Formatting . Indented );
			}
		}
	}
