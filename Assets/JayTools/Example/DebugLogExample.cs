using JayTools.JayLogs;
using UnityEngine;

namespace JayTools.Example
{
    public class DebugLogExample : MonoBehaviour
    {
        void Start()
        {
            //To setup priorities and categories open: Tools -> Jay Tools -> Jay Log Window
            //By default all is set to nothing, so nothing will show up in the console!
            
            JayLog.Log($"Message with priority High and category Gameplay", LogPriority.High, LogCategory.Gameplay);
            
            JayLog.LogBreak();
            
            JayLog.Log($"Message with priority Low and category {LogCategory.Gameplay} & {LogCategory.Audio}", LogPriority.Low, LogCategory.Gameplay | LogCategory.Audio);
            
            JayLog.LogWarning("Warning message with priority Medium and category Audio", LogPriority.Medium, LogCategory.Audio);
            
            JayLog.LogError("Error message with exception priority and category Other", LogPriority.Exception, LogCategory.Other);
            
            JayLog.OpenLogFile();
        }
    }
}
