﻿namespace JayTools.JayLogs
{
    /// <content>
    /// Contains saving keys and static strings used in logging process.
    /// </content>
    public partial class JayLogService
    {
        public const string LogCategorySaveKey = "JayLogCategoryMask";
        public const string LogPrioritySaveKey = "JayLogPriorityMask";
        public const string ClearLogOnStartKey = "JayLogClearLogOnStart";
        
        private static readonly string Break = new string('-', 32);
    }
}