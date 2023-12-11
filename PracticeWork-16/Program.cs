using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PracticeWork_16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к отслеживаемой директории: ");
            string path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    watcher.Path = path;

                    /// <summary>
                    ///     1. Отслеживание Изменений:
                    ///     -Приложение должно наблюдать за изменениями в определённой директории
                    ///     (создание, удаление, переименование файлов и поддиректорий).
                    /// </summary>

                                    watcher.IncludeSubdirectories = true;
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;


                    watcher.Created += OnChanged;
                    watcher.Deleted += OnChanged;
                    watcher.Renamed += OnRenamed;


                    watcher.EnableRaisingEvents = true;

                    Console.WriteLine($"Отслеживание изменений в директории {path}... (нажмите 'Enter' для завершения)");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Указанная директория не существует.");
            }
        }

        /// <summary>
        ///     2. Логирование Изменений:
        ///     - Все изменения должны записываться в лог-файл с указанием времени и типа изменения.
        ///     (Не уверен, что правильно)
        /// </summary>
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Log($"Изменение: {e.ChangeType} - {e.FullPath}");
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Log($"Переименование: {e.OldFullPath} -> {e.FullPath}");
        }

        private static void Log(string message)
        {
            string logFilePath = "log.txt";
            string logMessage = $"{DateTime.Now}: {message}";

            // Запись
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

            // Вывод
            Console.WriteLine(logMessage);
        }
    }
}
