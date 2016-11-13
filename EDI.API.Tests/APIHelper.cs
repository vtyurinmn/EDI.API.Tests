using System;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace EDI.API.Tests
{
    public class ApiHelper
    {
        public string ApiClientId;
        public string Login;
        public string Password;
        public string BoxId;
        public string Filename;
        public string Filepath;

        public string FormPreAuthString()
		{
            string preAuthString =
                $"KonturEdiAuth konturediauth_api_client_id={ApiClientId},konturediauth_login={Login},konturediauth_password={Password}";
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

		public string FormAuthString(string token)
		{
		    if (token == null) throw new ArgumentNullException(nameof(token));
		    string authString = $"KonturEdiAuth konturediauth_api_client_id={ApiClientId},konturediauth_token={token}";
			return authString;
		}

		public string SendMessage(string authString, Uri baseUrl)
		{
		    if (authString == null) throw new ArgumentNullException(nameof(authString));
            var endpoint = new Uri(baseUrl, "/V1/Messages/SendMessage");
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST);
			request.AddHeader("authorization", authString);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("boxId", BoxId);
            request.AddFile(Filename, Filepath);
            IRestResponse response = client.Execute(request);
            string message = response.Content;

            dynamic json = JToken.Parse(message);
            var messageId = json.MessageId;
            return messageId;
		}

		public string VerifyMessage(string authString, string messageId, Uri baseUrl)
		{
            var endpoint = new Uri(baseUrl, "/V1/Messages/GetInboxMessage");
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", authString);
            request.AddParameter("boxId", BoxId);
            request.AddParameter("messageId", messageId);
            IRestResponse response = client.Execute(request);
            string messageentity = response.Content;
            return messageentity;
        }
    }
} 