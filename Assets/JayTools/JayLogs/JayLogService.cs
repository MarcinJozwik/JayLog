﻿using UnityEngine;

 namespace JayTools.JayLogs
{
    /// <summary>
    /// Logs game messages in Unity console and in a text file. Filters logs using Log Categories and Log Priorities.
    /// Built upon standard Unity logging system.
    /// </summary>
    public partial class JayLogService : ILogService
    {
        #region Variables
        
        private static int priorityMask = 0;
        private static int categoryMask = 0;
        private static ILogger logger;

#if ENABLE_LOG_FILE
        private static ILogSave logSave;
#endif
        #endregion

        #region Constructor
        
        /// <summary>
        /// Initializes the logging system. This is the place where you can change behaviour of various aspects
        /// of the logging system to suit the needs of your project. You can change the internal logger (instead of using the Unity default one),
        /// the way filtering options are stored, and how the logs are saved. 
        /// </summary>
        public JayLogService()
        {
            logger = Debug.unityLogger;
            
            categoryMask = PlayerPrefs.GetInt(LogCategorySaveKey, 0);
            priorityMask = PlayerPrefs.GetInt(LogPrioritySaveKey, 0);

#if ENABLE_LOG_FILE
            logSave = new StringBuilderToTxtLogSave();
            logSave.Init();
#endif
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs a message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public void Log(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            InternalLog(message, LogType.Log, priority, category);
        }

        /// <summary>
        /// Logs a warning message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public void LogWarning(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            InternalLog(message, LogType.Warning, priority, category);
        }

        /// <summary>
        /// Logs an error message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public void LogError(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            InternalLog(message, LogType.Error, priority, category);
        }
        
        /// <summary>
        /// Logs an exception message to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        public void LogException(object message, LogPriority priority = LogPriority.Low,
            LogCategory category = LogCategory.Other)
        {
            InternalLog(message, LogType.Exception, priority, category);
        }
        
        /// <summary>
        /// Logs a separator string to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="priority">The priority of the break. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the break. Supports multiple categories at once using "|" bitwise operator.</param>
        public void LogBreak(LogPriority priority = LogPriority.Low, LogCategory category = LogCategory.Other)
        {
            InternalLog(Break, LogType.Log, priority, category);
        }

        /// <summary>
        /// Opens the log text file
        /// </summary>
        public void OpenLogFile()
        {
#if ENABLE_LOG_FILE
            logSave.Open();
#endif
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Logs a message of a given type to the Unity Console and a text file if the priority and category filtering conditions are met.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="logType">The type of the message</param>
        /// <param name="priority">The priority of the message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="category">The category of the message. Supports multiple categories at once using "|" bitwise operator.</param>
        private void InternalLog(object message, LogType logType,
            LogPriority priority = LogPriority.Zero,
            LogCategory category = LogCategory.Other)
        {
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            
            if (IsLogTypeAllowed(logType, priority, category))
            {
                logger.Log(logType, message);
            }
#endif
        }
        
        /// <summary>
        /// Checks if the log message comply with current preferences of the logging system
        /// </summary>
        /// <param name="logType">The type of the received message</param>
        /// <param name="logPriority">The priority of the received message. Supports multiple priorities at once using "|" bitwise operator.</param>
        /// <param name="logCategory">The category of the received message. Supports multiple categories at once using "|" bitwise operator.</param>
        /// <returns>Returns true if the log message comply with current preferences of the logging system</returns>
        private bool IsLogTypeAllowed(LogType logType, LogPriority logPriority, LogCategory logCategory)
        {
            bool typeAllowed = logger.IsLogTypeAllowed(logType);

            int categoryValue = (int) logCategory;
            int priorityValue = (int) logPriority;
            
            if ((priorityMask & priorityValue) == 0 ||
                (categoryMask & categoryValue) == 0)
            {
                return false;
            }

            return typeAllowed;
        }
        
        #endregion
    }
}