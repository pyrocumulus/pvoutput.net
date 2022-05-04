using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules.Implementations;
using PVOutput.Net.Responses;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Handler
{
    [TestFixture]
    public class BaseRequestHandlingTests
    {
        [Test]
        public async Task RequestHandler_OnRequest_SetsRequiredHeaders()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .WithHeaders(new Dictionary<string, string>()
                        {
                            { "X-Pvoutput-Apikey", TestConstants.PVOUTPUT_API_KEY },
                            { "X-Pvoutput-SystemId", TestConstants.PVOUTPUT_SYSTEM_ID.ToString() },
                            { "X-Rate-Limit", "1" }
                        })
                        .RespondPlainText("");

            PVOutputResponse<ISystem> response = await client.System.GetOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();
            Assert.That(response.ToBoolean(), Is.True);
        }

        [Test]
        public void DefaultClient_OnErrorResponse_ThrowsException()
        {
            HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
            var responseContent = "Invalid API Key";

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            var exception = Assert.ThrowsAsync<PVOutputException>(async () =>
            {
                _ = await client.System.GetOwnSystemAsync();
            });

            testProvider.VerifyNoOutstandingExpectation();
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo(responseContent));
                Assert.That(exception.StatusCode, Is.EqualTo(statusCode));
            });
        }

        [Test]
        public async Task DefaultClient_OnErrorArrayResponse_ThrowsException()
        {
            HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
            var responseContent = "Invalid API Key";

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("search.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            PVOutputArrayResponse<ISystemSearchResult> response = await client.Search.SearchAsync("test");
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.False);
                Assert.That(response.Error.Message, Is.EqualTo(responseContent));
                Assert.That(response.Error.StatusCode, Is.EqualTo(statusCode));
            });
        }

        [Test]
        public void ExecuteSingleItemRequest_OnCancellation_DoesNotThrowNullReferenceException()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("getsystem.jsp")
                .Respond(async _ =>
                {
                    // This should let the client hang on ExecuteSingleItemRequest until it's canceled.
                    await Task.Delay(1000);

                    throw new InvalidOperationException("Should have been canceled");
                });

            Task task = null;

            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                task = Task.Run(() => client.System.GetOwnSystemAsync(cancellationTokenSource.Token));

                // Now that the client started running above, cancel it.
                cancellationTokenSource.Cancel();

                await task;
            });

            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(task.IsCanceled, Is.True);
                Assert.That(cancellationTokenSource.IsCancellationRequested, Is.True);
            });
        }

        [Test]
        public void ExecuteArrayRequest_OnCancellation_DoesNotThrowNullReferenceException()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("search.jsp")
                .Respond(async _ =>
                {
                    // This should let the client hang on ExecuteSingleItemRequest until it's canceled.
                    await Task.Delay(1000);

                    throw new InvalidOperationException("Should have been canceled");
                });

            Task task = null;

            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                task = Task.Run(() => client.Search.SearchAsync("test", cancellationToken: cancellationTokenSource.Token));

                // Now that the client started running above, cancel it.
                cancellationTokenSource.Cancel();

                await task;
            });

            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(task.IsCanceled, Is.True);
                Assert.That(cancellationTokenSource.IsCancellationRequested, Is.True);
            });
        }

        [Test]
        public void ExecutePostRequest_OnCancellation_DoesNotThrowNullReferenceException()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("postsystem.jsp")
                .Respond(async _ =>
                {
                    // This should let the client hang on ExecuteSingleItemRequest until it's canceled.
                    await Task.Delay(1000);

                    throw new InvalidOperationException("Should have been canceled");
                });

            Task task = null;

            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                task = Task.Run(() => client.System.PostSystem(systemId: 42, cancellationToken: cancellationTokenSource.Token));

                // Now that the client started running above, cancel it.
                cancellationTokenSource.Cancel();

                await task;
            });

            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(task.IsCanceled, Is.True);
                Assert.That(cancellationTokenSource.IsCancellationRequested, Is.True);
            });
        }


        [Test]
        public async Task ClientWithNoThrowOption_OnErrorResponse_ReturnsErrorResponse()
        {
            HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
            var responseContent = "Invalid API Key";

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            PVOutputResponse<ISystem> response = await client.System.GetOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.False);
                Assert.That(response.Error.Message, Is.EqualTo(responseContent));
                Assert.That(response.Error.StatusCode, Is.EqualTo(statusCode));
            });
        }

        [Test]
        public async Task ClientWithNoThrowOption_OnFormattedErrorResponse_ReturnsErrorResponse()
        {
            HttpStatusCode statusCode = HttpStatusCode.Unauthorized;

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", "Too many request : Rate exceeded");

            PVOutputResponse<ISystem> response = await client.System.GetOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.False);
                Assert.That(response.Error.Message, Is.EqualTo("Rate exceeded"));
                Assert.That(response.Error.StatusCode, Is.EqualTo(statusCode));
            });
        }

        [Test]
        public async Task Response_WithApiRateInformation_ParsesCorrectInformation()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            var resetTimeStamp = new DateTime(2020, 3, 15, 11, 20, 0);
            var offset = (DateTimeOffset)DateTime.SpecifyKind(resetTimeStamp, DateTimeKind.Utc);

            var responseHeaders = new Dictionary<string, string>()
                        {
                            { "X-Rate-Limit-Remaining", "156" },
                            { "X-Rate-Limit-Limit", "300" },
                            { "X-Rate-Limit-Reset", offset.ToUnixTimeSeconds().ToString() }
                        };

            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(responseHeaders, "text/plain", "");

            PVOutputResponse<ISystem> response = await client.System.GetOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.That(response.ApiRateInformation.LimitRemaining, Is.EqualTo(156));
                Assert.That(response.ApiRateInformation.CurrentLimit, Is.EqualTo(300));
                Assert.That(response.ApiRateInformation.LimitResetAt, Is.EqualTo(resetTimeStamp));
            });
        }

        [Test]
        public void BaseResponse_IsEquivalentTo_ReturnsEquivelanceNotEquals()
        {
            var response1 = new PVOutputBasicResponse() { IsSuccess = true, SuccesMessage = "Yay" };
            var response2 = new PVOutputBasicResponse() { IsSuccess = true, SuccesMessage = "Yay" };
            var response3 = new PVOutputBasicResponse() { IsSuccess = false, SuccesMessage = "Nay" };
            PVOutputBasicResponse response4 = null;

            Assert.Multiple(() =>
            {
#pragma warning disable NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
                Assert.That(response1.Equals(response2), Is.False); // Purposefully not test through Is.Not.EqualTo
                Assert.That(response1.Equals(response3), Is.False); // Purposefully not test through Is.Not.EqualTo
#pragma warning restore NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
                Assert.That(response1.IsEquivalentTo(response2), Is.True);
                Assert.That(response1.IsEquivalentTo(response3), Is.False);
                Assert.That(response1.IsEquivalentTo(response4), Is.False);
            });
        }

        [Test]
        public void PVOutputResponse_IsEquivalentTo_ReturnsEquivelanceNotEquals()
        {
            var response1 = new PVOutputResponse<IStatus>() { IsSuccess = true, Value = new Status() };
            var response2 = new PVOutputResponse<IStatus>() { IsSuccess = true, Value = new Status() };
            var response3 = new PVOutputResponse<IStatus>() { IsSuccess = false };

            Assert.Multiple(() =>
            {
                Assert.That(response1.IsEquivalentTo(response2), Is.True);
                Assert.That(response1.IsEquivalentTo(response3), Is.False);
            });
        }

        [Test]
        public void PVOutputArrayResponse_IsEquivalentTo_ReturnsEquivelanceNotEquals()
        {
            var response1 = new PVOutputArrayResponse<IStatus>() { IsSuccess = true, Values = new List<Status>() };
            var response2 = new PVOutputArrayResponse<IStatus>() { IsSuccess = true, Values = new List<Status>() };
            var response3 = new PVOutputArrayResponse<IStatus>() { IsSuccess = false };

            Assert.Multiple(() =>
            {
                Assert.That(response1.IsEquivalentTo(response2), Is.True);
                Assert.That(response1.IsEquivalentTo(response3), Is.False);
            });
        }

        [Test]
        public void PVOutputApiError_IsEquivalentTo_ReturnsEquivelanceNotEquals()
        {
            var error1 = new PVOutputApiError() { StatusCode = HttpStatusCode.Unauthorized, Message = "Donation mode required." };
            var error2 = new PVOutputApiError() { StatusCode = HttpStatusCode.Unauthorized, Message = "Donation mode required." };
            var error3 = new PVOutputApiError() { StatusCode = HttpStatusCode.BadRequest, Message = "Unknown." };

            Assert.Multiple(() =>
            {
                Assert.That(error1.IsEquivalentTo(error2), Is.True);
                Assert.That(error1.IsEquivalentTo(error3), Is.False);
            });
        }

        [Test]
        public void BaseResponse_ImplicitBoolConversion_ReturnsSuccessState()
        {
            var response1 = new PVOutputBasicResponse() { IsSuccess = true, SuccesMessage = "Yay" };
            var response2 = new PVOutputBasicResponse() { IsSuccess = false, SuccesMessage = "Nay" };
            PVOutputBasicResponse response3 = null;

            Assert.Multiple(() =>
            {
                Assert.That((bool)response1, Is.True);
                Assert.That((bool)response2, Is.False);
                Assert.That((bool)response3, Is.False);
            });
        }

        [Test]
        public async Task BaseObjectReader_WithNullStream_ReturnsDefaultForType()
        {
            IObjectStringReader<IStatus> reader = StringFactoryContainer.CreateObjectReader<IStatus>();
            IStatus content = await reader.ReadObjectAsync(stream: null, cancellationToken: default).ConfigureAwait(false);

            Assert.That(content, Is.Null);
        }

        [Test]
        public async Task BaseArrayReader_WithNullStream_ReturnsDefaultForType()
        {
            IArrayStringReader<ISystemSearchResult> reader = StringFactoryContainer.CreateArrayReader<ISystemSearchResult>();
            IEnumerable<ISystemSearchResult> content = await reader.ReadArrayAsync(stream: null, cancellationToken: default).ConfigureAwait(false);

            Assert.That(content, Is.Null);
        }

        [Test]
        public async Task CharacterDelimitedArrayReader_WithNullStream_ReturnsDefaultForType()
        {
            IArrayStringReader<ISystemSearchResult> reader = new CharacterDelimitedArrayStringReader<ISystemSearchResult>();
            IEnumerable<ISystemSearchResult> content = await reader.ReadArrayAsync(stream: null, cancellationToken: default).ConfigureAwait(false);

            Assert.That(content, Is.Null);
        }

        [Test]
        public async Task LineDelimitedArrayReader_WithNullStream_ReturnsDefaultForType()
        {
            IArrayStringReader<ISystemSearchResult> reader = new LineDelimitedArrayStringReader<ISystemSearchResult>();
            IEnumerable<ISystemSearchResult> content = await reader.ReadArrayAsync(stream: null, cancellationToken: default).ConfigureAwait(false);

            Assert.That(content, Is.Null);
        }
    }
}
