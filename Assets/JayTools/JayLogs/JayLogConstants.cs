﻿namespace JayTools.JayLogs
{
    /// <content>
    /// Contains saving keys and static strings used in logging process.
    /// </content>
    public partial class JayLogConstants
    {
        public const string LogCategorySaveKey = "JayLogCategoryMask";
        public const string LogPrioritySaveKey = "JayLogPriorityMask";
        public const string ClearLogOnStartKey = "JayLogClearLogOnStart";

        public const string SaveFileDefineSymbol = "ENABLE_LOG_FILE";

        public static readonly string Break = new string('-', 32);
    }
}