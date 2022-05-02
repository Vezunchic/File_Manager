using Newtonsoft.Json;
using System;
using System.IO;

namespace File_Manager
{
    public class ExecutionCommand
    {
       
        public static void Exit(string path, Settings config) //Завершение программы
        {
            string toFile = JsonConvert.SerializeObject(config);
            using (var sw = File.CreateText(path))
            {
                sw.WriteLine(toFile);
            }
        }
        public static void Return(string path, string confirmation, Settings config) // переход на каталог с программой
        {
            if (confirmation == "y")
            {
                path = Environment.CurrentDirectory;
                Console.WriteLine($"Текущий каталог: {path}\n -----------------------");
                config.PathGurenDirectory = path;

            }
        }
        public static void Switching(string nameFile, Settings config) // переход по пути
        {
            if (Directory.Exists(nameFile))
            {
                config.PathGurenDirectory = nameFile;
            }
            else
            {
                Console.WriteLine("Каталог не найден");
            }
        }
        public static void Rename(string nameFile) //Изменение имени файла или каталога
        {
            if (Directory.Exists(nameFile) || File.Exists(nameFile))
            {
                Console.WriteLine("На какое имя хотите переименовать? Не забывайте расширение файла.");
                string newNameFile = @$"{Console.ReadLine()}";
                string nameCurrentFile = Path.GetFileName(newNameFile);
                string pathDirectory = Path.GetDirectoryName(nameFile);
                string newPathDirectory = ($"{pathDirectory}\\{nameCurrentFile}");
                try
                {
                    Directory.Move(nameFile, newPathDirectory); // Перемещает файл или каталог со всем его содержимым в новое местоположение.
                    Console.WriteLine(newPathDirectory);
                    Console.WriteLine("Файл или каталог переименован.");
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Такого каталога или файла нет! \n ------------------------------");
            }


        }

        public static void Search(string nameFile, string path)//Поиск имени файла или каталога
        {
            try
            {

                string nameCurrentFile = Path.GetFileName(nameFile);
                string[] resultDirectory = Directory.GetDirectories(path, nameCurrentFile, SearchOption.AllDirectories);
                string[] resultFiles = Directory.GetFiles(path, nameCurrentFile, SearchOption.AllDirectories);
                if (nameFile == "" || resultDirectory.Length == 0 && resultFiles.Length == 0)
                {
                    Console.WriteLine("Такого каталога или файла нет! \n ------------------------------");
                    return;
                };
                // поиск каталогов с заданным именем
                foreach (string directory in resultDirectory)
                {
                    Console.WriteLine($"\nРезультат поиска :{directory} ");
                }
                // Поиск файлов с заданным именем
                foreach (string file in resultFiles)
                {
                    Console.WriteLine($"\nРезультат поиска :{file} ");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        public static void CopyDirectory(string name)// Проверяет существует ли каталог и копирует его
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

        public static void CopyFile(string name) //Копирует файл
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

        public static void DeletDirectory(string nameFile) // Удалоение каталога
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

        public static void DeletFile(string nameFile) // Удаление файла
        {

            Console.WriteLine("Хотите удалить файл? y или n.");
            string confirmation = Console.ReadLine();
            if (confirmation == "y")
            {
                File.Delete(nameFile);
                Console.WriteLine("Файл удален!");
            }
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
    }
}
