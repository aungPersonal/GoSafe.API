namespace GoSafe.LoggerTool
{
    /// <summary>
    /// Logger options
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Log folder path
        /// </summary>
        public string LogFolderPath { get; set; } = null!;

        /// <summary>
        /// Log file name
        /// </summary>
        public string LogFileName { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public string? LogLevel { get; set; }

        /// <summary>
        /// Maximum file size
        /// </summary>
        public int MaximumFileSize { get; set; }

        /// <summary>
        /// Maximum rolling files
        /// </summary>
        public int MaxSizeRollBackups { get; set; }
    }
}
