using Newtonsoft . Json;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class AnalyzeResp {
		public static string Reason ( HttpResponseMessage response ) {
		object? jobj = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
		return JsonConvert . SerializeObject ( jobj , Formatting . Indented );
			}
		}
	}
