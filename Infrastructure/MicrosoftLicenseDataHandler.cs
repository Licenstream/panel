using Domain;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MicrosoftLicenseDataHandler : IDataHandler<License>
    {
        private HttpClient _httpClient;
        private LicenseResponse _response;
        public MicrosoftLicenseDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _response = new LicenseResponse();
        }
        public License Get(string input)
        {
            return GetAll().FirstOrDefault(x => x.SkuPartNumber == input);
        }

        public IEnumerable<License> GetAll()
        {
            // implementation
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/subscribedSkus");
            var token = "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6InRwa1NKTlgxQ1pRTmk0WEF1VUNLM3V1YzQ2VFNOMklkbENUenFINWd0eFkiLCJhbGciOiJSUzI1NiIsIng1dCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSIsImtpZCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzEzMTkzMjcxLCJuYmYiOjE3MTMxOTMyNzEsImV4cCI6MTcxMzE5ODY3NiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhXQUFBQUg2L0ROMS8vZWpyMDg4Z040V094TDRaeFJzQzJnaGgzdHgvRVd6b3ZHMGhtUmtUWEZVdGp2WG9RcU4vckp3VnBvZHV6Vm9WdWlWWVB2cjdPdWRsNktLblBwMUM3QVRubmlxdEt5UnMvQ3I4PSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiQ2xvdWQgUGFydG5lcnMiLCJhcHBpZCI6ImUzNTVkZDFlLTE0MGItNDNlMS1hZjc0LWNlOGYwMzQzZDQ3MSIsImFwcGlkYWNyIjoiMSIsImdpdmVuX25hbWUiOiJSb2IiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIxNDUuOTMuMTE2LjQ3IiwibmFtZSI6IlJvYiIsIm9pZCI6IjRjODQxZTYwLTAwYmYtNDc3My05ODAwLWUwM2QxM2FlZDJkMyIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMzcxRjg5MDBEIiwicmgiOiIwLkFhNEE4d0dPY0dkZEtrLWt2czduYlhtSUJRTUFBQUFBQUFBQXdBQUFBQUFBQUFDdEFBMC4iLCJzY3AiOiJEaXJlY3RvcnkuUmVhZC5BbGwgTWFpbC5SZWFkIFN1YnNjcmlwdGlvbi5SZWFkLkFsbCBVc2VyLlJlYWQgVXNlci5SZWFkLkFsbCBwcm9maWxlIG9wZW5pZCBlbWFpbCIsInN1YiI6ImtxQ0VsYzhaRy1iVmxoa1NZM19uRGJVeWdfeUFEQzhoaWtjZ3dYSnNaQlEiLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiRVUiLCJ0aWQiOiI3MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUiLCJ1bmlxdWVfbmFtZSI6InJvYkBNMzY1eDg0NzE0MTI0Lm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6InJvYkBNMzY1eDg0NzE0MTI0Lm9ubWljcm9zb2Z0LmNvbSIsInV0aSI6IjdfUXFxeXU5T0VPdkhLbmE1TzQxQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbIjYyZTkwMzk0LTY5ZjUtNDIzNy05MTkwLTAxMjE3NzE0NWUxMCIsImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfc3QiOnsic3ViIjoiazNXb3I3Q1dVU0JxYnRBZEtWdU5TLTNLX1RaUFRpQl91RkVPWTN6RHUyTSJ9LCJ4bXNfdGNkdCI6MTcwOTU2ODAyMCwieG1zX3RkYnIiOiJFVSJ9.a5tqAE4mveOT1Vm7No8HiKaDtDCGcgkDbPxo8VPTRMYtBJMyIpJzu9rlNrYdcrQGZavWBg0a7OeoQqp62jpss8mZsCv_LW6yc1xMhNTiaHQuaZbGSn6ACTU56bvQd-PKg8oP7_c7H0F9XqoEGQIB6KTvqjaSN-weYdn2cMSs2VvYoFCWbMZl1DCQwMLk0oddQUvh2sCy_XW0DD1MMyUM3qjfwBA0LJvSGLBmgTSwaUwA9DK6sZcltHqoigiuV_KwhxZhQC8oSBrIRlx3Q13cQgsF7ni8_aJ988N30tXa5oq5Xith0JmhIezpkQXg23Ls6LrbHylqCXxy7UX9VGS1rg";
            request.Headers.Add("Authorization", token);
            var response = _httpClient.Send(request);
            response.EnsureSuccessStatusCode();
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            
            if (apiResponse != null )
            {
                _response = JsonConvert.DeserializeObject<LicenseResponse>(apiResponse);
                return _response.Value;
            }
            
            return Enumerable.Empty<License>();
        }
    }
}
