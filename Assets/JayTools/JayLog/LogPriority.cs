using System;

namespace JayLog
{
    /// <summary>
    /// A list of example log priorities used for filtering log messages across the project.
    /// You can change the list however you want to suit your needs
    /// but remember about a correct order of the values.
    /// </summary>
    
    [Flags]
    public enum LogPriority
    {
        Zero = 1,
        Low = 1 << 1,
        Medium = 1 << 2,
        High = 1 << 3,
        Exception = 1 << 4,
    }
}