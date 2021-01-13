using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace task4
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime BirthDate { get; set; }

        public Student(string name, string group, DateTime bdate)
        {
            Name = name;
            Group = group;
            BirthDate = bdate;
        }
    }
    class Program
    {
        public static void ReadFile(string patch)
        {
            string name;
            string group;
            DateTime BirthDate;
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream(patch, FileMode.OpenOrCreate))
            {
                var newStudent = (Student)formatter.Deserialize(fs);              

            }

        }
        static void Main(string[] args)
        {
            string patch = @"c:\test\students.dat";
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

                if (File.Exists(patch))
                {
                                        
                    Console.WriteLine("_________________________\n");
                    ReadFile(patch);

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
