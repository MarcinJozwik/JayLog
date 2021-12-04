using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JayLog
{
    /// <summary>
    /// Implementation of ILogSave interface used for storing received logs from logging system.
    /// The class stores all the logs using string builder and if the application is quiting
    /// or the exception has occured, the content of the string builder is moved to the .txt file. 
    /// </summary>
    public class StringBuilderToTxtLogSave : ILogSave
    {
        private static bool clearLogFileOnStart = true;
        private static string logFilePath;
        private static StringBuilder logBuilder;

        /// <summary>
        /// Initializes the saving system.
        /// </summary>
        public void Init()
        {
            clearLogFileOnStart = PlayerPrefs.GetInt(JayLog.ClearLogOnStartKey, 1) == 1;
            
            logFilePath = Application.persistentDataPath + "/jayLog.txt";
            
            Application.logMessageReceived += LogToBuilder;
            Application.quitting += Save;
            
            InitBuilder();

            if (clearLogFileOnStart)
            {
                ClearLogFile();
            }
        }

        /// <summary>
        /// Moves the content of the string builder to the .txt file. Clears string builder.
        /// </summary>
        public void Save()
        {
            using(StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
            {
                writer.Write(logBuilder.ToString());
            }

            logBuilder.Clear();
        }

        /// <summary>
        /// Opens the log text file.
        /// </summary>
        public void Open()
        {
            Application.OpenURL(logFilePath);
        }

        /// <summary>
        /// Makes an instance of string builder and adds the current filtering information at the start.
        /// </summary>
        private void InitBuilder()
        {
            logBuilder = new StringBuilder();
            
            logBuilder.AppendLine("Powered by TweenyLite");
            logBuilder.AppendLine();
            LogMaskToBuilder("Category", PlayerPrefs.GetInt(JayLog.LogCategorySaveKey, 0), Enum.GetNames(typeof(LogCategory)).ToArray());
            logBuilder.AppendLine();
            LogMaskToBuilder("Priority", PlayerPrefs.GetInt(JayLog.LogPrioritySaveKey, 0), Enum.GetNames(typeof(LogPriority)).ToArray());
            logBuilder.AppendLine();
            logBuilder.AppendLine();
        }
        
        /// <summary>
        /// Adds a received log message to the string builder with current time. 
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="stackTrace"></param>
        /// <param name="logType"></param>
        private void LogToBuilder(string logMessage, string stackTrace, LogType logType)
        {
            logBuilder.AppendLine($"[{DateTime.Now:dd/MM/yyyy hh:mm:ss.ffffff tt}]");
            logBuilder.AppendLine(logMessage);
            logBuilder.Append(logType);
            logBuilder.Append(": ");
            logBuilder.AppendLine(stackTrace);

            if (logType == LogType.Exception)
            {
                Save();
            }
        }

        /// <summary>
        /// Clears the log text file.
        /// </summary>
        private void ClearLogFile()
        {
            File.WriteAllText(logFilePath, String.Empty);
        }
        
        /// <summary>
        /// Appends a string representation of filter mask to the log builder.
        /// </summary>
        private static void LogMaskToBuilder(string header, int mask, string[] options)
        {
            logBuilder.Append(header);
            logBuilder.Append(": ");
            
            if (mask == 0)
            {
                logBuilder.Append("nothing");
                return;
            }

            if (mask == -1)
            {
                logBuilder.Append("everything");
                return;
            }

            for (int i = 0; i < options.Length; i++)
            {
                int value = 1 << i;
                if ((value & mask) == value)
                {
                    logBuilder.Append(options[i] + " | ");
                }
            }

            logBuilder.Remove(logBuilder.Length - 3, 3);
        }
    }
}