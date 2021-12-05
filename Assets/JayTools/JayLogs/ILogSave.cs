﻿namespace JayTools.JayLogs
{
    /// <summary>
    /// An interface used for saving received logs. 
    /// </summary>
    public interface ILogSave
    {
        void Init();

        void Save();

        void Open(bool forceSave);
    }
}