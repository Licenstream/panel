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
            // still to implement
            throw new NotImplementedException();
        }

        public IEnumerable<License> GetAll()
        {
            // implementation
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/subscribedSkus");
            var token = "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6InN2SnBEUXFqaWFoODZYUEd4dEduRFhrTEwxYTVDMEstRVdKOFkyRmxvbEkiLCJhbGciOiJSUzI1NiIsIng1dCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSIsImtpZCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzEzMTgyMjE0LCJuYmYiOjE3MTMxODIyMTQsImV4cCI6MTcxMzE4Njk3NCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhXQUFBQW9EcHQ1eUxyenFxZWNWL2JDUERqNjFXS1pzY2F5RGVhS1ZKaTVWM0EzSWtPblUvMnJWdGMvcjY1cGxVVmNWUGdOQUFITFc3WUY4VTQ3TDRJRVRvOGtDdW5ENnVvT3FKd0NYZmZ1RWtsbDZrPSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiQ2xvdWQgUGFydG5lcnMiLCJhcHBpZCI6ImUzNTVkZDFlLTE0MGItNDNlMS1hZjc0LWNlOGYwMzQzZDQ3MSIsImFwcGlkYWNyIjoiMSIsImdpdmVuX25hbWUiOiJSb2IiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIyYTAyOmE0NmM6NDU0NjowOjhkZjk6OTFiNTozM2IwOjNjNjQiLCJuYW1lIjoiUm9iIiwib2lkIjoiNGM4NDFlNjAtMDBiZi00NzczLTk4MDAtZTAzZDEzYWVkMmQzIiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMyMDAzNzFGODkwMEQiLCJyaCI6IjAuQWE0QTh3R09jR2RkS2sta3ZzN25iWG1JQlFNQUFBQUFBQUFBd0FBQUFBQUFBQUN0QUEwLiIsInNjcCI6IkRpcmVjdG9yeS5SZWFkLkFsbCBNYWlsLlJlYWQgU3Vic2NyaXB0aW9uLlJlYWQuQWxsIFVzZXIuUmVhZCBVc2VyLlJlYWQuQWxsIHByb2ZpbGUgb3BlbmlkIGVtYWlsIiwic3ViIjoia3FDRWxjOFpHLWJWbGhrU1kzX25EYlV5Z195QURDOGhpa2Nnd1hKc1pCUSIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJFVSIsInRpZCI6IjcwOGUwMWYzLTVkNjctNGYyYS1hNGJlLWNlZTc2ZDc5ODgwNSIsInVuaXF1ZV9uYW1lIjoicm9iQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwidXBuIjoicm9iQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwidXRpIjoiYmNwR3djQmNjMDJRVDVaenFUU0hBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiNjJlOTAzOTQtNjlmNS00MjM3LTkxOTAtMDEyMTc3MTQ1ZTEwIiwiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il0sInhtc19zdCI6eyJzdWIiOiJrM1dvcjdDV1VTQnFidEFkS1Z1TlMtM0tfVFpQVGlCX3VGRU9ZM3pEdTJNIn0sInhtc190Y2R0IjoxNzA5NTY4MDIwLCJ4bXNfdGRiciI6IkVVIn0.wTaUyMfg7muEdm3wpFIdNfK-3HVuuFgLX0_k4Ht-Gk1rpeSLsxTRNsfwl60GX2G3TEmQ-x1MT8nFTjvWMRlmkOvKCKrje96gglxpqMQFjcsCThM41mnh6BxxINaIpZO-GZCnOhwIRwn4t7TCIXLfh99IU8SfWsn42ZVVj6KvOGui9SGn-Iftacg1HqvD2-Cio8iGXc8pmOUOUoIxHzRJ2GKS9e7bx-JCrQqu9vA5yicN55pD93th2gi1XoH9fQ_uAUT4tdmlsbpH47WgcftfBI1-y-JA_2t5MwWkEiA9nuq3pTSbOp7VhrYeDagIkANq17H42wXz4CSpWVpVuqVqrQ";
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
