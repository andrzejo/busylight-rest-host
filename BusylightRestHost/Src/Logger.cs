using System;
using System.IO;
using System.Text;

namespace BusylightRestHost
{
    public class Logger
    {
        private const string LogFilename = "busylight-rest-host.log";

        private static readonly FileStream LogFileStream = OpenLogFile();

        private static readonly Logger logger = new Logger();

        public static Logger GetLogger()
        {
            return logger;
        }

        public void Info(string message, params object[] args)
        {
            Write("INFO: " + message, args);
        }

        public void Debug(string message, params object[] args)
        {
            Write("DEBUG: " + message, args);
        }

        public void Warn(string message, params object[] args)
        {
            Write("WARN: " + message, args);
        }

        public void Error(string message, params object[] args)
        {
            Write("ERROR: " + message, args);
        }
        
        public void Error(Exception e, string message = "Exception")
        {
            Error($"{message}: {e.GetType()} - {e.Message}. Trace: {e.StackTrace}");
        }

        private void Write(string message, params object[] args)
        {
            var entry = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss - ") + string.Format(message, args);

            var bytes = new UTF8Encoding(true).GetBytes(entry + "\n");
            LogFileStream.Seek(0, SeekOrigin.End);
            LogFileStream.Write(bytes, 0, bytes.Length);
            LogFileStream.Flush();

            Console.WriteLine(entry);
        }

        private static FileStream OpenLogFile()
        {
            var logPath = GetLogPath();
            try
            {
                if (File.Exists(logPath) && new FileInfo(logPath).Length > 1024 * 1024)
                {
                    var archiveLogFile = logPath + ".1";
                    if (File.Exists(archiveLogFile))
                    {
                        File.Delete(archiveLogFile);
                    }

                    File.Move(logPath, archiveLogFile);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Unable to rotate log file: " + e);
            }

            return File.Open(logPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
        }

        private static string GetLogPath()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
                .FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).ToString();
            }

            return Path.Combine(path, LogFilename);
        }
    }
}