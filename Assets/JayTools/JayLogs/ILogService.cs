﻿namespace JayTools.JayLogs
{
    public interface ILogService
    {
        /// <summary>
        /// Logs a message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        void Log(object message, LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other);

        /// <summary>
        /// Logs a warning message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        void LogWarning(object message, LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other);

        /// <summary>
        /// Logs an error message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        void LogError(object message, LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other);

        /// <summary>
        /// Logs an exception message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        void LogException(object message, LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other);
        
        /// <summary>
        /// Logs a separator string to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="priority">The priority of the break. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the break. Supports multiple categories at once using "|" bitwise operator.</param>
        void LogBreak(LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other);

        /// <summary>
        /// Opens the log text file
        /// </summary>
        /// <param name="forceSave"></param>
        void OpenLogFile(bool forceSave);
    }
}