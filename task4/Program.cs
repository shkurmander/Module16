using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace task4
{
    [Serializable]
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
        public static void ReadFile(string path)
        {
            // объект для сериализации
            var student = new Student("Петров", "ПИМ-21", DateTime.Now.AddYears(-18));
            Console.WriteLine("Объект создан");

            BinaryFormatter formatter = new BinaryFormatter();

            // получаем поток, куда будем записывать сериализованный объект
            using (var fs = new FileStream("students.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, student);
                Console.WriteLine("Объект сериализован");
            }

            //using (var fs = new FileStream(path, FileMode.Open))
            using (var fs = new FileStream("students.dat", FileMode.Open))
            {
                var newStudent = (Student)formatter.Deserialize(fs);
                Console.WriteLine($"Из файла\nСтудент: {newStudent.Name}\t{newStudent.Group}\t{newStudent.DateOfBirth}");
            }



        }


        static void Main(string[] args)
        {
            string path = @"c:\test\students.dat";
            //string patch = "";
            

            bool exit = false;
            do
            {
                // проверка на пустой ввод
                //do
                //{
                //    console.writeline("введите путь к файлу с данными:");
                //    patch = console.readline();
                //    if (patch == "") console.writeline("вы ничего не ввели!!!");

                //} while (patch.length < 1);

                if (File.Exists(path))
                {
                                        
                    Console.WriteLine("_________________________\n");
                    ReadFile(path);

                }
                else Console.WriteLine("Ошибка - файл не найден!");
                Console.WriteLine("Для выхода нажмите ESC, для продолжения - любую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Escape) exit = true;
                Console.WriteLine("_________________________\n");
            } while (!exit);


            Console.ReadKey();


        }
    }
}
