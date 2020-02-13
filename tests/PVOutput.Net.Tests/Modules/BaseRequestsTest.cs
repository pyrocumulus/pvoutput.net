using NUnit.Framework;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Tests.Modules
{
    public class BaseRequestsTest
    {
        protected void AssertStandardResponse<TResponseContentType>(PVOutputResponse<TResponseContentType> response)
        {
            Assert.Multiple(() =>
            {
                Assert.IsNull(response.Error);
                Assert.IsTrue(response.HasValue);
                Assert.IsTrue(response.IsSuccess);
                Assert.IsNotNull(response.Value);
            });
        }

        protected void AssertStandardResponse<TResponseContentType>(PVOutputArrayResponse<TResponseContentType> response)
        {
            Assert.Multiple(() =>
            {
                Assert.IsNull(response.Error);
                Assert.IsTrue(response.HasValues);
                Assert.IsTrue(response.IsSuccess);
                Assert.IsNotNull(response.Values);
            });
        }
    }
}
