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
        public string BoxId = "e4e8b56d-3390-4e29-b7f5-169a83efacab";
        public string Filepath = "C:\\Tests\\Files\\ORDERS";

        [Test]
        public void Authenticate()
        {
            var authenitcate = new ApiHelper();
            var token = authenitcate.GetToken(ApiClientId, Login, Password, BaseUrl);
            Assert.That(token, Does.EndWith("=="));
        }

        [Test]
        public void SendingOrders()
        {
            var sendingorders = new ApiHelper();
            var token = sendingorders.GetToken(ApiClientId, Login, Password, BaseUrl);
            var messageid = sendingorders.SendMessage(BaseUrl, Filepath, BoxId, ApiClientId, token);
            Assert.That(messageid, Is.Not.Null);
        }


        [Test]
        public void ResendingOrders()
        {
            var resendingorders = new ApiHelper();
            var token = resendingorders.GetToken(ApiClientId, Login, Password, BaseUrl);
            var messageid = resendingorders.SendMessage(BaseUrl, Filepath, BoxId, ApiClientId, token);
            string undeliverymsg = resendingorders.VerifyMessage(token, messageid, BaseUrl, BoxId, ApiClientId);
            StringAssert.AreEqualIgnoringCase("Переданный файл пуст", undeliverymsg);
    }
    }
}