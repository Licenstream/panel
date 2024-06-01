using System.Net;
using Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure;

public class MicrosoftLicenseApiHandler
{
    private readonly ILogger _logger;

    public MicrosoftLicenseApiHandler(ILogger logger)
    {
        _logger = logger;
    }

    public IEnumerable<License> GetLicenses()
    {
        List<License> data = new List<License>();

        var token = GetAccessToken();
        var httpRequestMessage = GetSubscriptionBetaRequest(token.AccessToken);

        try
        {
            var task = GetJsonDataTask(httpRequestMessage);

            // Example JSON data for licenses
            // {
            //     "@odata.context": "https://graph.microsoft.com/beta/$metadata#directory/subscriptions(skuId,status,createdDateTime,nextLifecycleDateTime,isTrial,totalLicenses)",
            //     "value": [
            //         {
            //             "skuId": "6fd2c87f-b296-42f0-b197-1e91e994b900",
            //             "status": "Enabled",
            //             "createdDateTime": "2024-03-04T00:00:00Z",
            //             "nextLifecycleDateTime": "2024-07-04T00:00:00Z",
            //             "isTrial": true,
            //             "totalLicenses": 2
            //         },
            //         {
            //             "skuId": "06ebc4ee-1bb5-47dd-8120-11324bc54e06",
            //             "status": "Enabled",
            //             "createdDateTime": "2024-03-04T00:00:00Z",
            //             "nextLifecycleDateTime": "2024-07-04T00:00:00Z",
            //             "isTrial": true,
            //             "totalLicenses": 20
            //         }
            //     ]
            // }

            var desiredType = new
            {
                value = new[]
                {
                    new
                    {
                        skuId = "",
                        status = "",
                        skuPartNumber = "",
                        createdDateTime = "",
                        nextLifecycleDateTime = "",
                        isTrial = "",
                        totalLicenses = "",
                        serviceStatus = new[]
                        {
                            new
                            {
                                servicePlanId = "",
                                servicePlanName = "",
                                provisioningStatus = "",
                                appliesTo = ""
                            }
                        }
                    }
                }
            };

            var anonymousType = JsonConvert.DeserializeAnonymousType(task.Result, desiredType);
            foreach (var part in anonymousType.value)
            {
                var skuId = part.skuId;
                var status = part.status;
                var name = part.skuPartNumber;
                var createdDate = Convert.ToDateTime(part.createdDateTime);
                var nextLifeCycleDate = Convert.ToDateTime(part.nextLifecycleDateTime);
                var isTrail = Convert.ToBoolean(part.isTrial);
                var totalLicenses = Convert.ToInt32(part.totalLicenses);

                var newLicense = new License(-1, skuId, status, name, totalLicenses,
                    createdDate, nextLifeCycleDate, isTrail);
                
                foreach (var item in part.serviceStatus)
                {
                    var servicePlanId = item.servicePlanId;
                    var servicePlanName = item.servicePlanName;
                    var provisioningStatus = item.provisioningStatus;
                    var appliesTo = item.appliesTo;
                    
                    newLicense.ServiceStats.Add(
                        new ServiceStatus(servicePlanId, servicePlanName, 
                            provisioningStatus, appliesTo));
                }
                
                data.Add(newLicense);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return data;
    }

    private Token GetAccessToken()
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            "https://login.microsoftonline.com/708e01f3-5d67-4f2a-a4be-cee76d798805/oauth2/v2.0/token");
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("client_id", "b3dc09fc-31ac-4dd4-93e5-55bcaf09c9d3"));
        collection.Add(new("scope", "https://graph.microsoft.com/.default"));
        collection.Add(new("refresh_token",
            "0.Aa4A8wGOcGddKk-kvs7nbXmIBfwJ3LOsMdRNk-VVvK8JydOtABg.AgABAwEAAADnfolhJpSnRYB1SVj-Hgd8AgDs_wUA9P_7i4KOKaKVykJsOetnz2fbmqDTKN3vrQrSgkCoehcFQd7_WtyYN1cQG9KO1uv01YaYKREteu7iTz7ZZ2pvX1aLgdSsMCl3bj72n8C8g18VJ-Jsj46zWjRvc5XhQxApcqeZ9C5R1IjB_QCfovy5NE-GkRxwDP1BIr9u_4mQ62zLw51sSP5mUm48ihiCHFkANPVBcQWfLEbTWften7j3EIYxeWNWa_PQ781oAOyehhABSLhImj4cpeAr8Ef9Rhv1SufIRu86nHHd5qo1YXW0ypE-FW0KeJB58PiCcr9Nujqjaj-bOJKyyQ17h8dvsaruMdv6T0X5tl4ZLsoHaIeHPgDz1tAdjnNyjdfFZ1gLOb6Qffii4SSTW6xD5gOf1HiziQ_Q5cJDXieSdw3LQvjhZ5yrA1YAy6UNr0CtZnAC-Ug7S6ueSSlrRQwaP_rGGttKtzybVp9nlU2idDwKWgZvq_VmsgNvyN9O1posfnSVlaDXDF31OXaxijLIkg9lY94QokICdsDfppjuD4emI-gfFIjOEKqa_vvk49mor6QTvgPfyV2JK_M-3YwkIL33-XqTzmNPdJaZfdYMZdgtIspPYY1JyuTmPYrAsYmnsRgeifA0vv-JCYiK97EH0abPPOWKMJES3IhoaXEnsjw4PdeWFaOrGzwX6DVGzScXe9CY8iVP8j48qQH5zKb0P678cAxOTHMuUuVBVORa-wn0z2U5q4JrWBsqY_iWTYVTke1ehsjOS1PL_KixePIkrUv5c-QazVGSLg6gfNkLWHadKDbATg3t0YmY427hj1aWh2aZDFoQE7QUR64"));
        collection.Add(new("grant_type", "refresh_token"));
        collection.Add(new("client_secret", "YCi8Q~FbkYuBH.6tr~k-5I6cIK~5mhVubBl9vb29"));
        var requestContentCollection = new FormUrlEncodedContent(collection);
        request.Content = requestContentCollection;

