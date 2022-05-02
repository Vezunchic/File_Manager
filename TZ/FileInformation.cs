using System;

using System.IO;
using System.Text.RegularExpressions;

namespace File_Manager
{
    public class FileInformation
    {


        public static void AttributeChanges(string nameFile) // Изменение атрибутов файла
        {
            if (File.Exists(nameFile))
            {

                FileAttributes attributes = File.GetAttributes(nameFile); // Атрибуты файла
                Console.WriteLine(attributes);
                Console.WriteLine("На какой тип хотите изменить : 1 - Archive ; 2 - ReadOnly; 3 - Hidden; 4 - System; 0 - Normal; ");
                string typeAttributes = Console.ReadLine();
                bool result = int.TryParse(typeAttributes, out int currentVersion);
                if (result == true)
                {
                   var currenAttributes = RenameAttributes(currentVersion);
                    if ( currenAttributes == FileAttributes.Normal)
                    {
                        //Помечает, что файл без спец. аттрибутов.
                        attributes = FileAttributes.Normal;
                        File.SetAttributes(nameFile, attributes);
                        Console.WriteLine($" {File.GetAttributes(nameFile)} {nameFile}");
                        return;
                    }

                    if ((attributes & currenAttributes) == currenAttributes)
                    {
                        // если атрибут совпадает то отменяет его
                        attributes = RemoveAttribute(attributes, currenAttributes);
                        File.SetAttributes(nameFile, attributes);
                        Console.WriteLine($" {File.GetAttributes(nameFile)} {nameFile}");
                    }
                    else
                    {
                        //дополняет выбраный атрибут
                        File.SetAttributes(nameFile, File.GetAttributes(nameFile) | currenAttributes);
                        Console.WriteLine($" {File.GetAttributes(nameFile)} {nameFile}");
                    }
                    return;

                }
                else
                {
                    Console.WriteLine("Не верно ведена команда!");
                }
            }
            Console.WriteLine("Не верно введен адрес файла!");
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove) 
        {
            return attributes & ~attributesToRemove;
        }

        static FileAttributes RenameAttributes(int typeAttributes) // выбор атрибутов файла
            {
                switch (typeAttributes)
                {
                    case 1:
                        return FileAttributes.Archive;
                    case 2:
                        return FileAttributes.ReadOnly;
                    case 3:
                        return FileAttributes.Hidden;
                    case 4:
                        return FileAttributes.System;
                }
                return FileAttributes.Normal;
            }
        

        public static void InfoFile(string nameFile) //Проверяет если файл и выводит информацию о нем
        {
            if (File.Exists(nameFile))
            {

                FileInfo fileSize = new FileInfo(nameFile);
                FileAttributes attributes = File.GetAttributes(nameFile); // Атрибуты файла
                Console.WriteLine($" {attributes} \n size: {fileSize.Length} byte, \n {fileSize.LastWriteTime.ToLongDateString()} {fileSize.LastWriteTime.ToLongTimeString()} \n");


                string pathDirectory = Path.GetDirectoryName(nameFile);
                var result = Directory.GetFiles(pathDirectory, "*.txt");

                for (int i = 0; i <= result.Length - 1; i++)
                {

                    if (result[i] == nameFile)
                    {
                        NumberWordsWhitespace(nameFile);
                        NumberLines(nameFile);
                        NumberpParagraphs(nameFile);
                        NumberpCharSpaces(nameFile);
                        NumberpChar(nameFile);

                    }
                }

                return;
            }
            Console.WriteLine("Не верно введен адрес файла!");
        }
        static void NumberWordsWhitespace(string nameFile) //считает сколько слов для .txt
        {
            int result = 0;

            StreamReader words = new StreamReader(nameFile);
            var allWords = words.ReadToEnd();
            var text = allWords.Split(new char[] { ',', ' ', '.', '!', '?', ';', ':', '\t', '\\', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < text.Length; i++)
            {
                result++;
            }
            words.Close();
            Console.WriteLine($"Количество слов:{result}");

        }
        static void NumberLines(string nameFile) // считаест сколько строк .txt
        {
            StreamReader words = new StreamReader(nameFile);
            int quantity = 0;
            while (words.ReadLine() != null)
            {
                quantity++;
            }
            Console.WriteLine($"Количество строк: {quantity}");
            words.Close();
        }
        static void NumberpParagraphs(string nameFile)// считаест сколько парагрофоф .txt
        {

            StreamReader words = new StreamReader(nameFile);
            var allWords = words.ReadToEnd();

            var result = Regex.Matches(allWords, @"\t").Count;


            words.Close();
            Console.WriteLine($"Количество парагрофоф:{result}");


        }
        static void NumberpCharSpaces(string nameFile) //считает сколько символов .txt
        {
            StreamReader words = new StreamReader(nameFile);
            var allWords = words.ReadToEnd();
            int quantity = 0;
            char[] coupLine = allWords.ToCharArray();

            for (int i = 0; i < coupLine.Length; i++)
            {
                quantity++;
            }
            words.Close();
            Console.WriteLine($"Количество символов с пробелами:{quantity}");
        }
        static void NumberpChar(string nameFile)//считает сколько символов , без  пробелов .txt
        {
            int result = 0;
            StreamReader words = new StreamReader(nameFile);
            var text = words.ReadToEnd();
            char[] allChar = text.ToCharArray();
            for (int i = 0; i < allChar.Length; i++)
            {
                if (allChar[i] != ' ')
                {
                    result++;
                }
            }
            words.Close();
            Console.WriteLine($"Количество символов без пробелов:{result}");

        }
    }

}
