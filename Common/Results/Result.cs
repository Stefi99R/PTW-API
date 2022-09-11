namespace Common.Results
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Class that is used to wrap up result from execution methods, exceptions, nulls or empty results.
    /// Result can be success or failure.
    /// </summary>
    public class Result : ResultCommonLogic
    {
        private Result() : base(ResultType.Ok, isFailure: false, message: string.Empty)
        {
        }

        internal Result(ResultType resultType, string message) : base(resultType, isFailure: true, message: message)
        {
        }

        /// <summary>
        /// Returns a Result object indicating that the operation was successful.
        /// Usually used when the method returns positive outcome of the execution.
        /// </summary>
        /// <returns>Result object without data.</returns>
        public static Result Ok() =>
            new Result();

        /// <summary>
        /// Returns an error result with result type conflicted. 
        /// Usually used when execution performs already done or duplicate operation.
        /// </summary>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result Conflicted(string message) =>
            new Result(ResultType.Conflicted, message);

        /// <summary>
        /// Returns an error result with result type failed.
        /// Usually used when internal server error has occurred in the code.
        /// </summary>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result Failed(string message) =>
            new Result(ResultType.InternalError, message);

        /// <summary>
        /// Returns an error result with result type forbidden.
        /// Usually used when the action is forbidden to be performed.
        /// </summary>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result Forbidden(string message) =>
            new Result(ResultType.Forbidden, message);

        /// <summary>
        /// Returns an error result with result type invalid.
        /// Usually used when the method has done some validations of the data and the data was invalid.
        /// </summary>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result Invalid(string message) =>
            new Result(ResultType.Invalid, message);

        /// <summary>
        /// Returns an error result with result type not found.
        /// Usually used when the method cannot found provided data that was requested.
        /// </summary>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result NotFound(string message) =>
            new Result(ResultType.NotFound, message);

        /// <summary>
        /// Returns an error result object for result type unauthorized error.
        /// Usually used when execution of the method is unauthorized by the party.
        /// </summary>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result Unauthorized(string message) =>
            new Result(ResultType.Unauthorized, message);

        // Generics

        /// <summary>
        /// Returns a Result object indicating that the operation was successful.
        /// Usually used when the method returns positive outcome of the execution.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="value">Data to be returned.</param>
        /// <returns>Result object with data from execution.</returns>
        public static Result<T> Ok<T>(T value) =>
            new Result<T>(value);

        /// <summary>
        /// Returns an error result with result type conflicted. 
        /// Usually used when execution performs already done or duplicate operation.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns>Result object without data.</returns>
        public static Result<T> Conflicted<T>(string message) =>
            new Result<T>(ResultType.Conflicted, message);

        /// <summary>
        /// Returns an error result with result type failed.
        /// Usually used when internal server error has occurred in the code.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns></returns>
        public static Result<T> Failed<T>(string message) =>
            new Result<T>(ResultType.InternalError, message);

        /// <summary>
        /// Returns an error result with result type forbidden.
        /// Usually used when the action is forbidden to be performed.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns></returns>
        public static Result<T> Forbidden<T>(string message) =>
            new Result<T>(ResultType.Forbidden, message);

        /// <summary>
        /// Returns an error result with result type invalid.
        /// Usually used when the method has done some validations of the data and the data was invalid.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns></returns>
        public static Result<T> Invalid<T>(string message) =>
            new Result<T>(ResultType.Invalid, message);

        /// <summary>
        /// Returns an error result with result type not found.
        /// Usually used when the method cannot found provided data that was requested.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns></returns>
        public static Result<T> NotFound<T>(string message) =>
            new Result<T>(ResultType.NotFound, message);

        /// <summary>
        /// Returns an error result object for result type unauthorized error.
        /// Usually used when execution of the method is unauthorized by the party.
        /// </summary>
        /// <typeparam name="T">Generic type that is used for result.</typeparam>
        /// <param name="message">The code that should describe the failure.</param>
        /// <returns></returns>
        public static Result<T> Unauthorized<T>(string message) =>
            new Result<T>(ResultType.Unauthorized, message);

        /// <summary>
        /// Creates and returns a result with data provided from the ResultCommonLogic object that is passed.
        /// Usually used to transform generic data of X into Y result object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<T> FromError<T>(ResultCommonLogic result) =>
            new Result<T>(result.ResultType, result.Message);

        /// <summary>
        /// Method that returns OK (success) result if all the supplied result are not failure.
        /// Otherwise will return the first failed result object.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result FirstFailureOrOk(params Result[] results)
        {
            if (results.Any(f => f.IsFailure))
            {
                return results.First(f => f.IsFailure);
            }

            return Ok();
        }

        /// <summary>
        /// Method that returns OK (success) result if all the supplied result are not failure.
        /// Otherwise will return the first failed result object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result FirstFailureOrOk<T>(IEnumerable<Result<T>> results)
        {
            if (results.Any(x => x.IsFailure))
            {
                return results.First(x => x.IsFailure);
            }

            return Ok();
        }
    }

    /// <summary>
    /// Class that is used to wrap up result from execution methods, exceptions, nulls or empty results.
    /// Result can be success or failure.
    /// </summary>
    public class Result<T> : ResultCommonLogic
    {
        /// <summary>
        /// Returns true if data object is empty, otherwise false.
        /// </summary>
        public bool IsEmpty => Value?.Equals(Empty) ?? true;

        /// <summary>
        /// Contains the data of the object that was used as generic.
        /// </summary>
        public T Value { get; }

        private static T Empty => default(T)!;

        internal Result(ResultType resultType, string message) : base(resultType, isFailure: true, message: message)
        {
            Value = Empty;
        }

        internal Result(T value) : base(ResultType.Ok, isFailure: false, message: string.Empty)
        {
            Value = value;
        }

        public static implicit operator T(Result<T> result) => result.Value;

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Result.Ok();
            }

            return new Result(result.ResultType, result.Message);
        }
    }

    [DebuggerStepThrough]
    public abstract class ResultCommonLogic
    {
        private readonly string _message;

        /// <summary>
        /// Returns true if result object is failure, otherwise false.
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Returns true if result object is success, otherwise false.
        /// </summary>
        public bool IsSuccess => !IsFailure;

        /// <summary>
        /// Returns true if result object is failure with status code not found, otherwise false.
        /// </summary>
        public bool IsNotFound => IsFailure && HttpStatusCode == HttpStatusCode.NotFound;

        /// <summary>
        /// Message that usually shows the code of failure.
        /// </summary>
        public string Message => _message;

        /// <summary>
        /// Type of the result shown as enumeration
        /// </summary>
        public ResultType ResultType { get; }

        /// <summary>
        /// HTTP status code provided by result type.
        /// </summary>
        public HttpStatusCode HttpStatusCode
        {
            get
            {
                HttpStatusCode statusCode;

                switch (ResultType)
                {
                    case ResultType.Ok:
                        statusCode = HttpStatusCode.OK;
                        break;
                    case ResultType.NotFound:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    case ResultType.Forbidden:
                        statusCode = HttpStatusCode.Forbidden;
                        break;
                    case ResultType.Conflicted:
                        statusCode = HttpStatusCode.Conflict;
                        break;
                    case ResultType.Invalid:
                        statusCode = HttpStatusCode.NotAcceptable;
                        break;
                    case ResultType.Unauthorized:
                        statusCode = HttpStatusCode.Unauthorized;
                        break;
                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                }

                return statusCode;
            }
        }

        protected ResultCommonLogic(ResultType resultType, bool isFailure, string message)
        {
            if (isFailure)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(nameof(message), "There must be error message for failure.");
                }

                if (resultType == ResultType.Ok)
                {
                    throw new ArgumentException("There should be error type for failure.", nameof(resultType));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentException("There should be no error message for success.", nameof(message));
                }

                if (resultType != ResultType.Ok)
                {
                    throw new ArgumentException("There should be no error type for success.", nameof(resultType));
                }
            }

            ResultType = resultType;
            IsFailure = isFailure;
            _message = message;
        }
    }
}
