using LogMicroservice.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogMicroservice.Repositories.Interfaces
{
    /// <summary>
    /// Interface for Log Repository 
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// Processes a log from an external service and writes it to a text file
        /// </summary>
        /// <param name="log">The log to process</param>
        /// <returns></returns>
        Task<IActionResult> ProcessLog(Log log);
    }
}