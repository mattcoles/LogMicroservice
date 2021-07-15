using System.ComponentModel;

namespace LogMicroservice.Repositories.Entities
{
    /// <summary>
    /// Log Error enumerations
    /// </summary>
    public enum LogError
    {
        [Description("Log id is requried")]
        ID_REQUIRED,

        [Description("Log Date is requried")]
        DATE_REQUIRED,

        [Description("Log message is required")]
        MESSAGE_REQUIRED,

        [Description("Log message cannot be more than 255 characters")]
        MESSAGE_TOO_LONG,

        [Description("Unable to write the log to file")]
        FILE_WRITE_ERROR,

        [Description("An unknown error has occured")]
        UNKNOWN_ERROR,

        [Description("An internal exception has occured")]
        EXCEPTION
    }
}