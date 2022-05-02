using System;
using System.IO;


namespace File_Manager
{
    public class CatalogInformation
    {
        public static void InfoDirectory(string nameFile) // Проверяет если каталог и выводит инфрмацию о нем
        {
            if (Directory.Exists(nameFile))
            {
                DirectoryInfo directorySize = new DirectoryInfo(nameFile);
                FileAttributes attributes = File.GetAttributes(nameFile); // Атрибуты каталога
                Console.WriteLine($" {attributes} \n size: {SizeFileseDirectory(nameFile)} byte; \n {directorySize.LastWriteTime.ToLongDateString()} {directorySize.LastWriteTime.ToLongTimeString()} \n");
                Console.WriteLine();

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


    }
}