        var task = GetJsonDataTask(request);

        // Example JSON data for access token
        // {
        //     "token_type": "Bearer",
        //     "scope": "openid profile email https://graph.microsoft.com/Directory.Read.All https://graph.microsoft.com/Subscription.Read.All https://graph.microsoft.com/User.Read https://graph.microsoft.com/User.Read.All https://graph.microsoft.com/.default",
        //     "expires_in": 4937,
        //     "ext_expires_in": 4937,
        //     "access_token": "eyJ0eXAiOiJKV1QiLCJub25jZSI6Im0tZTExNkFnZU02blQ4ZmZIQlZ3dktEUWZPdlYzSHA5M21ZaDd6cTYyWjQiLCJhbGciOiJSUzI1NiIsIng1dCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCIsImtpZCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUvIiwiaWF0IjoxNzE2OTI4MTE3LCJuYmYiOjE3MTY5MjgxMTcsImV4cCI6MTcxNjkzMzM1NSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhXQUFBQXpkNDNVNFg3WUxFRTZ2SGd5N1BiK28rellMT0N4WTU3MVc0TmdxQ0VsVmNFOG9mZDhqYU1HVWFZTEZXR2U4Z05PdllPSlRPWkxIakEwTzJwVDRPVElHYTRMOWZObzdRQWtuV2dmRE9VZDh3PSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiTGljZW5zdHJlYW0iLCJhcHBpZCI6ImIzZGMwOWZjLTMxYWMtNGRkNC05M2U1LTU1YmNhZjA5YzlkMyIsImFwcGlkYWNyIjoiMSIsImZhbWlseV9uYW1lIjoiQWRtaW5pc3RyYXRvciIsImdpdmVuX25hbWUiOiJNT0QiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIyNC4xMzIuOTcuNjgiLCJuYW1lIjoiTU9EIEFkbWluaXN0cmF0b3IiLCJvaWQiOiJjZmYxZGIzOS00Mzc5LTRiMTQtYWJkYS04NTg5ZjhhNDJkMDkiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzIwMDM1RDE5QTkwMyIsInJoIjoiMC5BYTRBOHdHT2NHZGRLay1rdnM3bmJYbUlCUU1BQUFBQUFBQUF3QUFBQUFBQUFBQ3RBQmcuIiwic2NwIjoiRGlyZWN0b3J5LlJlYWQuQWxsIG9wZW5pZCBTdWJzY3JpcHRpb24uUmVhZC5BbGwgVXNlci5SZWFkIFVzZXIuUmVhZC5BbGwgcHJvZmlsZSBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJrbXNpIl0sInN1YiI6IjlBbXhsc0lOcUdXWDk0TE9SOVJfY3dpaTRZblE3akZrVnprVmNqNXE0TjAiLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiRVUiLCJ0aWQiOiI3MDhlMDFmMy01ZDY3LTRmMmEtYTRiZS1jZWU3NmQ3OTg4MDUiLCJ1bmlxdWVfbmFtZSI6ImFkbWluQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwidXBuIjoiYWRtaW5ATTM2NXg4NDcxNDEyNC5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJlNDYwVVIzeEpFRzVFUzVubHJwUEFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6IkNTUVVuU3gwT0FFaG9ueEp0aFdTRDRvdXB1elJMTXZVa3NHVmFVQnh3cUUifSwieG1zX3RjZHQiOjE3MDk1NjgwMjAsInhtc190ZGJyIjoiRVUifQ.ldCpDdGhIy91UUHJlYHztyBuqMblnv1_PCe-e73fWWLWsugM1RfLpW7nE2--7HatgTvpjjekycyeMumIcqc6zGErhICOzDqcXQGy4mwi6eD_K3D24b8NT87sImYpdHwplgYNCVK_6t96fk2B5VSKaJWHWEUaZ1DElspvGU9eSCCvBAiuAKAbOdRxjkEorxyfvE7DXOotRCF460r_0dkZ_Pu2rZ85si0HjdC0n1cnbuxd1jn0k1uDG9vzA5lj4Zs2mSkQdDlWj33dhw9rwVNQ0VqJc8y967CyrYMOjcnmp9xON4_ZSwmtpfZefYlbxnCDuOamoHoeWDl7c3HpyF-OmQ",
        //     "refresh_token": "0.Aa4A8wGOcGddKk-kvs7nbXmIBfwJ3LOsMdRNk-VVvK8JydOtABg.AgABAwEAAADnfolhJpSnRYB1SVj-Hgd8AgDs_wUA9P-rTcEko2o85PW19zp5xh4DWqVrCm7qr6YtPiirEqt0Z9vSkYU176AP0la9kOL6hRn0SGTVY0M8N0taGH0KADPKgEVvdcoChfMtBSiKJA61yaB9FbV2XaSYW_35P0-VJX2rpSdXe8JLuI8uye_bvHDHtzmEx7isR7kyzVOxLl0JdXE_CIdUFqL1y9w2SQuUOmivrGyUV2SqJbX0DF-aVLHsEtwvVVhVcpUiT0tXm7OtUfERhUPjuYlQgBwq86aghE7SeBjqcp8HqiBBmdE_mnimZsW-DtBo8SCXmDrv7SMXIsZseb654UjMoEvvD1ucIWyWlaONSsfUquxSNIXDhMLJ7GvoFprgiZaFlDSlBRqN8Ai_DrO2EtgsgUikp8QXsSEeXHfZRcOB0bViRX4XGwfvqGfYVDiOaIYofCHSuJBVvOCCto-NDNWgRNDkqvU_014SwNpnpwzRBJ3sPHC9_-CHJmAJFJcHAxc4QXZ1I0MiCt-M_O2MGH_Uw-p15N-tncdxj4yF5ljd4L7uKHwUSMcL-vXivkaRQvShzq8WvjEhbFosm6ZJKzWD94zkqrGQ-A-mgDv30T_4yVRiIF_Xg_ihN6eOxXzhTLPif9FZt6nzYMqFHvE0WXI2pPVa_blraLpBgfjnr2e_Yu4SN_nRRRWFzucPvdZ2bn4KFFCwsmK481xXog-sVnWoL_s4gItChE-ex9HCnHusbFk51kxuCtv_QmovqLcjlNTZDxeiT7_fCAsaOPzQxWdxmxbxNs8QLg6yzeeRVzi6gE5h9vbYWSgZzh4Mv3zdY4NWgII",
        //     "id_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IkwxS2ZLRklfam5YYndXYzIyeFp4dzFzVUhIMCJ9.eyJhdWQiOiJiM2RjMDlmYy0zMWFjLTRkZDQtOTNlNS01NWJjYWYwOWM5ZDMiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vNzA4ZTAxZjMtNWQ2Ny00ZjJhLWE0YmUtY2VlNzZkNzk4ODA1L3YyLjAiLCJpYXQiOjE3MTY5MjgxMTcsIm5iZiI6MTcxNjkyODExNywiZXhwIjoxNzE2OTMyMDE3LCJhaW8iOiJBV1FBbS84V0FBQUFKTDlzL3ZTamVwallBSXVmQlQrUTMxanJ6OHNzZ0E4cm5GS21SaDZaZmhCOElLNDVuaFQ1Ky9UTHRHVkxGdXR6UCtBWEdRdWFBWGphUWcvV1d5cEJwQUVLNXpSOUhWajRFS0RxTno3TXdrWmdZamhFMEFsQmhOS21JeEhrWG9DUCIsIm5hbWUiOiJNT0QgQWRtaW5pc3RyYXRvciIsIm9pZCI6ImNmZjFkYjM5LTQzNzktNGIxNC1hYmRhLTg1ODlmOGE0MmQwOSIsInByZWZlcnJlZF91c2VybmFtZSI6ImFkbWluQE0zNjV4ODQ3MTQxMjQub25taWNyb3NvZnQuY29tIiwicmgiOiIwLkFhNEE4d0dPY0dkZEtrLWt2czduYlhtSUJmd0ozTE9zTWRSTmstVlZ2SzhKeWRPdEFCZy4iLCJzdWIiOiJDU1FVblN4ME9BRWhvbnhKdGhXU0Q0b3VwdXpSTE12VWtzR1ZhVUJ4d3FFIiwidGlkIjoiNzA4ZTAxZjMtNWQ2Ny00ZjJhLWE0YmUtY2VlNzZkNzk4ODA1IiwidXRpIjoiZTQ2MFVSM3hKRUc1RVM1bmxycFBBQSIsInZlciI6IjIuMCJ9.kX0TT8fDH8PEDWkAlSSwY_pH6lTEoessF4FWLl7dujoj2sEUyWL9o1qKxYLjaWh39pSsIv-GA2vOTSmev-PeKj-f52F_SPRn7411eLwuHBjK79MndNmmt3LhKGWGaXf1opnogoRjQy1Qxxz1hM_8PxcwPudQsf4ZO5Unvzuss9PS6WM0cr2FIPN0LhPydDH4vKOvQZtJFxhfPKV79FrlOQz5k6S0H-qoECGVSK3okvL7-8sxwQUtShZDJ3FrtmuqAllJuyETu10MDZOvJr1--swTFZXHHjImsbc7BadMWpeftaJCoCBPrF6P35ri6nnoy2jeOhOzCmhJSnPpWAuGBg"
        // }

