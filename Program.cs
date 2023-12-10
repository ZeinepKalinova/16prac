using System;
using System.IO;

class FileChangeLogger
{
    private static string logFilePath;

    static void Main()
    {
        Console.WriteLine("Приложение для логирования изменений в файлах");

        // Задача 3: Конфигурация Слежения
        Console.Write("Введите путь к отслеживаемой директории: ");
        string directoryPath = Console.ReadLine();

        Console.Write("Введите путь к лог-файлу: ");
        logFilePath = Console.ReadLine();

        // Задача 5: Защита от Ошибок
        try
        {
            // Задача 1: Отслеживание Изменений
            using (FileSystemWatcher watcher = new FileSystemWatcher(directoryPath))
            {
                // Задача 4: Интерактивный Консольный Интерфейс
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;

                Console.WriteLine($"Отслеживание изменений в директории: {directoryPath}");
                Console.WriteLine("Нажмите 'q' для выхода.");

                while (Console.ReadKey().KeyChar != 'q') ;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void OnChanged(object sender, FileSystemEventArgs e)
    {
        LogChange($"Изменение: {e.ChangeType} - {e.FullPath}");
    }

    private static void OnRenamed(object sender, RenamedEventArgs e)
    {
        LogChange($"Переименование: {e.OldFullPath} -> {e.FullPath}");
    }

    private static void LogChange(string message)
    {
        // Задача 2: Логирование Изменений
        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
        }
    }
}
