using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FinalTask
{
   
    [Serializable]   
    /// <summary>
    /// Класс сущности студента для десериализации
    /// </summary>
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateofbirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateofbirth;
        }
    }
    class Program
    {
        public static Student[] ReadFile(string path)
        {
            // объект для сериализации
            //var student = new Student("Петров", "ПИМ-21", DateTime.Now.AddYears(-18));
            //Console.WriteLine("Объект создан");

            //BinaryFormatter formatter = new BinaryFormatter();
            //// получаем поток, куда будем записывать сериализованный объект
            //using (var fs = new FileStream("students.dat", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, student);
            //    Console.WriteLine("Объект сериализован");
            //}

            BinaryFormatter formatter = new BinaryFormatter();

            //using (var fs = new FileStream("students.dat", FileMode.Open))
            Console.WriteLine($"Данные из файла:\n");
            //Десериализуем данные из файла и пишем их в массив объектов Students и попутно выводим
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var newStudent = (Student[])formatter.Deserialize(fs);
                foreach (var item in newStudent)
                {
                    Console.WriteLine($"Студент: {item.Name}\t{item.Group}\t{item.DateOfBirth}");
                }
                return newStudent;
            }

        }
        /// <summary>
        /// Метод печатающий содержимое папки
        /// </summary>
        /// <param name="patch"></param>
        /// <returns>Возвращает true если каталог существует, false  если нет</returns>
        public static bool ShowDirInfo(string pach)
        {
            DirectoryInfo dir = new DirectoryInfo(pach);

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
        /// Метод создает файлы по именам групп и закидывает в них данные о студентах
        /// </summary>
        /// <param name="args"></param>
        public static void SaveData(Student[] students)
        {
            //получаем путь к папке рабочего стола и создаем на нем папку Students
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\Students";            
            Directory.CreateDirectory(path); 

            //Выдергиваем из массива список уникальных номеров групп
            var groupList = new List<string>();
            foreach (var item in students)
            {
                if (!groupList.Contains(item.Group))
                {
                    groupList.Add(item.Group);
                }
            }
            //Создаем файлы соответвенно именам групп и пишем в них данные о студентах этих групп
            foreach (var item in groupList)
            {
                using (StreamWriter sw = File.CreateText(path + "\\" + item + ".txt"))
                {
                    foreach (var student in students)
                    {
                        if(student.Group == item)
                        {
                            sw.WriteLine($"{student.Name}\t{student.DateOfBirth}");
                        }
                    }
                }
            }
            Console.WriteLine("_________________________\n");
            Console.WriteLine("Данные о студентах распределены по файлам");
            ShowDirInfo(path);

        }

        static void Main(string[] args)
        {
            //string path = @"c:\test\students.dat";
            string path = "";
            

            bool exit = false;
            do
            {
                //проверка на пустой ввод
                do
                {
                    Console.WriteLine("введите путь к файлу с данными:");
                    path = Console.ReadLine();
                    if (path == "") Console.WriteLine("вы ничего не ввели!!!");

                } while (path.Length < 1);

                if (File.Exists(path))
                {
                                        
                    Console.WriteLine("_________________________\n");
                    var students = ReadFile(path);
                    SaveData(students);

                }
                else Console.WriteLine("Ошибка - файл не найден!");
                Console.WriteLine("Для выхода нажмите ESC, для продолжения - любую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Escape) exit = true;
                
            } while (!exit);


            Console.ReadKey();


        }
    }
}
