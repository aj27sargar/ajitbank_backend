using System;
using System.IO;

namespace Ajit_Bank.Helpers
{
    public class ErrorLogger
    {
        private readonly string _logFilePath;

        public ErrorLogger()
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            // the log file name based on the current UTC date (one file per day)
            _logFilePath = Path.Combine(logDir, $"log_{DateTime.UtcNow:yyyy-MM-dd}.csv");

            // Add header if file is newly created
            if (!File.Exists(_logFilePath))
            {
                File.AppendAllText(_logFilePath, "Date,Time,Message,Type\n");
            }
        }

        public void LogInfo(string message)    // Method to log general information messages
        {
            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string time = DateTime.UtcNow.ToString("HH:mm:ss");
            string line = $"{date},{time},{message},INFO\n";
            File.AppendAllText(_logFilePath, line);
        }

        public void LogError(string message, Exception ex)    // Method to log error messages with exception details
        {
            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string time = DateTime.UtcNow.ToString("HH:mm:ss");
            string fullMessage = $"{message} | Exception: {ex.Message}";
            string line = $"{date},{time},{fullMessage},ERROR\n";
            File.AppendAllText(_logFilePath, line);
        }
    }
}
