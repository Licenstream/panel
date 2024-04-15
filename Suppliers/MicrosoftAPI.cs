using Domain;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using Domain.Interfaces;

namespace SupplierService
{
    public class MicrosoftAPI
    {
        private IDataHandler _dataHandler;

        public MicrosoftAPI(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IEnumerable<Domain.License>> GetLicensesAsync()
        {
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/subscribedSkus");
            //var token = "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6Illvbk03UGM0a2Q5dE0wbDNTWEF5X2xIcDZrekV5Tk5ZdDUyVUZjblB5UVkiLCJhbGciOiJSUzI1NiIsIng1dCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSIsImtpZCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzEyNTg2Mjg2LCJuYmYiOjE3MTI1ODYyODYsImV4cCI6MTcxMjU5MTA4NCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhXQUFBQWJNY3MzYWt3S3RkMm1EYXF2T0R6c0RHN2hma0s4aU5zNlZsaWNiT0daL2dtdkFiV3c0bEVOeUZ4aFlvbmVRWjAiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkNsb3VkIFBhcnRuZXJzIiwiYXBwaWQiOiJlMzU1ZGQxZS0xNDBiLTQzZTEtYWY3NC1jZThmMDM0M2Q0NzEiLCJhcHBpZGFjciI6IjEiLCJmYW1pbHlfbmFtZSI6IkFkbWluaXN0cmF0b3IiLCJnaXZlbl9uYW1lIjoiTU9EIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMTQ1LjkzLjExOC45NSIsIm5hbWUiOiJNT0QgQWRtaW5pc3RyYXRvciIsIm9pZCI6ImNmZjFkYjM5LTQzNzktNGIxNC1hYmRhLTg1ODlmOGE0MmQwOSIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMzVEMTlBOTAzIiwicmgiOiIwLkFhNEE4d0dPY0dkZEtrLWt2czduYlhtSUJRTUFBQUFBQUFBQXdBQUFBQUFBQUFDdEFCZy4iLCJzY3AiOiJEaXJlY3RvcnkuUmVhZC5BbGwgTWFpbC5SZWFkIFN1YnNjcmlwdGlvbi5SZWFkLkFsbCBVc2VyLlJlYWQgVXNlci5SZWFkLkFsbCBwcm9maWxlIG9wZW5pZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJrbXNpIl0sInN1YiI6IjlBbXhsc0lOcUdXWDk0TE9SOVJfY3dpaTRZblE3akZrVnprVmNqNXE0TjAiLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiRVUiLCJ0aWQiOiI3MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUiLCJ1bmlxdWVfbmFtZSI6ImFkbWluQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwidXBuIjoiYWRtaW5ATTM2NXg4NDcxNDEyNC5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJKUXRWOW55dllVUzJVT1VzWWNaOUFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6Ikl6TUsyM0dsX1hOcFVFWmNrZEgtTW9uVVZwVjlPS3JydTZxMmN6RUZwN1EifSwieG1zX3RjZHQiOjE3MDk1NjgwMjAsInhtc190ZGJyIjoiRVUifQ.qeX9SbK6JNBkYpgrl1LlG1CrePSWjO437gL67jgE5xl31h0AaiifE-bpM61B-c7-BbGtmrAEo8yiURXOurlqc-BAu-64iASv47E6J6PJ22csq2zFZI0jiQ9hBsu0r2h8RGGk0O8NyB7alT_44iIiaT8J_UVTcbMCtdIiZnbiPd0Pv1MUIwhlQ9zDGrdiJB2JPT1LZjC2ekk3_zYEdgKbrLyvpmuCKFthvYsqQoMlZlPjU7fSGxuHh1jHqlUqRl_Ym93m8vjpbbSp5iEcXUwewLjfAWgzEcLxx368LgQxkbu8HonKyUiWNh_DGcPDfMhWPINGVKom2cKCgXhwMslCig";
            //request.Headers.Add("Authorization", token);
            //var response = client.Send(request);
            //response.EnsureSuccessStatusCode();
            //string apiResponse = await response.Content.ReadAsStringAsync();
            //LicenseResponse licenseResponse = JsonConvert.DeserializeObject<LicenseResponse>(apiResponse);
            return _dataHandler.GetLicenses();
        }
    }
}
