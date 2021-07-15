using System;

namespace LogMicroservice.Repositories.Entities
{
    /// <summary>
    /// Log entity
    /// </summary>
    public class Log
    {
        /// <summary>
        /// The log id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The log date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The log message. Can be up to 255 characters in length
        /// </summary>
        public string Message { get; set; }
    }
}