        var desiredType = new
        {
            token_type = "",
            access_token = "",
            refresh_token = "",
            id_token = ""
        };

        var part = JsonConvert.DeserializeAnonymousType(task.Result, desiredType);

        var tokenType = part.token_type;
        var accessToken = part.access_token;
        var refreshToken = part.refresh_token;
        var idToken = part.id_token;

        var token = new Token(tokenType, accessToken, refreshToken, idToken);

        return token;
    }

    private HttpRequestMessage GetSubscriptionBetaRequest(string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://graph.microsoft.com/beta/directory/subscriptions?$select=skuId,status,skuPartNumber,createdDateTime,nextLifecycleDateTime,isTrial,totalLicenses,serviceStatus");

        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        return request;
    }

    private async Task<string> GetJsonDataTask(HttpRequestMessage httpRequestMessage)
    {
        Task<string> task;

        var client = new HttpClient();

        using (var response = await client.SendAsync(httpRequestMessage))
        {
            response.EnsureSuccessStatusCode();

            task = response.Content.ReadAsStringAsync();
        }

        return await task;
    }
}

internal class Token
{
    public string TokenType { get; }
    public string AccessToken { get; }
    public string RefreshToken { get; }
    public string IdToken { get; }

    public Token(string tokenType, string accessToken, string refreshToken, string idToken)
    {
        TokenType = tokenType;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        IdToken = idToken;
    }
}