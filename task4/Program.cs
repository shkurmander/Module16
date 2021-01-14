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
        public static void ReadFile(string patch)
        {
            
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream(patch, FileMode.Open))
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
