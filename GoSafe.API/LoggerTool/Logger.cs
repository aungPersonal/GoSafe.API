using System.Reflection;
using System.Runtime.CompilerServices;

namespace GoSafe.LoggerTool
{
    /// <summary>
    ///  Defines logging severity levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Log all
        /// </summary>
        All = 0,
        /// <summary>
        ///     Logs that contain the most detailed messages. These messages may contain sensitive
        ///     application data. These messages are disabled by default and should never be
        ///     enabled in a production environment.
        /// </summary>
        Trace,

        /// <summary>
        ///     Logs that are used for interactive investigation during development. These logs
        ///     should primarily contain information useful for debugging and have no long-term
        ///     value.
        /// </summary>
        Debug,

        /// <summary>
        ///     Logs that track the general flow of the application. These logs should have long-term
        ///     value.
        /// </summary>
        Info,

        /// <summary>
        ///     Logs that highlight an abnormal or unexpected event in the application flow,
        ///     but do not otherwise cause the application execution to stop.
        /// </summary>
        Warn,

        /// <summary>
        ///     Logs that highlight when the current flow of execution is stopped due to a failure.
        ///     These should indicate a failure in the current activity, not an application-wide
        ///     failure.
        /// </summary>
        Error,

        /// <summary>
        ///     Logs that describe an unrecoverable application or system crash, or a catastrophic
        ///     failure that requires immediate attention.
        /// </summary>
        Critical,

