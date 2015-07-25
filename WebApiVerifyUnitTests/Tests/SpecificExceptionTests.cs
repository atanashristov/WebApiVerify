using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security;
using WebApiVerify;

namespace WebApiVerifyUnitTests.Tests
{
    [TestFixture]
    public class SpecificExceptionTests
    {
        [Test]
        public void TestThat_Verify_CanThrowSpecificExceptions()
        {
            foreach (var test in GetTestData())
            {
                Console.WriteLine("Test: " + test.ExpectedException.Message);

                Exception exception = null;

                try
                {
                    Verify.It.IsNot.Null(null, () => test.ExpectedException);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                Assert.IsInstanceOf(test.ExpectedException.GetType(), exception);
                Assert.AreEqual(test.ExpectedException.Message, exception.Message);
            }
        }

        public IEnumerable<TestData> GetTestData()
        {
            yield return new TestData
            {
                ExpectedException = new Exception("We have problem right now, please try again later.")
            };

            yield return new TestData
            {
                ExpectedException = new ArgumentException("Please provide userId as greater than zero number.")
            };

            yield return new TestData
            {
                ExpectedException = new SecurityException("Attempt to access unauthorized!")
            };

            yield return new TestData
            {
                ExpectedException = new NotImplementedException("This controller is not ready yet.")
            };
        }

        public class TestData
        {
            public Exception ExpectedException { get; set; }
        }
    }
}
