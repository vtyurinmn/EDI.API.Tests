using System;
using NUnit.Framework;

namespace EDI.API.Tests
{
    [TestFixture]

    public class ApiTest
    {
        public Uri BaseUrl = new Uri("http://test-edi-api.kontur.ru");
        public string Login = "i.a.kadochnikov@yandex.ru";
        public string Password = "P@ssw0rd";
        public string ApiClientId = "kadochnikov-302a38e2-c79c-45c6-af34-5ac20866114f";
        public string BoxId = "bccbecfb-a370-4f17-8873-8e05aaeb0bdc";
        public string Filename = "ORDERS";
        public string Filepath = "C:\\Tests\\Files\\ORDERS";

        [Test]
        public void Authenticate()
        {
            var authenitcate = new ApiHelper();
            var preauthstring = authenitcate.FormPreAuthString(ApiClientId, Login, Password);
            var token = authenitcate.GetToken(preauthstring, BaseUrl);
            Assert.That(token, Is.TypeOf<string>());
        }

        [Test]
        public void SendingOrders()
        {
            var sendingorders = new ApiHelper();
            var preauthstring = sendingorders.FormPreAuthString(ApiClientId, Login, Password);
            var token = sendingorders.GetToken(preauthstring, BaseUrl);
            var authstring = sendingorders.FormAuthString(ApiClientId, token);
            var message = sendingorders.SendMessage(authstring, BaseUrl, Filename, Filepath, BoxId);
            Assert.That(message.Contains("DocumentCirculationId"));
        }

        [Test]
        public void ResendingOrders()
        {
            Console.Out.WriteLine();
        }

        [Test]
        public void OutboxMessage()
        {

        }

        [Test]
        public void InboxMessage()
        {

        }

        [Test]
        public void VerifyOrdersNames()
        {

        }

        [Test]
        public void GetXlsxFile()
        {

        }

        [Test]
        public void ParseXlsxFile()
        {

        }
    }
}