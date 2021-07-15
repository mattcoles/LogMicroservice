using LogMicroservice.Helpers;
using LogMicroservice.Repositories.Entities;
using LogMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LogMicroservice.Repositories
{
    /// <summary>
    /// Repository for all Log methods
    /// </summary>
    public class LogRepository : ControllerBase, ILogRepository
    {
        // Get a reference to the environment.
        private readonly IWebHostEnvironment _env;

        // Constructor.
        public LogRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Processes a log from an external service and writes it to a text file
        /// </summary>
        /// <param name="log">The log to process</param>
        /// <returns>An IActionResult</returns>
        async Task<IActionResult> ILogRepository.ProcessLog(Log log)
        {
            try
            {
                // Validate the log file.
                var logErrors = ValidateLog(log);

                // Check for errors.
                if (logErrors == null)
                {
                    // Write the log to a text file.
                    if (await WriteLogToTextFile(log))
                    {
                        // Return a 201 Created & the processed log.
                        return Created("api/log", log);
                    }
                    else
                    {
                        // Create the error.
                        var error = ErrorHelper.CreateErrorResult(400, LogError.FILE_WRITE_ERROR);
                        
                        // Try and write to the error file.
                        await WriteLogToTextFile(log, error);
                        
                        // Return a bad request and a file save error.
                        return BadRequest(error);
                    }
                }
                else
                {
                    // Create the error.
                    var errors = ErrorHelper.CreateErrorResult(400, logErrors);

                    // Try and write to the error file.
                    await WriteLogToTextFile(log, errors);

                    // Return a bad request and the errors.
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                // Create the error.
                var error = ErrorHelper.CreateErrorResult(400, LogError.EXCEPTION, ex);

                // Try and write to the error file.
                await WriteLogToTextFile(log, error);

                // Return a bad request and the exception.
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Validates a log and returns a list of errors or null if no errors found
        /// </summary>
        /// <param name="log">The log to be validated</param>
        /// <returns>A list of errors or null</returns>
        private static List<ErrorDetail> ValidateLog(Log log)
        {
            var errors = new List<ErrorDetail>();

            if (log.Id == Guid.Empty)
            {
                errors.Add(new ErrorDetail { Title = LogError.ID_REQUIRED.ToString(), Message = LogError.ID_REQUIRED.GetEnumDescription() });
            }

            if (log.Date == default)
            {
                errors.Add(new ErrorDetail { Title = LogError.DATE_REQUIRED.ToString(), Message = LogError.DATE_REQUIRED.GetEnumDescription() });
            }

            if (log.Message == null)
            {
                errors.Add(new ErrorDetail { Title = LogError.MESSAGE_REQUIRED.ToString(), Message = LogError.MESSAGE_REQUIRED.GetEnumDescription() });
            }

            if (!string.IsNullOrEmpty(log.Message) && log.Message.Length > 255)
            {
                errors.Add(new ErrorDetail { Title = LogError.MESSAGE_TOO_LONG.ToString(), Message = LogError.MESSAGE_TOO_LONG.GetEnumDescription() });
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return null;
        }

        /// <summary>
        /// Write the log to a text file.
        /// </summary>
        /// <param name="log">The log to be writtten</param>
        /// <returns>A bool to indicate if the write to file succeded</returns>
        private async Task<bool> WriteLogToTextFile(Log log, ApiError error = null)
        {
            try
            {
                // Get the path to the Logs.txt
                var logPath = $@"{_env.ContentRootPath}\Logs\logs.txt";
                var errorPath = $@"{_env.ContentRootPath}\Logs\errors.txt";

                if (error != null)
                {
                    // Write the error to the error file.
                    using (StreamWriter writer = System.IO.File.AppendText(errorPath))
                    {
                        var errorsStr = string.Empty;
                        foreach (var e in error.Errors)
                        {
                            errorsStr += $"{e.Title}|";
                        }

                        // Remove the trailing pipe.
                        errorsStr = errorsStr.Remove(errorsStr.LastIndexOf('|'));

                        await writer.WriteLineAsync($"{DateTime.Now}|{error.StatusCode}|{errorsStr}" +
                            $"{(log.Id != Guid.Empty ? "|Log Id: " + log.Id : null)}{(!string.IsNullOrEmpty(log.Message) ? "|Log Message: " + log.Message : null)}");
                    }
                }
                else
                {
                    // Write the Log to the log file.
                    using (StreamWriter writer = System.IO.File.AppendText(logPath))
                    {
                        await writer.WriteLineAsync($"{log.Id}|{log.Date}|{log.Message}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Write failed - return false.
                return false;
            }
        }
    }
}