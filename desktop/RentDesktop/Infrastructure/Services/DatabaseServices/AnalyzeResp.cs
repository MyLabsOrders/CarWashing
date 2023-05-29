using Newtonsoft.Json;
using System.Net.Http;

namespace RentDesktop.Infrastructure.Services.DatabaseServices
{
    internal static class AnalyzeResp
    {
        public static string Reason(HttpResponseMessage response)
        {
            string c = response.Content.ReadAsStringAsync().Result;
            object? jobj = JsonConvert.DeserializeObject(c);
            return JsonConvert.SerializeObject(jobj, Formatting.Indented);
        }
    }
}
