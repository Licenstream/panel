using Domain;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace SupplierService
{
    public class MicrosoftAPI
    {
        //{
    //    public License GetSubscriptionData()
    //    {
    //        var license = new License();
    //        var httpClient = new HttpClient();
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJub25jZSI6Ik5ySVNhbjBvUWpzOGthNm5JS1JUXzB1bWV0U081bmZGYXdiLUMzSVp1N2MiLCJhbGciOiJSUzI1NiIsIng1dCI6IlhSdmtvOFA3QTNVYVdTblU3Yk05blQwTWpoQSIsImtpZCI6IlhSdmtvOFA3QTNVYVdTblU3Yk05blQwTWpoQSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzExMzg2MzY0LCJuYmYiOjE3MTEzODYzNjQsImV4cCI6MTcxMTM5MTIzNSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhXQUFBQUhaaTYvSXMrZ2d1QWgvd3gwMlVmRkY1ajNtTUpHTWtUc2plMEI5dzdnSkl4N1ZrcFRuYkp2cFYvQUJ0bWsxZGMiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkNsb3VkIFBhcnRuZXJzIiwiYXBwaWQiOiJlMzU1ZGQxZS0xNDBiLTQzZTEtYWY3NC1jZThmMDM0M2Q0NzEiLCJhcHBpZGFjciI6IjEiLCJmYW1pbHlfbmFtZSI6IkFkbWluaXN0cmF0b3IiLCJnaXZlbl9uYW1lIjoiTU9EIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMTQ1LjkzLjExNC4xNjEiLCJuYW1lIjoiTU9EIEFkbWluaXN0cmF0b3IiLCJvaWQiOiJjZmYxZGIzOS00Mzc5LTRiMTQtYWJkYS04NTg5ZjhhNDJkMDkiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzIwMDM1RDE5QTkwMyIsInJoIjoiMC5BYTRBOHdHT2NHZGRLay1rdnM3bmJYbUlCUU1BQUFBQUFBQUF3QUFBQUFBQUFBQ3RBQmcuIiwic2NwIjoiRGlyZWN0b3J5LlJlYWQuQWxsIE1haWwuUmVhZCBTdWJzY3JpcHRpb24uUmVhZC5BbGwgVXNlci5SZWFkIFVzZXIuUmVhZC5BbGwgcHJvZmlsZSBvcGVuaWQgZW1haWwiLCJzdWIiOiI5QW14bHNJTnFHV1g5NExPUjlSX2N3aWk0WW5RN2pGa1Z6a1ZjajVxNE4wIiwidGVuYW50X3JlZ2lvbl9zY29wZSI6IkVVIiwidGlkIjoiNzA4ZTAxZjMtNWQ2Ny00ZjJhLWE0YmUtY2VlNzZkNzk4ODA1IiwidW5pcXVlX25hbWUiOiJhZG1pbkBNMzY1eDg0NzE0MTI0Lm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6ImFkbWluQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwidXRpIjoiZ2M1MzNBNjYza09URlVzSS1SS1BBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiNjJlOTAzOTQtNjlmNS00MjM3LTkxOTAtMDEyMTc3MTQ1ZTEwIiwiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il0sInhtc19zdCI6eyJzdWIiOiJJek1LMjNHbF9YTnBVRVpja2RILU1vblVWcFY5T0tycnU2cTJjekVGcDdRIn0sInhtc190Y2R0IjoxNzA5NTY4MDIwLCJ4bXNfdGRiciI6IkVVIn0.kMWHv-SDl7KYwxtvNYpEPTWhvZ9_86_lnQhPKtEIYh9S9ukF1CHd9S3p6sO2qLFU2fgVFonENGXxmG8NUR7swjWFIjsrgsir5A01xshWL_RO5jypJ190nV8q-Ti7q4AbIW9mLMMPpP6dcecphDgSL-t3gWcAEvTQskgxiQhJOHGNRDqv5Dh0EpfSV7rIyKi7wYvpo-Q1qbIDv8xSAvIjY47eD7SpmEiOufGG8wynK4aLkjOERs9JEKMs-wZ8Jn6pjemdPp97Zc4qwS05TqNQlS0cuW_358AnsHrCoS21RPGKVnlrpq751QmpI5TudMpyUrk3warw1oH6DksW3tDpGA");

    //        var requestmessage = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/subscribedSkus");
    //        using (httpClient)
    //        {
    //            using (var response = httpClient.Send(requestmessage))
    //            {
    //                string apiResponse = response.Content.ToString();
    //                license = JsonConvert.DeserializeObject<License>(apiResponse);
    //            }
    //        }
    //        return license;
    //    }
    //}

        public Domain.License GetLicense()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/subscribedSkus");
            request.Headers.Add("Authorization", "eyJ0eXAiOiJKV1QiLCJub25jZSI6Ink2clhSTmQwTnYwQ2pUbHh3d1RnSGNwS0xLTFVEcFZBRy1jLWJLek1yeDgiLCJhbGciOiJSUzI1NiIsIng1dCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSIsImtpZCI6InEtMjNmYWxldlpoaEQzaG05Q1Fia1A1TVF5VSJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzExNjI0MjU3LCJuYmYiOjE3MTE2MjQyNTcsImV4cCI6MTcxMTYyODYyNSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhXQUFBQXZ3WXRwcGp0QngrSWdrUWdtckVLSXQ2UUJGc1NiQ2Z3M1ZkclFNNFZIMDhZa0NyNnNJQloyU3ZzWVNVWERvaEsiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkNsb3VkIFBhcnRuZXJzIiwiYXBwaWQiOiJlMzU1ZGQxZS0xNDBiLTQzZTEtYWY3NC1jZThmMDM0M2Q0NzEiLCJhcHBpZGFjciI6IjEiLCJmYW1pbHlfbmFtZSI6IkFkbWluaXN0cmF0b3IiLCJnaXZlbl9uYW1lIjoiTU9EIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMmEwMjphNDZjOjQ1NDY6MDo3YzJkOmE4NTQ6OTdlYjplYjM0IiwibmFtZSI6Ik1PRCBBZG1pbmlzdHJhdG9yIiwib2lkIjoiY2ZmMWRiMzktNDM3OS00YjE0LWFiZGEtODU4OWY4YTQyZDA5IiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMyMDAzNUQxOUE5MDMiLCJyaCI6IjAuQWE0QTh3R09jR2RkS2sta3ZzN25iWG1JQlFNQUFBQUFBQUFBd0FBQUFBQUFBQUN0QUJnLiIsInNjcCI6IkRpcmVjdG9yeS5SZWFkLkFsbCBNYWlsLlJlYWQgU3Vic2NyaXB0aW9uLlJlYWQuQWxsIFVzZXIuUmVhZCBVc2VyLlJlYWQuQWxsIHByb2ZpbGUgb3BlbmlkIGVtYWlsIiwic2lnbmluX3N0YXRlIjpbImttc2kiXSwic3ViIjoiOUFteGxzSU5xR1dYOTRMT1I5Ul9jd2lpNFluUTdqRmtWemtWY2o1cTROMCIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJFVSIsInRpZCI6IjcwOGUwMWYzLTVkNjctNGYyYS1hNGJlLWNlZTc2ZDc5ODgwNSIsInVuaXF1ZV9uYW1lIjoiYWRtaW5ATTM2NXg4NDcxNDEyNC5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJhZG1pbkBNMzY1eDg0NzE0MTI0Lm9ubWljcm9zb2Z0LmNvbSIsInV0aSI6Il9oOUZFckZGUGs2RDJkczBIUllPQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbIjYyZTkwMzk0LTY5ZjUtNDIzNy05MTkwLTAxMjE3NzE0NWUxMCIsImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfc3QiOnsic3ViIjoiSXpNSzIzR2xfWE5wVUVaY2tkSC1Nb25VVnBWOU9LcnJ1NnEyY3pFRnA3USJ9LCJ4bXNfdGNkdCI6MTcwOTU2ODAyMCwieG1zX3RkYnIiOiJFVSJ9.4xTFni9a_EnRiwMgnRRBvGs2qLinBlj7AmgYkiceTB_HG9krn1GVnj3NDi - 3RmbmiDKj8b_ywZl5_skU7TjEU2QB3wMMybcqh55eHxFt741WFB - RtU1GrPkh_nU - Bi3LAlOHHaR1g63H_Qo - PCCHwtq_uruTZXWhoaG3Mm9dWQeP7l3nsDOYqfT9_m_hRYqHrvkLywZ0pobuGaI20S_RNPmAHQpbLoMDT4iu_fJ7xfHc7vo3z26ZjyW1eHDCqA2XC95c24HJpgEbjG7rpsqivfmqZEnYCOuc09bNjvvYVJtTRgee5zikhfT87U8N9l8J4hD7pk7_EvahAuc - bwzzEw");
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            string apiResponse = response.Content.ToString();
            var license = JsonConvert.DeserializeObject<Domain.License>(apiResponse);
            return license;
        }
    }
}
