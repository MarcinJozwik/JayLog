using JayTools.JayLogs;
using UnityEngine;

namespace JayTools.Examples.JayLogs
{
    public class DebugLogExample : MonoBehaviour
    {
        void Start()
        {
            //To setup priorities and categories open: Tools -> Jay Tools -> Jay Log Window
            //By default all is set to "nothing", so nothing will show up in the console!
            
            JayLog.Log($"Message with priority High and category Gameplay", LogPriority.High, LogCategory.Gameplay);
            
            JayLog.LogBreak();
            
            JayLog.Log($"Message with priority Low and category {LogCategory.Gameplay} & {LogCategory.Audio}", LogPriority.Low, LogCategory.Gameplay | LogCategory.Audio);
            
            JayLog.LogWarning("Warning message with priority Medium and category Audio", LogPriority.Medium, LogCategory.Audio);
            
            JayLog.LogError("Error message with exception priority and category Other", LogPriority.Exception, LogCategory.Other);
            
            //The saving happens on application exit or when exception occurs,
            //but you can manually force saving current logs on opening the log file.
            //Log file is store in appropriate AppData folder.
            JayLog.OpenLogFile(true);
        }
    }
}
