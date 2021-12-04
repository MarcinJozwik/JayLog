using JayLog;
using UnityEngine;

public class DebugLogExample : MonoBehaviour
{
    void Start()
    {
        Debug.LogFormat(gameObject, "My message");
        InvokeRepeating(nameof(Print),1f,2f);
        JayLog.JayLog.Log($"Message with priority High and category Gameplay", LogPriority.High, LogCategory.Gameplay);
        JayLog.JayLog.Log($"Message with priority Low and category {LogCategory.Gameplay | LogCategory.Audio}", LogPriority.Low, LogCategory.Gameplay | LogCategory.Audio);
        
        int categoryMask = PlayerPrefs.GetInt(JayLog.JayLog.LogCategorySaveKey, 0);
        int priorityMask = PlayerPrefs.GetInt(JayLog.JayLog.LogPrioritySaveKey, 0);

        Debug.Log($"Category: {(LogCategory)categoryMask}");
        Debug.Log($"Priority: {(LogPriority)priorityMask}");
        
        //JayLog.LogWarning("Message with priority and category", LogPriority.High, LogCategory.Gameplay);
        //JayLog.LogError("Message with priority and category", LogPriority.High, LogCategory.Gameplay);
        //JayLog.OpenLogFileDirectory();
    }

    private void Print()
    {
        Debug.Log("Message");
    }
}
