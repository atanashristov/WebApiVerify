using NUnit.Framework;
using System.Net;

namespace WebApiVerifyUnitTests.Tests
{
    [TestFixture]
    public abstract class BaseVerifyTests
    {
        protected const HttpStatusCode Code = HttpStatusCode.BadRequest;
        protected const string Msg = "bad request data";
    }
}
