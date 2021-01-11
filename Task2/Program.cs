using System;
using System.IO;

namespace Task2
{
    class Program
    {
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
        static void Main(string[] args)
        {
            string patch = "";

            Boolean exit = false;
            do
            {
                // проверка на пустой ввод
                do
                {
                    Console.WriteLine("Введите путь к каталогу:");
                    patch = Console.ReadLine();
                    if (patch == "") Console.WriteLine("Вы ничего не ввели!!!");

                } while (patch.Length < 1);


                //вывод содержимого папки совмещенный с проверкой существования папки 
                if (ShowDirInfo(patch))
                {
                    Console.WriteLine("_________________________\n");
                    var totalSize = GetTotalSize(patch);
                    Console.WriteLine($"Общий размер файлов: \n{totalSize} Байт" );
                    Console.WriteLine($"{(totalSize/Math.Pow(1024,1)):f2} КБайт");
                    Console.WriteLine($"{(totalSize / Math.Pow(1024,2)):f2} МБайт");
                }
                Console.WriteLine("Для выхода нажмите ESC, для продолжения - любую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Escape) exit = true;
                Console.WriteLine("_________________________");
            } while (!exit);


            Console.ReadKey();

        }
    }
}
