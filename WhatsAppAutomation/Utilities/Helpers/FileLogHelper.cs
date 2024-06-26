using Newtonsoft.Json;
//using WhatsAppAutomation.Models.Logs;

namespace WhatsAppAutomation.Utilities.Helpers;

public class FileLogHelper
{
    private const string LogDirectory = "./Logs";

    //public static async Task WriteLog(CustomLog log)
    //{
    //    var path = Path.Combine(LogDirectory, "Customs", $"{DateTime.Now:yyyyMMdd}.log");
    //    await WriteLogToFile(path, log);
    //}

    //public static async Task WriteLog(ErrorLog log)
    //{
    //    var path = Path.Combine(LogDirectory, "Errors", $"{DateTime.Now:yyyyMMdd}.log");
    //    await WriteLogToFile(path, log);
    //}

    private static async Task WriteLogToFile(string path, object log)
    {
        if (!Directory.Exists(LogDirectory))
            Directory.CreateDirectory(LogDirectory);

        string[] logTypes =
        {
            "Customs",
            "Errors"
        };

        foreach (var logType in logTypes)
        {
            var logPath = Path.Combine(LogDirectory, logType);
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
        }

        var serializedLog = JsonConvert.SerializeObject(log, Formatting.Indented);
        await File.AppendAllTextAsync(path, $"{serializedLog}{Environment.NewLine}");
    }
}