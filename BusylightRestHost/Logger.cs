using System;
using System.IO;
using System.Text;

namespace bl_host
{
    public class Logger
    {
        private const string LogFilename = "thulium-agent-launcher.log";

        private static readonly FileStream LogFileStream = OpenLogFile();

        private static readonly Logger logger = new Logger();

        readonly string name;

        private Logger()
        {
            this.name = "";
        }

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

        private void Write(string message, params object[] args)
        {
            string entry = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss -") + " [" + name + "] " +
                           String.Format(message, args);

            byte[] bytes = new UTF8Encoding(true).GetBytes(entry + "\n");
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