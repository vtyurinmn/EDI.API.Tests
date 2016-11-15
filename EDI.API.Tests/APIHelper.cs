using System;
using Newtonsoft.Json;
using RestSharp;

namespace EDI.API.Tests
{
    public class ApiHelper
    {
        public string GetToken(string apiClientId, string login, string password, Uri baseUrl)
		{
            string preAuthString =
               $"KonturEdiAuth konturediauth_api_client_id={apiClientId},konturediauth_login={login},konturediauth_password={password}";
            var endpoint = new Uri(baseUrl, "/V1/Authenticate");
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", preAuthString);
            IRestResponse response = client.Execute(request);
            string token = response.Content;
			return token;
		}

        public string SendMessage(Uri baseUrl, string filepath, string boxId, string apiClientId, string token)
		{
		    string authString = $"KonturEdiAuth konturediauth_api_client_id={apiClientId},konturediauth_token={token}";
		    byte[] documentBytes = System.IO.File.ReadAllBytes(filepath);
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/V1/Messages/SendMessage", Method.POST);
            request.AddHeader("authorization", authString);
            request.AddHeader("Content-Type", "application/edi-x12");
            request.AddParameter("application/edi-x12", documentBytes, ParameterType.RequestBody);
            request.AddQueryParameter("boxId", boxId);
            IRestResponse response = client.Execute(request);
            dynamic message = JsonConvert.DeserializeObject(response.Content);
            var messageid = message["MessageId"];
            return messageid;
		}

        //на повторную отправку приходят два события - NewOutboxMessage и MessageUndelivered. Придется забирать оба. Соответственно, зададим count=2

        public string VerifyMessage(string token, string messageId, Uri baseUrl, string boxId, string apiClientId)
		{
            string authString = $"KonturEdiAuth konturediauth_api_client_id={apiClientId},konturediauth_token={token}";
            var endpoint = new Uri(baseUrl, "/V1/Messages/GetEvents");
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", authString);
            request.AddParameter("boxId", boxId);
            request.AddParameter("count", "2");
            IRestResponse response = client.Execute(request);
            dynamic message = JsonConvert.DeserializeObject(response.Content);
            var reasons = message["Events"][1]["EventContent"]["MessageUndeliveryReasons"][0];
		    return reasons;
		}
    }
} 