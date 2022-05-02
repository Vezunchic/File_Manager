using System;

using System.IO;

namespace File_Manager
{
    public class Paging
    {
        public static void CheckNumber(string command, Settings config) // Проверяет число или символ, выводит каталоги и файлы
        {
            bool result = int.TryParse(command, out int currentPage);
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
                Console.WriteLine("Каталоги:");

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
                Console.WriteLine("\nФайлы:");
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
    }
}
