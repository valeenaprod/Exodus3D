using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

namespace Exodus3D.Utility;

public static class Logger
{
    public enum LogLevel { Info, Warning, Error, Debug}

    private const string LogFilePath = "user://log.txt";

    public static void Log(string message, LogLevel level = LogLevel.Debug, 
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        if (level == LogLevel.Debug)
        {
            LogDebug(message, memberName, filePath, lineNumber);
            return;
        }
        
        var logMessage = level == LogLevel.Info
            ? $"{DateTime.Now}: [{level}] {message}"
            : $"{DateTime.Now}: [{level}] {message} (Caller: {memberName}, {filePath}, line {lineNumber})";

        switch (level)
        {
            case LogLevel.Error:
                GD.PrintErr(logMessage);
                break;
            case LogLevel.Warning:
                GD.PushWarning(logMessage);
                break;
            case LogLevel.Info:
                GD.Print(logMessage);
                break;
            default:
                GD.Print(logMessage);
                break;
        }

        WriteToFile(logMessage);
    }

    private static void LogDebug(string message, string memberName, string filePath, int lineNumber)
    {
        #if DEBUG
        var logMessage = $"{DateTime.Now}: [DEBUG] {message} " +
                     $"(Caller: {memberName}, {filePath}, line {lineNumber}";
        GD.Print(logMessage);
        WriteToFile(logMessage);
        #endif
        
    }

    private static async void WriteToFile(string message)
    {
        await Task.Run(() =>
        {
            try
            {
                using var logFile = FileAccess.Open(LogFilePath, FileAccess.ModeFlags.WriteRead);
                logFile.Seek(logFile.GetLength()); // Move to end for appending
                logFile.StoreLine(message);
            }
            catch (Exception e)
            {
                GD.PrintErr($"Failed to write to log file: {e.Message}");
            }
        });

    }
}