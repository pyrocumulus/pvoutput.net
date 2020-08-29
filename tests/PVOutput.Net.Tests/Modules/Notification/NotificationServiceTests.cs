using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Supply
{
    [TestFixture]
    public partial class NotificationServiceTests : BaseRequestsTest
    {
        [Test]
        public void Parameter_ApplicationId_CreatesCorrectUriParameters()
        {
            var request = new RegisterNotificationRequest() { ApplicationId = "my.first.application" };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["appid"], Is.EqualTo("my.first.application"));
        }

        [Test]
        public void Parameter_CallbackUrl_CreatesCorrectUriParameters()
        {
            var request = new RegisterNotificationRequest() { CallbackUri = new Uri("http://www.google.com/callmeback") };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["url"], Is.EqualTo("http://www.google.com/callmeback"));
        }

        [Test]
        public void Parameter_AlertType_CreatesCorrectUriParameters()
        {
            var request = new RegisterNotificationRequest() { AlertType = 11 };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["type"], Is.EqualTo(11));
        }

        [Test]
        public void Parameter_ApplicationId_Deregister_CreatesCorrectUriParameters()
        {
            var request = new DeregisterNotificationRequest() { ApplicationId = "my.first.application" };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["appid"], Is.EqualTo("my.first.application"));
        }

        [Test]
        public void Parameter_AlertType_Deregister_CreatesCorrectUriParameters()
        {
            var request = new DeregisterNotificationRequest() { AlertType = 11 };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["type"], Is.EqualTo(11));
        }


        [Test]
        public async Task NotificationService_RegisterNotification_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(REGISTERNOTIFICATION_URL)
                        .WithQueryString("appid=my.application.id&type=14&url=http://www.google.com/callmeback")
                        .RespondPlainText("");

            var response =  await client.Notification.RegisterNotificationAsync("my.application.id", "http://www.google.com/callmeback", 14);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task NotificationService_RegisterNotificationViaUri_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(REGISTERNOTIFICATION_URL)
                        .WithQueryString("appid=my.application.id&type=17&url=http://www.microsoft.com/callmeback")
                        .RespondPlainText("");

            var response = await client.Notification.RegisterNotificationAsync("my.application.id", new Uri("http://www.microsoft.com/callmeback"), 17);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task NotificationService_DeregisterNotification_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(DEREGISTERNOTIFICATION_URL)
                        .WithQueryString("appid=my.application.id&type=24")
                        .RespondPlainText("");

            var response = await client.Notification.DeregisterNotificationAsync("my.application.id", 24);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public void ApplicationId_TooLong_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(REGISTERNOTIFICATION_URL)
                        .RespondPlainText("");

            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                _ = await client.Notification.RegisterNotificationAsync(new string('*', 101), "");
            });
        }


        [Test]
        public void CallbackUrl_TooLong_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(REGISTERNOTIFICATION_URL)
                        .RespondPlainText("");

            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                _ = await client.Notification.RegisterNotificationAsync("my.application.id", new string('*', 151));
            });
        }
    }
}
