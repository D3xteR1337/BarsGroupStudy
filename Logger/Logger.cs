using System;
using System.Collections.Generic;
using System.IO;

namespace Logger
{
    /// <summary>
    /// Класс логирования, реализация ILog
    /// </summary>
    class Logger : ILog
    {
        /// <summary>
        /// Константы названий файлов
        /// </summary>
        private const string ErrorFile = "Error.log";
        private const string WarningFile = "Warning.log";
        private const string FatalFile = "Fatal.log";
        private const string DebugFile = "Debug.log";
        private const string InfoFile = "Info.log";
        private const string SysInfoFile = "SystemInfo.log";
        private const string Path = @"C:/Users/Роберт/source/repos/Logger/logs/";


        /// <summary>
        /// Текущая дата (при вызове лога проводится проверка на совпадение этой переменной с реальным текущим временем)
        /// </summary>
        private DateTime currDate = new DateTime();

        /// <summary>
        /// Список уникальных ошибок
        /// </summary>
        private List<string> uniqueError { get; set; } = new List<string>();

        /// <summary>
        /// Список уникальных предупреждений
        /// </summary>
        private List<string> uniqueWarning { get; set; } = new List<string>();



        public void Debug(string message)
        {
            IntoFile(DebugFile, ToText("DEBUG", message));
        }

        public void Debug(string message, Exception e)
        {
            IntoFile(DebugFile, ToText("DEBUG", message, e));
        }

        public void DebugFormat(string message, params object[] args)
        {
            IntoFile(DebugFile, ToText("DEBUG", message, args));
        }

        public void Error(string message)
        {
            IntoFile(ErrorFile, ToText("ERROR", message));

        }

        public void Error(string message, Exception e)
        {
            uniqueError.Add($"{e.Message} - {e.StackTrace}");
            IntoFile(ErrorFile, ToText("ERROR", message, e));
        }

        public void Error(Exception ex)
        {
            uniqueError.Add($"{ex.Message} - {ex.StackTrace}");
            IntoFile(ErrorFile, ToText("DEBUG", ex));
        }

        public void ErrorUnique(string message, Exception e)
        {
            if(!uniqueError.Contains($"{e.Message} - {e.StackTrace}"))
            {
                uniqueError.Add($"{e.Message} - {e.StackTrace}");
                IntoFile(ErrorFile, ToText("UNIQERROR", message, e));
            }
        }

        public void Fatal(string message)
        {
            IntoFile(FatalFile, ToText("FATAL", message));
        }

        public void Fatal(string message, Exception e)
        {
            IntoFile(FatalFile, ToText("FATAL", message, e));
        }

        public void Info(string message)
        {
            IntoFile(InfoFile, ToText("INFO", message));
        }

        public void Info(string message, Exception e)
        {
            IntoFile(InfoFile, ToText("INFO", message, e));
        }

        public void Info(string message, params object[] args)
        {
            IntoFile(InfoFile, ToText("INFO", message, args));
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            IntoFile(SysInfoFile, ToText("SYSINFO", message, null, properties));
        }

        public void Warning(string message)
        {
            uniqueWarning.Add($"{message}");
            IntoFile(WarningFile, ToText("WARNING", message));
        }

        public void Warning(string message, Exception e)
        {
            uniqueWarning.Add($"{message}");
            IntoFile(WarningFile, ToText("Warning", message, e));
        }

        public void WarningUnique(string message)
        {
            if(!uniqueWarning.Contains($"{message}"))
            {
                uniqueWarning.Add($"{message}");
                IntoFile(WarningFile, ToText("WARNING", message));
            }
        }


        /// <summary>
        /// Метод записи сообщений в файлы
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="message"></param>
        private void IntoFile(string filename, string message)
        {
            if(currDate != DateTime.Now)
            {
                currDate = DateTime.Now;
                uniqueError.Clear();
                uniqueWarning.Clear();
            }
            string dirpath = Path + currDate.ToString("yyyy-MM-dd");
            DirectoryInfo directoryInfo = new DirectoryInfo(dirpath);
            Console.WriteLine(directoryInfo.ToString());
            FileInfo file = new FileInfo(filename);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                File.AppendAllText(@String.Format("{0}/{1}", dirpath, filename), $"{currDate.ToString("dd.MM.yyyy")} {currDate.ToString("HH:mm:ss")} {message}\n");
            }
            if (!file.Exists)
            {
                file.Create();
            }
        }


        /// <summary>
        /// Метод создания полноценных сообщений для записи в логи
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private string ToText(string name, string message, Exception e = null, Dictionary<object,object> properties = null) 
        {
            if (e != null)
            {
                string exceptionInfo = $"Исключение: {e.Message};    Метод: {e.TargetSite};    Трассировка стека: {e.StackTrace}";
                return $"({name}):  {message}:  {exceptionInfo}";
            }
            else if (properties != null)
            {
                string FullText = "";
                foreach(KeyValuePair<object,object> prop in properties)
                {
                    FullText += $"\n{prop.Key} - {prop.Value}";
                }
                return $"({name}):  {message}:  {FullText}";
            }
            else
            {
                return $"({name}):  {message}";
            }
        }

        /// <summary>
        /// Метод создания полноценных сообщений для записи в логи
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private string ToText(string name, string message, params object[] args)
        {
            string FullText = "";
            for(int i = 0; i < args.Length; i++)
            {
                FullText += $"\n {args[i]}";
            }
            return $"({name}):  {message}:  {FullText}";
        }

        /// <summary>
        /// Метод создания полноценных сообщений для записи в логи
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private string ToText(string name, Exception e)
        {
            return $"({name}):   Исключение: {e.Message};    Метод: {e.TargetSite};    Трассировка стека: {e.StackTrace}";
        }
    }
}
