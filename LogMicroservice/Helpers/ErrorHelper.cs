using LogMicroservice.Repositories.Entities;
using System;
using System.Collections.Generic;

namespace LogMicroservice.Helpers
{
    public static class ErrorHelper
    {
        /// <summary>
        /// Creates an error object
        /// </summary>
        /// <param name="statusCode">The error status code</param>
        /// <param name="errors">The list of errors</param>
        /// <returns>An Error object</returns>
        public static ApiError CreateErrorResult(int statusCode, List<ErrorDetail> errors)
        {
            return new ApiError
            {
                StatusCode = statusCode,
                Errors = errors
            };
        }

        /// <summary>
        /// Overloaded method to create an error object with a LogError
        /// </summary>
        /// <param name="statusCode">The error status code</param>
        /// <param name="error">The LogError</param>
        /// <returns></returns>
        public static ApiError CreateErrorResult(int statusCode, LogError error)
        {
            return CreateErrorResult(statusCode, new List<ErrorDetail> {
                new ErrorDetail {
                    Title = error.ToString(),
                    Message = error.GetEnumDescription()
                }
            });
        }

        /// <summary>
        /// Overloaded method to create an error object with a LogError & an excpetion
        /// </summary>
        /// <param name="statusCode">The error status code</param>
        /// <param name="error">The LogError</param>
        /// <param name="e">The exception</param>
        /// <returns></returns>
        public static ApiError CreateErrorResult(int statusCode, LogError error, Exception e)
        {
            return CreateErrorResult(statusCode, new List<ErrorDetail> {
                new ErrorDetail {
                    Title = error.ToString(),
                    Message = $"{error.GetEnumDescription()}: {GetExceptionMessages(e)}" 
                }
            });
        }

        /// <summary>
        /// Takes an Exception and returns a concatenated string of the exception messages.
        /// </summary>
        /// <param name="e">The exception object to check</param>
        /// <returns>A formatted string containing exception message and inner exception message if present</returns>
        public static string GetExceptionMessages(Exception e)
        {
            return $"{e.Message}{(e.InnerException != null ? ", " + e.InnerException.Message : null)}";
        }
    }
}