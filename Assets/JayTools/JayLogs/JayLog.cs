﻿namespace JayTools.JayLogs
{
    /// <summary>
    /// Logs game messages in Unity console and in a text file. Filters logs using Log Categories and Log Priorities.
    /// Built upon standard Unity logging system.
    /// Static access to chosen Log Service
    /// </summary>
    public static class JayLog
    {
        #region Variables

        private static ILogService logService;

        private static ILogService LogService
        {
            get => logService ?? (logService = new JayLogService());
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Logs a message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public static void Log(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            LogService.Log(message, priority, category);
        }

        /// <summary>
        /// Logs a warning message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public static void LogWarning(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            LogService.LogWarning(message, priority, category);
        }
        
        /// <summary>
        /// Logs an error message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public static void LogError(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            LogService.LogError(message, priority, category);
        }
        
        /// <summary>
        /// Logs an exception message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public static void LogException(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            LogService.LogException(message, priority, category);
        }
        
        /// <summary>
        /// Logs a separator string to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="priority">The priority of the break. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the break. Supports multiple categories at once using "|" bitwise operator.</param>
        public static void LogBreak(LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other)
        {
            LogService.LogBreak(priority, category);
        }

        /// <summary>
        /// Opens the log text file
        /// </summary>
        public static void OpenLogFile(bool forceSave)
        {
            LogService.OpenLogFile(forceSave);
        }

        #endregion
    }
}