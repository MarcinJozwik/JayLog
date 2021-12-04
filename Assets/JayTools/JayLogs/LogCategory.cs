﻿using System;

 namespace JayTools.JayLogs
{
    /// <summary>
    /// A list of example log categories used for filtering log messages across the project.
    /// You can change the list however you want to suit your needs
    /// but remember about a correct order of the values.
    /// </summary>
    
    [Flags]
    public enum LogCategory
    {
        Other = 1,
        Gameplay = 1 << 1,
        AI = 1 << 2,
        Animations = 1 << 3,
        Audio = 1 << 4,
        Graphics = 1 << 5,
        UI = 1 << 6,
    }
}