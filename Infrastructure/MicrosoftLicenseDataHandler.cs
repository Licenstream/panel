using Domain;
using Domain.Interfaces;
using Newtonsoft.Json;

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
        public License Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<License> GetAll()
        {
            // implementation
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/subscribedSkus");
            var token = "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6IjYxS1k0cW8wRWgwWHpNTUhzZFlMTXJTVmZtR19ob1JTVXZHejM0ZVJiMk0iLCJhbGciOiJSUzI1NiIsIng1dCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCIsImtpZCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9iYmRiNjIxNC04YjVjLTQ1MjktYTdkYi0wZjQ4OGE2YjY0YjAvIiwiaWF0IjoxNzE2MjExMDA4LCJuYmYiOjE3MTYyMTEwMDgsImV4cCI6MTcxNjIxNTUzNiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhXQUFBQW0zNVU0SDRrRHdqUGorK0VYM3BuajRzb2NHcFZmZndnRjRHL2tWZEFpZFdkR3NwU3dBK2twNjBQc1BaOUlML3VPVnM4RzZjZzArQmN6QWVwRnZjRVBNR21IOHpRbmUrUmtobXowNi8xYlhjPSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiTGljZW5zdHJlYW0iLCJhcHBpZCI6ImIzZGMwOWZjLTMxYWMtNGRkNC05M2U1LTU1YmNhZjA5YzlkMyIsImFwcGlkYWNyIjoiMSIsImZhbWlseV9uYW1lIjoiQmxva3MiLCJnaXZlbl9uYW1lIjoiWGFuZGVyIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMjQuMTMyLjk3LjY4IiwibmFtZSI6IlhhbmRlciBCbG9rcyB8IEJsb2dCbG9rcyIsIm9pZCI6ImY0ZDQzNDZiLTQzZDQtNDAyNi04YWNiLTk3NjhhODQ4MDZmMiIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS00MzUwMTg3NTktMzU1OTgxOTAwMi0yOTE3NDg2NjItMTExNiIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMTA2MTA0OTNEIiwicmgiOiIwLkFYTUFGR0xidTF5TEtVV24ydzlJaW10a3NBTUFBQUFBQUFBQXdBQUFBQUFBQUFCekFHdy4iLCJzY3AiOiJEaXJlY3RvcnkuUmVhZC5BbGwgb3BlbmlkIFN1YnNjcmlwdGlvbi5SZWFkLkFsbCBVc2VyLlJlYWQgVXNlci5SZWFkLkFsbCBwcm9maWxlIGVtYWlsIiwic2lnbmluX3N0YXRlIjpbImttc2kiXSwic3ViIjoiN21MNDdjVXdSM1NPdDUtcEI0LWwtOGMyYWlGWFU0dFdyNzBWbUhlT2VXYyIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJFVSIsInRpZCI6ImJiZGI2MjE0LThiNWMtNDUyOS1hN2RiLTBmNDg4YTZiNjRiMCIsInVuaXF1ZV9uYW1lIjoieGJsb2tzQEJsb2dCbG9rcy5ubCIsInVwbiI6InhibG9rc0BCbG9nQmxva3MubmwiLCJ1dGkiOiJBM3hKdVdESmtFLUhQU19ueTh3eUFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyIzZWRhZjY2My0zNDFlLTQ0NzUtOWY5NC01YzM5OGVmNmMwNzAiLCI2ZTU5MTA2NS05YmFkLTQzZWQtOTBmMy1lOTQyNDM2NmQyZjAiLCJlMzk3M2JkZi00OTg3LTQ5YWUtODM3YS1iYThlMjMxYzcyODYiLCI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCI5MzYwZmViNS1mNDE4LTRiYWEtODE3NS1lMmEwMGJhYzQzMDEiLCJmNzA5MzhhMC1mYzEwLTQxNzctOWU5MC0yMTc4Zjg3NjU3MzciLCIzZDc2MmM1YS0xYjZjLTQ5M2YtODQzZS01NWEzYjQyOTIzZDQiLCI5ZjA2MjA0ZC03M2MxLTRkNGMtODgwYS02ZWRiOTA2MDZmZDgiLCJiYWYzN2IzYS02MTBlLTQ1ZGEtOWU2Mi1kOWQxZTVlODkxNGIiLCIwZjk3MWVlYS00MWViLTQ1NjktYTcxZS01N2JiOGEzZWZmMWUiLCJmY2Y5MTA5OC0wM2UzLTQxYTktYjViYS02ZjBlYzgxODhhMTIiLCI2OTA5MTI0Ni0yMGU4LTRhNTYtYWE0ZC0wNjYwNzViMmE3YTgiLCJhYWY0MzIzNi0wYzBkLTRkNWYtODgzYS02OTU1MzgyYWMwODEiLCI4OGQ4ZTNlMy04ZjU1LTRhMWUtOTUzYS05Yjk4OThiODg3NmIiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6IkY5TjRmVmc5UF9YNlg5WngxR1NGem10V2M2S0lYVERMWTh5bS1tYWtGeUEifSwieG1zX3RjZHQiOjE2MDA1MjUyNDYsInhtc190ZGJyIjoiRVUifQ.MUtQBxDF_l1X-mTTi_AcpuiwO5WT4QO9lZm7iP8XTRdE4NV3xbAmAQTQuHbkw_WVpOTOI2QQlTLlUU52vIdrN644xqjbUgliYequPQupM_lq_d6WmfV7X0VwiaRUim8j5zTPaj9vUyTRhAE9qrgUq3kuxcxnHtkccfTSjpxutCLL1Y_1o8FKzsLcMyov7XCz1oUloZcAlSn2XAcEC4MFwd9XnYVLexRHp0l_ypo11FOGnGZHdS4lQziXFfU4lsSseH6xdkqmGBCtRq0SJJM79WnkMBY4kAAWpCs1XA2HnVXRVLUYbaa4fNXF2is0-bMRrTbT5iPvxp8brrdCaG6lFw";
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

        public int Insert(License dataType)
        {
            throw new NotImplementedException();
        }
    }
}
