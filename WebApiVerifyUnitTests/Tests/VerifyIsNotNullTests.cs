using NUnit.Framework;
using System.Web.Http;
using WebApiVerify;

namespace WebApiVerifyUnitTests.Tests
{
    public class VerifyIsNotNullTests : BaseVerifyTests
    {
        [Test]
        public void WhenNullIsPassed_ThrowsHttpResponseException()
        {
            var ex = Assert.Throws<HttpResponseException>(() =>
                Verify.It.IsNot.Null(null, Msg));

            Assert.AreEqual(Msg, ex.Response.ReasonPhrase);
            Assert.AreEqual(Code, ex.Response.StatusCode);
        }

        [Test]
        public void WhenNotNullIsPassed_DoesNotThrowAnException()
        {
            Assert.DoesNotThrow(() =>
                Verify.It.IsNot.Null(new object(), Msg));
        }
    }
}
