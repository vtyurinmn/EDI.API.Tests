using System;
using RestSharp;

namespace EDI.API.Tests
{
    public class ApiHelper
    {
        public string FormPreAuthString(string apiClientId, string login, string password)
		{
            string preAuthString =
                $"KonturEdiAuth konturediauth_api_client_id={apiClientId},konturediauth_login={login},konturediauth_password={password}";
			return preAuthString;
		}

        public string GetToken(string preAuthString, Uri baseUrl)
		{
		    if (preAuthString == null) throw new ArgumentNullException(nameof(preAuthString));
            var endpoint = new Uri(baseUrl, "/V1/Authenticate");
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", preAuthString);
            IRestResponse response = client.Execute(request);
            string token = response.Content;
			return token;
		}

		public string FormAuthString(string apiClientId, string token)
		{
		    if (token == null) throw new ArgumentNullException(nameof(token));
		    string authString = $"KonturEdiAuth konturediauth_api_client_id={apiClientId},konturediauth_token={token}";
			return authString;
		}

		public string SendMessage(string authString, Uri baseUrl, string filename, string filepath, string boxId)
		{
		    if (authString == null) throw new ArgumentNullException(nameof(authString));
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/V1/Messages/SendMessage", Method.POST);
            request.AddHeader("authorization", authString);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddQueryParameter("boxId", boxId);
            request.AddFile(filename, filepath);
            IRestResponse response = client.Execute(request);
            string message = response.Content;
            return message;
		}

		public string VerifyMessage(string authString, string messageId, Uri baseUrl, string boxId)
		{
            var endpoint = new Uri(baseUrl, "/V1/Messages/GetInboxMessage");
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", authString);
            request.AddParameter("boxId", boxId);
            request.AddParameter("messageId", messageId);
            IRestResponse response = client.Execute(request);
            string messageentity = response.Content;
            return messageentity;
        }
    }
} 