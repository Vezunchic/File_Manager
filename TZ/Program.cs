using System;
using System.IO;



namespace File_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"FileInfo.txt";
            Settings config = Settings.LoadConfiguration(path);
            Console.WriteLine($"Исходный каталог: {config.PathGurenDirectory}\n");
            while (true)
            {
                Console.WriteLine("Введите команду или номер строки(строки начинаются с 0): ");
                string command = Console.ReadLine();
                Console.WriteLine("------------------------------");
                switch (command)
                {
                    case "h":
                    case "help":
                        {
                            Console.WriteLine($" help или h - помощь; \n del или d - удаление файла или каталога; \n copy или c - копирование файла или каталога;" +
                                $" \n info или i - информация о файле или каталоге; \n exit или ex - завершить программу; \n return или re - возврат к текущей рабочему каталогу;" +
                                $"\n search или s - поиск каталога или файла;\n enter или e - переход по заданному пути;\n rename или r - переименование файла или каталога; " +
                                $"\n Atributtes или A - изменение атрибутов файла;\n creature или cr - создание файла или каталога");
                            Console.WriteLine();
                        }
                        continue;
                    case "cr":
                    case "creature":
                        {
                            Console.WriteLine("В каком каталоге хотите создать файл или каталог?");
                            string nameFile = @$"{Console.ReadLine()}";

                            ExecutionCommand.Сreature(nameFile);

                            Console.WriteLine("\n -----------------------");

                        }
                        continue;
                        
                    case "a":
                    case "atributtes":
                        {
                            Console.WriteLine("В каком файле хотите изменить атрибуты. Название файла вводить с расширением.");
                            string nameFile = @$"{Console.ReadLine()}";
                            FileInformation.AttributeChanges(nameFile);
                            
                            Console.WriteLine("\n -----------------------");

                        }
                        continue;
                    case "r":
                    case "rename":
                        {
                            Console.WriteLine("Какой файл или каталог вы хотите переименовать? Название файла вводить с расширением");
                            string nameFile = @$"{Console.ReadLine()}";
                            ExecutionCommand.Rename(nameFile);
                            Console.WriteLine("\n -----------------------");

                        }
                        continue;
                    case "s":
                    case "search":
                        {
                            Console.WriteLine("Какой файл или каталог вы хотите найти? Название файла вводить с расширением");
                            string nameFile = @$"{Console.ReadLine()}";
                            ExecutionCommand.Search(nameFile, config.PathGurenDirectory);
                            Console.WriteLine("\n -----------------------");

                        }
                        continue;
                    case "e":
                    case "enter":
                        {
                            Console.WriteLine("Путь каталога в который хотите войти?");
                            string nameFile = @$"{Console.ReadLine()}";
                            ExecutionCommand.Switching(nameFile, config); 
                        }
                        continue;
                    case "d":
                    case "del":
                        {

                            Console.WriteLine("Введите адрес файла или каталога.");
                            string nameFile = @$"{Console.ReadLine()}";
                            if (File.Exists(nameFile))
                            {
                                ExecutionCommand.DeletFile(nameFile);
                            }
                            else
                            {
                                ExecutionCommand.DeletDirectory(nameFile);
                            }
                            Console.WriteLine("\n -----------------------");

                        }
                        continue;
                    case "c":
                    case "copy":
                        {
                            Console.WriteLine("Введите адрес файла или каталога");
                            string name = @$"{Console.ReadLine()}";

                            if (File.Exists(name))
                            {
                                ExecutionCommand.CopyFile(name);
                            }
                            else
                            {
                                ExecutionCommand.CopyDirectory(name);
                            }
                        }
                        continue;
                    case "i":
                    case "info":
                        {
                            Console.WriteLine("Введите адрес файла или каталога");
                            string nameFile = @$"{Console.ReadLine()}";
                            FileInformation.InfoFile(nameFile);
                            CatalogInformation.InfoDirectory(nameFile);

                        }
                        continue;
                    case "ex":
                    case "exit":
                        {
                            ExecutionCommand.Exit(path, config);
                            return;
                        }
                    case "re":
                    case "return": 
                        {
                            Console.WriteLine("Хотите вовратиться в исходный каталог? y или n.");
                            string confirmation = Console.ReadLine();
                            ExecutionCommand.Return(path, confirmation, config);

                        }
                        continue;
                }

                Paging.CheckNumber(command, config);

            }

        }













    }
}
