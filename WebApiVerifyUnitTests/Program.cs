using System;
using WebApiVerifyUnitTests.Tests;

namespace WebApiVerifyUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running SpecificExceptionTests ...");

            new SpecificExceptionTests().TestThat_Verify_CanThrowSpecificExceptions();

            new ToBeOrNotToBeTests().Tobe_IsNotSetByDefault();
            new ToBeOrNotToBeTests().ClauseIsNot_NegatesVerification();
            new ToBeOrNotToBeTests().ClauseIs_AndClauseIsNot_CanBeChained();

            new VerifyIsNotNullTests().WhenNullIsPassed_ThrowsHttpResponseException();
            new VerifyIsNotNullTests().WhenNotNullIsPassed_DoesNotThrowAnException();

            Console.WriteLine("OK.");

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press <Enter> to close.");
                Console.ReadLine();
            }
        }
    }
}
