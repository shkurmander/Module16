using System;
using System.Collections.Generic;
using System.IO;

namespace Task3
{
    class Program
    {
        /// <summary>
        /// Метод удаляющий все содержимое папки
        /// в принципе лишнюю проверку на существование папки можно убрать, если мы 
        /// сначала вызываем метот ShowDirInfo
        /// </summary>
        /// <param name="patch"></param>
        /// <returns>Пока резервный возврат true если успешно или с ошибками</returns>
        public static bool DeleteAll(string patch)
        {
            DirectoryInfo dir = new DirectoryInfo(patch);
            long cnt = 0;

            if (dir.Exists)
            {
                var dirlist = dir.GetDirectories();
                var filelist = dir.GetFiles();
                if (dirlist.Length == 0 && filelist.Length == 0)
                {
                    Console.WriteLine("Ошибка: Каталог пуст!!!");
                    return false;
                }
                bool flag = false;                           //флаг ошибок
                List<string> ErrorBuf = new List<string>();  //Список ошибок удаления
                foreach (var item in dirlist)
                {
                    try
                    {
                        item.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Каталог '{item.Name}' не может быть удален:");
                        ErrorBuf.Add(ex.Message);
                        flag = true;

                    }
                }
                foreach (var item in filelist)
                {
                    try
                    {
                        item.Delete();
                        ++cnt;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Файл '{item.Name}' не может быть удален:");
                        ErrorBuf.Add(ex.Message);
                        flag = true;
                    }
                }
                if (flag)
                {
                    Console.WriteLine("При удалении возникли ошибки:");
                    foreach (var item in ErrorBuf)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine($"Удалено файлов: {cnt}");
                    return false;
                }
                else
                {
                    Console.WriteLine("Каталог успешно очищен");
                    Console.WriteLine($"Удалено файлов: {cnt}") ;                    
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Указанный каталог не существует:");
                return false;
            }

        }
        /// <summary>
        /// Метод печатающий содержимое папки
        /// </summary>
        /// <param name="patch"></param>
        /// <returns>Возвращает true если каталог существует, false  если нет</returns>
        public static bool ShowDirInfo(string patch)
        {
            DirectoryInfo dir = new DirectoryInfo(patch);

            if (dir.Exists)
            {
                var dirlist = dir.GetDirectories();
                var filelist = dir.GetFiles();
                Console.WriteLine("Содержимое каталога:\n");
                foreach (var item in dirlist)
                {
                    Console.WriteLine($"D   {item.Name}");
                }
                foreach (var item in filelist)
                {
                    Console.WriteLine($"F   {item.Name}");
                }                
                return true;
            }
            else
            {
                Console.WriteLine("Указанный каталог не существует:");
                return false;
            }


        }
        /// <summary>
        /// Метод возвращающий размер файла
        /// </summary>
        /// <param name="patch">путь к файлу</param>
        /// <returns>Возвращает размер файла в байтах</returns>
        public static long GetFileSize(string patch)
        {
            return new FileInfo(patch).Length;
        }
        /// <summary>
        /// Рекурсивный метод подсчета размера файлов в папке, включая подпапки
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        public static long GetTotalSize(string patch)
        {
            long totalSize = 0;
            //Сначала подсчитываем размер файлов в корневой папке
            try
            {
                var files = new DirectoryInfo(patch).GetFiles();
                foreach (var file in files)
                {
                    totalSize += GetFileSize(file.FullName);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            //Дальше идем по подпапкам
            try
            {
                var dirs = new DirectoryInfo(patch).GetDirectories();
                foreach (var dir in dirs)
                {
                    totalSize += GetTotalSize(dir.FullName);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return totalSize;
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //string patch = "c:\test";
            string patch = "";
            long totalSize = 0;            

            Boolean exit = false;
            do
            {
                // проверка на пустой ввод
                do
                {
                    Console.WriteLine("Введите путь к каталогу который необходимо очистить:");
                    patch = Console.ReadLine();
                    if (patch == "") Console.WriteLine("Вы ничего не ввели!!!");

                } while (patch.Length < 1);


                //вывод содержимого папки совмещенный с проверкой существования папки 
                if (ShowDirInfo(patch))
                {
                    totalSize = GetTotalSize(patch);
                    Console.WriteLine($"Общий размер файлов: \n{totalSize} Байт");
                    Console.WriteLine($"{(totalSize / Math.Pow(1024, 1)):f2} КБайт");
                    Console.WriteLine($"{(totalSize / Math.Pow(1024, 2)):f2} МБайт");
                    Console.WriteLine("_________________________\n");

                    Console.Write("Для удаления всех файлов и папок наберите y:");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        Console.WriteLine();
                        DeleteAll(patch);
                        Console.WriteLine("_________________________\n");
                        ShowDirInfo(patch);

                        var totalSize2 = GetTotalSize(patch);
                        Console.WriteLine($"Общий размер файлов: \n{totalSize2} Байт");
                        Console.WriteLine($"{(totalSize2 / Math.Pow(1024, 1)):f2} КБайт");
                        Console.WriteLine($"{(totalSize2 / Math.Pow(1024, 2)):f2} МБайт");
                        var empty = totalSize - totalSize2;
                        Console.WriteLine($"Освобождено:\n {empty} Байт");
                        Console.WriteLine($"{(empty / Math.Pow(1024, 1)):f2} КБайт");
                        Console.WriteLine($"{(empty / Math.Pow(1024, 2)):f2} МБайт");

                    }
                    Console.WriteLine();

                }
                Console.WriteLine("Для выхода нажмите ESC, для продолжения - любую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Escape) exit = true;
                Console.WriteLine("_________________________\n");
            } while (!exit);


            Console.ReadKey();


        }
    }
}
