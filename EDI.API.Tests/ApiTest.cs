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
        public string Filepath = "C:\\Tests\\Files\\";

        [Test]
        public void Authenticate()
        {
            var authenitcate = new ApiHelper();
            var authstring = authenitcate.FormPreAuthString();
            var token = authenitcate.GetToken(authstring, BaseUrl);
            Assert.That(token, Is.TypeOf<string>());
        }

        [Test]
        public void SendingOrders()
        {
            var sendingorders = new ApiHelper();
            var authstring = sendingorders.FormPreAuthString();
            var message = sendingorders.SendMessage(authstring, BaseUrl);
            Assert.That(string.IsNullOrEmpty(message));
        }

        [Test]
        public void ResendingOrders()
        {

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