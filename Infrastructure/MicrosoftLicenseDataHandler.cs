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
            var token = "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6IldHTTRqakpZY3pFYWlnWnN3dHNFanpHak1mdjcxTGVJNHE1ckJfVXdGQ1EiLCJhbGciOiJSUzI1NiIsIng1dCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCIsImtpZCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzE1NTI4NzQ0LCJuYmYiOjE3MTU1Mjg3NDQsImV4cCI6MTcxNTUzMzg3OSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhXQUFBQVJoWG5UdXp4ZVBXVmMxUWhCRm9Kd2ExbTFZczJ2eUxSdlFORmc3Wm43WStRRU12eS9sdE5ZOS9oNXltTDZZVlBtTXNnc3N4TENGSWhSU0k2TldiTytNYVEzaWVWZis3REtDdzRSS0hGcU9jPSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiQ2xvdWQgUGFydG5lcnMiLCJhcHBpZCI6ImUzNTVkZDFlLTE0MGItNDNlMS1hZjc0LWNlOGYwMzQzZDQ3MSIsImFwcGlkYWNyIjoiMSIsImZhbWlseV9uYW1lIjoiQWRtaW5pc3RyYXRvciIsImdpdmVuX25hbWUiOiJNT0QiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIyNC4xMzIuOTcuNjgiLCJuYW1lIjoiTU9EIEFkbWluaXN0cmF0b3IiLCJvaWQiOiJjZmYxZGIzOS00Mzc5LTRiMTQtYWJkYS04NTg5ZjhhNDJkMDkiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzIwMDM1RDE5QTkwMyIsInJoIjoiMC5BYTRBOHdHT2NHZGRLay1rdnM3bmJYbUlCUU1BQUFBQUFBQUF3QUFBQUFBQUFBQ3RBQmcuIiwic2NwIjoiRGlyZWN0b3J5LlJlYWQuQWxsIE1haWwuUmVhZCBTdWJzY3JpcHRpb24uUmVhZC5BbGwgVXNlci5SZWFkIFVzZXIuUmVhZC5BbGwgcHJvZmlsZSBvcGVuaWQgZW1haWwiLCJzdWIiOiI5QW14bHNJTnFHV1g5NExPUjlSX2N3aWk0WW5RN2pGa1Z6a1ZjajVxNE4wIiwidGVuYW50X3JlZ2lvbl9zY29wZSI6IkVVIiwidGlkIjoiNzA4ZTAxZjMtNWQ2Ny00ZjJhLWE0YmUtY2VlNzZkNzk4ODA1IiwidW5pcXVlX25hbWUiOiJhZG1pbkBNMzY1eDg0NzE0MTI0Lm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6ImFkbWluQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwidXRpIjoidWhNTUcwX1gwMHE0SXlnMWRlS2pBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiNjJlOTAzOTQtNjlmNS00MjM3LTkxOTAtMDEyMTc3MTQ1ZTEwIiwiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il0sInhtc19zdCI6eyJzdWIiOiJJek1LMjNHbF9YTnBVRVpja2RILU1vblVWcFY5T0tycnU2cTJjekVGcDdRIn0sInhtc190Y2R0IjoxNzA5NTY4MDIwLCJ4bXNfdGRiciI6IkVVIn0.L66Ho-bQ9YZZHmNsfhqGHRokS_yHBwmNUlC5L97xAxogTv3uZ453Bn8rzgX08yWfG_jya7xdF_Y34shsvgkWaZC54RYJ7-UO2PFVKyJbxIK4tbMtFxV2TIyH2G5riBveBkXIbosSUrLk5wh5xWhbAs4HuMOy4128BQTY7Ryx-OPkYcYTVWiNwCC8KW2lcmfEnRFC7AWYLHGgE-CLVv9uqbaLX2JHyT2_caB-OuYkn2PajmOu8oz5tHVXhqFctzpPp3pOWJMq54ENxA6vWJafCJ68Kefqe221DQW5G9E4MJPYtV354hxvJMdndnETMgP822Uma3bAkgL77ybY8HGyag";
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
