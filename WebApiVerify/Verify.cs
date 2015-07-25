using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiVerify
{
    public class Verify
    {
        public static Verify It { get { return new Verify(); } }

        private bool? _tobe;

        /// <summary>
        /// Once set, which is by default, any verification in the chain after that is evaluated to be true.
        /// </summary>
        /// <remarks>is set to true by default</remarks>
        public Verify Is
        {
            get
            {
                _tobe = true;
                return this;
            }
        }

        /// <summary>
        /// Once set, any verification in the chain after that is evaluated to be false.
        /// </summary>
        public Verify IsNot
        {
            get
            {
                _tobe = false;
                return this;
            }
        }

        /// <summary>
        /// Verify for null object. Throws HttpResponseException with specific HttpStatusCode or 400 - BadRequest by default.
        /// </summary>
        /// <param name="obj">object to verify for null</param>
        /// <param name="errReasonPhrase">Response phrase for error</param>
        /// <param name="errStatusCode">Status code for error</param>
        /// <returns>the Verify object for chaining</returns>
        public Verify Null(object obj, string errReasonPhrase, HttpStatusCode errStatusCode = HttpStatusCode.BadRequest)
        {
            True(() => obj == null, errReasonPhrase, errStatusCode);
            return this;
        }

        /// <summary>
        /// Verify for null object. 
        /// Throws specific exception when verification failed. 
        /// Use the VerifyExceptionFilter to map to appropriate HttpStatusCode.
        /// </summary>
        /// <typeparam name="TException">Type of the exception to throw when verification failed.</typeparam>
        /// <param name="obj">object to verify for null</param>
        /// <param name="exception">exception functor</param>
        /// <returns>the Verify object for chaining</returns>
        public Verify Null<TException>(object obj, Func<TException> exception)
            where TException : Exception
        {
            return True(() => obj == null, exception);
        }

        /// <summary>
        /// Verify if the condition is true.
        /// Throws HttpResponseException with specific HttpStatusCode or 400 - BadRequest by default.
        /// </summary>
        /// <param name="test">condition functor</param>
        /// <param name="errReasonPhrase">Response phrase for error</param>
        /// <param name="errStatusCode">Status code for error, 400 - Bad Request by default</param>
        /// <returns>the Verify object for chaining</returns>
        public Verify True(Func<bool> test, string errReasonPhrase, HttpStatusCode errStatusCode = HttpStatusCode.BadRequest)
        {
            return True(test, () =>
            {
                var msg = new HttpResponseMessage
                {
                    StatusCode = errStatusCode,
                    ReasonPhrase = errReasonPhrase,
                };

                return new HttpResponseException(msg);
            });
        }

        /// <summary>
        /// Verify if the condition is true.
        /// Throws specific exception when verification failed. 
        /// Use the VerifyExceptionFilter to map to appropriate HttpStatusCode.
        /// </summary>
        /// <typeparam name="TException">Type of the exception to throw when verification failed.</typeparam>
        /// <param name="test">condition functor</param>
        /// <param name="exception">exception functor</param>
        /// <returns>the Verify object for chaining</returns>
        public Verify True<TException>(Func<bool> test, Func<TException> exception)
            where TException : Exception
        {
            if (!_tobe.HasValue)
                throw new ArgumentException("Incorrect syntax. Please start verification with Verify.It.Is... or Verify.It.IsNot...");

            if (test() != _tobe)
            {
                throw exception();
            }

            return this;
        }

    }
}
