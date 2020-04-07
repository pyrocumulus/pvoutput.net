using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using NUnit.Framework;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Tests.Client
{
    [TestFixture]
    public class PVOutputExceptionTests
    {
        public static IEnumerable DefaultExceptionTests
        {
            get
            {
                yield return new TestCaseData(new PVOutputException());
                yield return new TestCaseData(new PVOutputException("Test"));
                yield return new TestCaseData(new PVOutputException("Test", new InvalidOperationException()));
            }
        }

        [Test]
        [TestCaseSource(typeof(PVOutputExceptionTests), "DefaultExceptionTests")]
        public void Default_Has_DefaultStatusCode(PVOutputException exception)
        {
            Assert.AreEqual((HttpStatusCode)0, exception.StatusCode);
        }

        public static IEnumerable WithStatusCodeExceptionTests
        {
            get
            {
                yield return new TestCaseData(new PVOutputException(HttpStatusCode.Unauthorized)).Returns(HttpStatusCode.Unauthorized);
                yield return new TestCaseData(new PVOutputException(HttpStatusCode.TooManyRequests, "No donation")).Returns(HttpStatusCode.TooManyRequests);
            }
        }

        [Test]
        [TestCaseSource(typeof(PVOutputExceptionTests), "WithStatusCodeExceptionTests")]
        public HttpStatusCode WithStatusCode_Sets_StatusCode(PVOutputException exception)
        {
            return exception.StatusCode;
        }
    }
}
