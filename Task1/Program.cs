using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
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
                        Console.WriteLine($"Каталог не может быть удален: {ex}");
                        ErrorBuf.Add(ex.Message);

                    }
                }
                foreach (var item in filelist)
                {
                    try
                    {
                        item.Delete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Файл не может быть удален: {ex}");
                        ErrorBuf.Add(ex.Message);

                    }
                }
                if (flag)
                {
                    Console.WriteLine("При удалении возникли ошибки:");
                    foreach (var item in ErrorBuf)
                    {
                        Console.WriteLine(item);
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Каталог успешно очищен");
                    ShowDirInfo(patch);
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
        static void Main(string[] args)
        {
            //string patch = "c:\test";
            string patch = "";
            
            Boolean exit = false;
            do
            {
                // проверка на пустой ввод
                do
                {
                    Console.WriteLine("Введите путь к каталогу который необходимо очистить:");
                    patch = Console.ReadLine();
                    if(patch == "") Console.WriteLine("Вы ничего не ввели!!!");

                } while (patch.Length<1);


                //вывод содержимого папки совмещенный с проверкой существования папки 
                if (ShowDirInfo(patch))
                {
                    Console.Write("Для удаления всех файлов и папок наберите y:");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        Console.WriteLine();
                        DeleteAll(patch);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Для выхода нажмите ESC, для продолжения - любую клавишу");
                
                if (Console.ReadKey().Key == ConsoleKey.Escape) exit = true;
                Console.WriteLine("_________________________");
            } while (!exit);
            

            Console.ReadKey();


        }
    }
}
