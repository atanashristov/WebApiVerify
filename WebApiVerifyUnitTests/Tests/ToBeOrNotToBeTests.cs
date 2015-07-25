using NUnit.Framework;
using System;
using WebApiVerify;

namespace WebApiVerifyUnitTests.Tests
{
    public class ToBeOrNotToBeTests : BaseVerifyTests
    {
        [Test]
        public void Tobe_IsNotSetByDefault()
        {
            Assert.Throws<ArgumentException>(() =>
                Verify.It.Null(null, Msg));

            Assert.Throws<ArgumentException>(() =>
                Verify.It.True(() => true, Msg));
        }


        [Test]
        public void ClauseIsNot_NegatesVerification()
        {
            Assert.DoesNotThrow(() =>
                Verify.It.IsNot.Null(new object(), Msg));

            Assert.DoesNotThrow(() =>
                Verify.It.IsNot.True(() => false, Msg));
        }

        [Test]
        public void ClauseIs_AndClauseIsNot_CanBeChained()
        {
            Assert.DoesNotThrow(() =>
                Verify.It
                .Is
                    .Null(null, Msg)
                    .Null(null, Msg)
                    .Null(null, Msg)
                .IsNot
                    .Null(new object(), Msg)
                    .Null(new object(), Msg)
                    .Null(new object(), Msg)
                .Is
                    .Null(null, Msg)
                    .Null(null, Msg)
                    .Null(null, Msg)
                .IsNot
                    .Null(new object(), Msg)
                    .Null(new object(), Msg)
                    .Null(new object(), Msg)
                );
        }

    }
}
