namespace JayLog
{
    /// <content>
    /// Contains saving keys and static strings used in logging process.
    /// </content>
    public static partial class JayLog
    {
        public const string LogCategorySaveKey = "JayLogCategoryMask";
        public const string LogPrioritySaveKey = "JayLogPriorityMask";
        public const string ClearLogOnStartKey = "JayLogClearLogOnStart";
        
        private static readonly string Break = new string('-', 32);
    }
}