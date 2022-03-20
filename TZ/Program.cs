using Newtonsoft.Json;
using System;
using System.IO;



namespace File_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"FileInfo.txt";
            Settings config = LoadConfiguration(path);
            Console.WriteLine($"Исходный каталог: {config.PathGurenDirectory}\n");
            while (true)
            {
                Console.WriteLine("Введите команду или номер строки: ");
                string command = Console.ReadLine();
                Console.WriteLine("------------------------------");
                switch (command)
                {
                    case "h":
                    case "help":
                        {
                            Console.WriteLine($" help или h - помощь; \n del или d - удаление файла или каталога; \n copy или c - копирование файла или каталога; \n info или i - информация о файле или каталоге; \n exit или ex - завершить программу; \n refund или r - возврат к текущей рабочему каталогу.");
                            Console.WriteLine();
                        }
                        continue;
                    case "e":
                    case "enter":
                        {
                            Console.WriteLine("Путь каталога в который хотите войти?");
                            string nameFile = @$"{Console.ReadLine()}";
                            if (Directory.Exists(nameFile))
                            {
                                config.PathGurenDirectory = nameFile;
                            }
                            else
                            {
                                Console.WriteLine("Каталог не найден");
                            }
                        }
                        continue;
                    case "d":
                    case "del":
                        {

                            Console.WriteLine("Введите адрес файла или каталога.");
                            string nameFile = @$"{Console.ReadLine()}";
                            if (File.Exists(nameFile))
                            {
                                DeletFile(nameFile);
                            }
                            else 
                            {
                                DeletDirectory(nameFile);
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
                            CopyFile(name);
                            }
                            else 
                            { 
                            CopyDirectory(name);
                            }
                        }
                        continue;
                    case "i":
                    case "info":
                        {
                            Console.WriteLine("Введите адрес файла или каталога");
                            string nameFile = @$"{Console.ReadLine()}";
                            InfoFile(nameFile);
                            InfoDirectory(nameFile);
                           

                        }
                        continue;
                    case "ex":
                    case "exit":
                        {
                            string toFile = JsonConvert.SerializeObject(config);
                            using (var sw = File.CreateText(path))
                            {
                                sw.WriteLine(toFile);
                            }
                            return;
                        }
                    case "r":
                    case "refund":
                        {
                            Console.WriteLine("Хотите вовратиться в исходный каталог? y или n.");
                            string confirmation = Console.ReadLine();

                            if (confirmation == "y")
                            {
                                path = Environment.CurrentDirectory;
                                Console.WriteLine($"Текущий каталог: {path}\n -----------------------");
                                config.PathGurenDirectory = path;
                                
                            }
                        }
                        continue;
                }

                CheckNumber(command, config);
              
            }

        }

        private static void CheckNumber(string command, Settings config) // Проверяет число или символ, выводит каталоги и файлы
        {
            bool result = int.TryParse(command, out var currentPage);
            if (result == true)
            {
                TreeDirectory(config.PathGurenDirectory, currentPage, config.NumberElementsPage);// вывод каталогов и файлов в текущем каталоге
            }
            else
            {
                Console.WriteLine("Нет такой команды!");
            }
        }

        static void TreeDirectory(string pathToFindToDirectoria, int page, int outputSize) //вывод каталогов и файлов в текущем каталоге
        {

            int skippingFiles = outputSize * page;
            int filesShow = outputSize * page + outputSize;
            try
            {
                string[] treeDirectory = Directory.GetDirectories(pathToFindToDirectoria); //получает каталоги из текущего каталога
                string[] fileTree = Directory.GetFiles(pathToFindToDirectoria);//получает файлы из текущего каталога

                for (int i = skippingFiles; i < filesShow; i++) // вывод каталогов
                {
                    if (treeDirectory.Length <= i)
                    {
                        Console.WriteLine("\n------------------------------");

                        Console.WriteLine("каталогов больше нет! \n ------------------------------");
                        break;
                    }
                    Console.WriteLine($"{treeDirectory[i]}");
                }
                for (int j = skippingFiles; j < filesShow; j++) // вывод файлов
                {
                    if (fileTree.Length <= j)
                    {
                        Console.WriteLine("\n------------------------------");
                        Console.WriteLine("файлов больше нет! \n ------------------------------");
                        break;
                    }
                    Console.WriteLine($"{fileTree[j]}");
                }
                Console.WriteLine($"Страница: {page}\n------------------------------"); ;
            }
            catch (Exception ex)
            
                {
                    Console.WriteLine($" Ошибка: {ex.Message} ");

                }
        }
        

        private static void InfoDirectory(string nameFile) // Проверяет если каталог и выводит инфрмацию о нем
        {
            if (Directory.Exists(nameFile))
            {
                DirectoryInfo directorySize = new DirectoryInfo(nameFile);
                FileAttributes attributes = File.GetAttributes(nameFile); // Атрибуты каталога
                Console.WriteLine($" {attributes} \n size: {SizeFileseDirectory(nameFile)} byte; \n {directorySize.LastWriteTime.ToLongDateString()} {directorySize.LastWriteTime.ToLongTimeString()} \n");
                Console.WriteLine();

            }
        }

        private static void InfoFile(string nameFile) //Проверяет если файл и выводит информацию о нем
        {
            if (File.Exists(nameFile))
            {
                FileInfo fileSize = new FileInfo(nameFile);
                FileAttributes attributes = File.GetAttributes(nameFile); // Атрибуты файла
                Console.WriteLine($" {attributes} \n size: {fileSize.Length} byte, \n {fileSize.LastWriteTime.ToLongDateString()} {fileSize.LastWriteTime.ToLongTimeString()} \n");
                Console.WriteLine();
            }
        }

        private static void CopyDirectory(string name)// Проверяет существует ли каталог и копирует его
        {
            if (Directory.Exists(name)) // Проверяет каталог или нет
            {
                Console.WriteLine("Куда хотите скопировать?");
                string newPath = @$"{Console.ReadLine()}";

                if (Directory.Exists(newPath)) // Проверяет если каталог в заданном пути
                {
                    DirectoryCopy(name, newPath, true); // копирует все каталоги и файлы
                    Console.WriteLine("Копирование завершено.");
                }
                else
                {
                    Console.WriteLine("Не верный адрес");
                }
            }
            else 
            {
                Console.WriteLine("Такого каталога нет!");
            }
        } 

        private static void CopyFile(string name) //Копирует файл
        {

            Console.WriteLine("Куда хотите скопировать?");
            string newPath = @$"{Console.ReadLine()}";
            string nameFile = Path.GetFileName(name); // наименование файла
                                              
            string destFile = Path.Combine(newPath, nameFile);// путь к файлу куда копируют
            
                if (Directory.Exists(newPath)) // проверяет если каталог в заданном пути
                {
                    File.Copy(name, destFile, true);
                    Console.WriteLine("Файл скопирован.");
                }
                else
                {
                    Console.WriteLine("Не верный адрес");

                }
            
        } 

        private static void DeletDirectory(string nameFile) // Удалоение каталога
        {
            if (Directory.Exists(nameFile))
            {
                Console.WriteLine("Хотите удалить каталог? y или n.");
                string confirmation = Console.ReadLine();
                if (confirmation == "y")
                {
                    Directory.Delete(nameFile, true);
                    Console.WriteLine("Каталог удален!");
                }
            }
            else
            {
                Console.WriteLine("Такого каталога нет!");
            }
        }

        private static void DeletFile(string nameFile) // Удаление файла
        {
            
                Console.WriteLine("Хотите удалить файл? y или n.");
                string confirmation = Console.ReadLine();
                if (confirmation == "y")
                {
                    File.Delete(nameFile);
                    Console.WriteLine("Файл удален!");
                }
        }
           
        

        static long SizeFileseDirectory(string nameFile) // считает размер каталога
        {

                string[] fileTree = Directory.GetFiles(nameFile);
                long sum = 0;
                for (int i = 0; i < fileTree.Length; i++)
                {
                    FileInfo fileSize = new FileInfo(fileTree[i]);
                    sum = 0 + fileSize.Length;


                }
                return sum;

        }
        static void DirectoryCopy(string name, string newPath, bool confirmation)// копирует все подкаталоги и файлы в каталоге
        {
                //Копирует каталоги из текущего каталога
                foreach (string dirPath in Directory.GetDirectories(name, "*.*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(name, newPath));
                }
                // Копирует все файлы из текущего каталога
                foreach (string Path in Directory.GetFiles(name, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(Path, Path.Replace(name, newPath), confirmation);
                }
        } 

        private static Settings LoadConfiguration(string path) // берет путь и количество элементов которые будут выводиться на одной странице из файла, если файла нет то создает его 
        {
                if (File.Exists(path))
                {
                var textToFile = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject <Settings> (textToFile);
                return config;
                }
                 var configuration = new Settings();
                string toFile = JsonConvert.SerializeObject(configuration);
                using (var sw = File.AppendText(path)) 
                {
                sw.WriteLine(toFile);
                }
                return configuration;
                 
        }
    }
}
