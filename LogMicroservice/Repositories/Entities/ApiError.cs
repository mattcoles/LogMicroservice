using System.Collections.Generic;

namespace LogMicroservice.Repositories.Entities
{
    /// <summary>
    /// ApiError entity
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// The error status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// the error list of errors
        /// </summary>
        public List<ErrorDetail> Errors { get; set; }
    }

    /// <summary>
    /// Error detail entity
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// The error detail title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the error detail error message
        /// </summary>
        public string Message { get; set; }
    }
}