        /// <summary>
        ///     Not used for writing log messages. Specifies that a logging category should not
        ///     write any messages.
        /// </summary>
        None
    }

    /// <summary>
    /// Logger
    /// </summary>
    public class Logger
    {
        private readonly LoggerOptions loggerOptions;

        private const string _LOG_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss fff";
        private const int LOG_FILE_SIZE = 314572800; // 300 MB

        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel Level { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerOptions">logger options</param>
        public Logger(LoggerOptions loggerOptions)
        {
            this.loggerOptions = loggerOptions;

            if (loggerOptions != null && !string.IsNullOrEmpty(loggerOptions.LogLevel))
            {
                LogLevel logLevel;
                bool isSuccess = Enum.TryParse(loggerOptions.LogLevel, out logLevel);

                if (isSuccess)
                {
                    this.Level = logLevel;
                }

                if (loggerOptions.MaximumFileSize <= 0)
                {
                    loggerOptions.MaximumFileSize = LOG_FILE_SIZE;
                }
            }
            else
            {
                this.Level = LogLevel.None;
            }
        }

        /// <summary>
        /// Check IsEnabled
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            bool isEnable = false;
            isEnable = logLevel >= Level;

            return isEnable;
        }

        /// <summary>
        /// Wirte log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="filePath">File path</param>
        /// <param name="methodName">Method name</param>
        /// <param name="lineNo">Line no</param>
        public void Error(
              Exception? exception
            , string message = ""
            , [CallerFilePath] string filePath = ""
            , [CallerMemberName] string methodName = ""
            , [CallerLineNumber] int lineNo = 0)
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (string.IsNullOrEmpty(logFilePath))
                {
                    return;
                }

                if (IsEnabled(LogLevel.Error) == false)
                {
                    return;
                }

                string caller = filePath.Split('\\').Last().Split('.').First();
                caller = string.Format("{0}.{1}", caller, methodName);

                var logRecord = string.Format("{0} | {1} | {2} | {3} | {4} | {5}",
                                                DateTime.Now.ToString(_LOG_DATE_FORMAT),
                                                lineNo,
                                                LogLevel.Error.ToString(),
                                                caller,
                                                message,
                                                exception != null ? exception.ToString() : "");

                WriteLog(logFilePath, logRecord);
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// Wirte log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="filePath">File path</param>
        /// <param name="methodName">Method name</param>
        /// <param name="lineNo">Line no</param>
        public void Error(
              string errMessage,
              Exception? exception
            , string message = ""
            , [CallerFilePath] string filePath = ""
            , [CallerMemberName] string methodName = ""
            , [CallerLineNumber] int lineNo = 0)
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (string.IsNullOrEmpty(logFilePath))
                {
                    return;
                }

                if (IsEnabled(LogLevel.Error) == false)
                {
                    return;
                }

                string caller = filePath.Split('\\').Last().Split('.').First();
                caller = string.Format("{0}.{1}", caller, methodName);

                var logRecord = string.Format("{0} | {1} | {2} | {3} | {4} | {5}",
                                                DateTime.Now.ToString(_LOG_DATE_FORMAT),
                                                lineNo,
                                                LogLevel.Error.ToString(),
                                                caller,
                                                message,
                                                exception != null ? exception.ToString() : "");

                WriteLog(logFilePath, logRecord);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Wirte log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="filePath">File path</param>
        /// <param name="methodName"></param>
        /// <param name="lineNo"></param>
        public void Info(string message
            , [CallerFilePath] string filePath = ""
            , [CallerMemberName] string methodName = ""
            , [CallerLineNumber] int lineNo = 0)
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (string.IsNullOrEmpty(logFilePath))
                {
                    return;
                }

                if (IsEnabled(LogLevel.Info) == false)
                {
                    return;
                }

                string caller = filePath.Split('\\').Last().Split('.').First();
                caller = string.Format("{0}.{1}", caller, methodName);

                var logRecord = string.Format("{0} | {1} | {2} | {3} | {4}",
                                                DateTime.Now.ToString(_LOG_DATE_FORMAT),
                                                lineNo,
                                                LogLevel.Info.ToString(),
                                                caller,
                                                message);

                WriteLog(logFilePath, logRecord);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Wirte log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="filePath">File path</param>
        /// <param name="methodName">Method name</param>
        /// <param name="lineNo">Line no</param>
        public void Debug(string message
            , [CallerFilePath] string filePath = ""
            , [CallerMemberName] string methodName = ""
            , [CallerLineNumber] int lineNo = 0)
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (string.IsNullOrEmpty(logFilePath))
                {
                    return;
                }

                if (IsEnabled(LogLevel.Info) == false)
                {
                    return;
                }

                string caller = filePath.Split('\\').Last().Split('.').First();
                caller = string.Format("{0}.{1}", caller, methodName);

                var logRecord = string.Format("{0} | {1} | {2} | {3} | {4}",
                                                DateTime.Now.ToString(_LOG_DATE_FORMAT),
                                                lineNo,
                                                LogLevel.Info.ToString(),
                                                caller,
                                                message);

                WriteLog(logFilePath, logRecord);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Wirte log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="filePath">File path</param>
        /// <param name="methodName">Method name</param>
        /// <param name="lineNo">Line no</param>
        public void Warn(string message
            , [CallerFilePath] string filePath = ""
            , [CallerMemberName] string methodName = ""
            , [CallerLineNumber] int lineNo = 0)
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (string.IsNullOrEmpty(logFilePath))
                {
                    return;
                }

                if (IsEnabled(LogLevel.Warn) == false)
                {
                    return;
                }

                string caller = filePath.Split('\\').Last().Split('.').First();
                caller = string.Format("{0}.{1}", caller, methodName);

                var logRecord = string.Format("{0} | {1} | {2} | {3} | {4}",
                                                DateTime.Now.ToString(_LOG_DATE_FORMAT),
                                                lineNo,
                                                LogLevel.Warn.ToString(),
                                                caller,
                                                message);

                WriteLog(logFilePath, logRecord);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Get log folder path
        /// </summary>
        /// <returns></returns>
        private string GetLogFilePath()
        {
            try
            {
                if (string.IsNullOrEmpty(loggerOptions.LogFolderPath))
                {
                    return string.Empty;
                }

                string logFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", loggerOptions.LogFolderPath);
                string logFilePath = Path.Combine(logFolderPath, loggerOptions.LogFileName);

                return logFilePath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="messge">log message</param>
        private void WriteLog(string fileName, string messge)
        {
            try
            {
                lock (this)
                {
                    string? dir = Path.GetDirectoryName(fileName);

                    if (string.IsNullOrEmpty(dir))
                    {
                        return;
                    }

                    if (Directory.Exists(dir) == false)
                    {
                        Directory.CreateDirectory(dir);
                    }

                    // File size and rolling backup
                    if (File.Exists(fileName))
                    {
                        FileInfo fileInfo = new(fileName);

                        if (fileInfo.Length >= loggerOptions.MaximumFileSize)
                        {
                            RollFile(dir, fileName);
                        }
                    }

                    using (var streamWriter = new StreamWriter(fileName, true))
                    {
                        streamWriter.WriteLine(messge);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RollFile(string dir, string fileName)
        {
            try
            {
                string logFileName = Path.GetFileNameWithoutExtension(loggerOptions.LogFileName);
                string logFileExt = Path.GetExtension(loggerOptions.LogFileName);
                string regFormat = $"{logFileName}.*{logFileExt}";

                List<FileInfo> fileLst = Directory.GetFiles(dir, regFormat)
                                            .Select(x => new FileInfo(x))
                                            .OrderBy(f => f.CreationTime)
                                            .ToList();



                if (loggerOptions.MaxSizeRollBackups > 0)
                {
                    int currIndex = loggerOptions.MaxSizeRollBackups - 1;

                    if (fileLst == null || fileLst.Count == 0)
                    {
                        // rename fileName to fileName.1
                        string newFName = string.Format("{0}.{1}{2}", logFileName, 1, logFileExt);
                        File.Move(fileName, Path.Combine(dir, newFName));
                        return;
                    }

                    //fileLst.Reverse();

                    if (fileLst.Count > currIndex)
                    {
                        int delCnt = (fileLst.Count - currIndex);
                        for (int i = 0; i < delCnt; i++)
                        {
                            FileInfo fi = fileLst[i];

                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }

                        fileLst.RemoveRange(0, delCnt);
                    }

                    int rnFIndex = fileLst.Count + 1;

                    for (int i = 0; i < fileLst.Count; i++)
                    {
                        FileInfo fi = fileLst[i];
                        string newFName = string.Format("{0}.{1}{2}", logFileName, rnFIndex, logFileExt);
                        fi.MoveTo(Path.Combine(dir, newFName));
                        rnFIndex--;
                    }

                    // rename fileName to fileName.1
                    string newFileName = string.Format("{0}.{1}{2}", logFileName, 1, logFileExt);
                    File.Move(fileName, Path.Combine(dir, newFileName));
                }
                else
                {
                    // Delete file
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}