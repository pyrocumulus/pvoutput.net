using NUnit.Framework;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Tests.Modules
{
    public class BaseRequestsTest
    {
        protected static void AssertStandardResponse<TResponseContentType>(PVOutputResponse<TResponseContentType> response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response.Error, Is.Null);
                Assert.That(response.HasValue, Is.True);
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Value, Is.Not.Null);
            });
        }

        protected static void AssertStandardResponse<TResponseContentType>(PVOutputArrayResponse<TResponseContentType> response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response.Error, Is.Null);
                Assert.That(response.HasValues, Is.True);
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Values, Is.Not.Null);
            });
        }

        protected static void AssertStandardResponse(PVOutputBasicResponse response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response.Error, Is.Null);
                Assert.That(response.IsSuccess, Is.True);
            });
        }
    }
}
