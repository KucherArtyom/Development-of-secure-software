using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ConsoleApp36
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере и типе файловой системы \n" +
            "2. Работа с файлами \n" +
            "3. Работа с форматом JSON \n" +
            "4. Работа с форматорм XML \n" +
            "5. Создание zip архива, добавление туда файла, определение размера архива");
                int n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        DriveInfo[] drives = DriveInfo.GetDrives();

                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine($"Название: {drive.Name}");
                            Console.WriteLine($"Тип: {drive.DriveType}");
                            if (drive.IsReady)
                            {
                                Console.WriteLine($"Объем диска: {drive.TotalSize}");
                                Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        FileTxt txtFile = new FileTxt();
                        Filename fn = new Filename();
                        Console.WriteLine("1. Создать файл \n" +
                        "2. Записать в файл строку, введённую пользователем \n" +
                        "3. Прочитать файл в консоль \n" +
                        "4. Удалить файл");
                        n = Convert.ToInt32(Console.ReadLine());
                        switch (n)
                        {
                            case 1:
                                txtFile.TxtPath();
                                txtFile.CreateTxtFile();
                                break;
                            case 2:
                                txtFile.TxtPath();
                                txtFile.StringTxtFile();
                                break;
                            case 3:
                                txtFile.TxtPath();
                                txtFile.ConsoleTxtFile();
                                break;
                            case 4:
                                txtFile.TxtPath();
                                txtFile.DeleteTxtFile();
                                break;
                        }
                        break;
                    case 3:
                        FileJson jsonfile = new FileJson();
                        Console.WriteLine("1. Создать файл формате JSON в любом редакторе или с использованием данных, введенных пользовователем \n" +
                        "2. Создать новый объект. Выполнить сериализацию объекта в формате JSON и записывать в файл \n" +
                        "3. Прочитать файл в консоль \n" +
                        "4. Удалить файл");
                        n = Convert.ToInt32(Console.ReadLine());
                        switch (n)
                        {
                            case 1:
                                jsonfile.JsonPath();
                                jsonfile.JsconCreate();
                                break;
                            case 2:
                                jsonfile.JsonPath();
                                jsonfile.JsonObject();
                                break;
                            case 3:
                                jsonfile.JsonPath();
                                jsonfile.JsonRead();
                                break;
                            case 4:
                                jsonfile.JsonPath();
                                jsonfile.DeleteJsonFile();
                                break;
                        }
                        break;
                    case 4:
                        FileXml xmlFile = new FileXml();
                        Console.WriteLine("1. Создать файл в формате XML из редактора \n" +
                        "2. Записать в файл новые данные из консоли \n" +
                        "3. Прочитать файл в консоль \n" +
                        "4. Удалить файл");
                        n = Convert.ToInt32(Console.ReadLine());
                        switch (n)
                        {
                            case 1:
                                xmlFile.XmlPath();
                                xmlFile.XmlCreate();
                                break;
                            case 2:
                                xmlFile.XmlPath();
                                xmlFile.XmlData();
                                break;
                            case 3:
                                xmlFile.XmlPath();
                                xmlFile.XmlRead();
                                break;
                            case 4:
                                xmlFile.XmlPath();
                                xmlFile.XmlDelete();
                                break;
                        }
                        break;
                    case 5:
                        FileZip zipFile = new FileZip();
                        Console.WriteLine("1. Создать архив в формате zip \n" +
                        "2. Добавить файл, выбранный пользователем, в архив \n" +
                        "3. Разархивировать файл и вывести данныне о нём \n" +
                        "4. Удалить файл и архив");
                        n = Convert.ToInt32(Console.ReadLine());
                        switch (n)
                        {
                            case 1:
                                zipFile.ZipPath();
                                zipFile.CreateZipFile();
                                break;
                            case 2:
                                zipFile.ZipPath();
                                zipFile.FileNamePath();
                                zipFile.AddFile();
                                zipFile.InfoZip();
                                break;
                            case 3:
                                zipFile.ZipPath();
                                zipFile.FileNamePath();
                                zipFile.ExtraxtFile();
                                zipFile.InfoFile();
                                break;
                            case 4:
                                zipFile.ZipPath();
                                zipFile.DeleteZipFile();
                                break;
                        }
                        break;
                }

            }
        }
    }

    class Information
    {

    }
    class FileTxt
    {
        string txtfilename;

        public string TxtFileName
        {
            get { return txtfilename; }
            set { txtfilename = value; }
        }
        public string TxtPath()
        {
            Console.WriteLine("Введите название текстового файла и его тип: ");
            txtfilename = Console.ReadLine();
            return txtfilename;
        }
        public void CreateTxtFile()
        {
            StreamWriter file = File.CreateText(txtfilename);
            file.Close();
        }
        public void DeleteTxtFile()
        {
            FileInfo fileInf = new FileInfo(txtfilename);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
            else
            {
                Console.WriteLine("Данный файл уже удален!");
            }
        }
       
        public void StringTxtFile()
        {
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();
            using (FileStream fstream = new FileStream($"{txtfilename}", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }
        public void ConsoleTxtFile()
        {
            using (FileStream fstream = File.OpenRead($"{txtfilename}"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
        }
        

    }
    class FileXml
    {
        string xmlfilename;

        public string XmlFileName
        {
            get { return xmlfilename; }
            set { xmlfilename = value; }
        }

        public string XmlPath()
        {
            Console.WriteLine("Введите название xml-файла: ");
            xmlfilename = Console.ReadLine();
            return xmlfilename;
        }

        public void XmlCreate()
        {
            FileInfo file = new FileInfo(xmlfilename);
            if (!file.Exists)
            {
                file.Create();
            }
        }
        public void XmlRead()
        {
            using (XmlTextReader reader = new XmlTextReader(xmlfilename))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.Write("<" + reader.Name);
                            Console.WriteLine(">");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            Console.Write("</" + reader.Name);
                            Console.WriteLine(">");
                            break;
                    }
                }
            }
        }
        public void XmlDelete()
        {
            FileInfo fileInf = new FileInfo(xmlfilename);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
        public void XmlData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlfilename);
            Console.Write("Введите имя элемента: ");
            string elementName = Console.ReadLine();

            Console.Write("Введите значение: ");
            string elementValue = Console.ReadLine();

            XmlElement newElement = doc.CreateElement(elementName);
            newElement.InnerText = elementValue;
            doc.DocumentElement?.AppendChild(newElement);
            doc.Save(xmlfilename);
            Console.WriteLine("Новые данные успешно добавлены в XML-файл.");
        }
    }
    class FileJson
    {
        string jsonfilename;

        public string JsonFileName
        {
            get { return jsonfilename; }
            set { jsonfilename = value; }
        }
        public string Name { get; set; }
        public int Value { get; set; }

        public string JsonPath()
        {
            Console.WriteLine("Введите название json-файла: ");
            jsonfilename = Console.ReadLine();
            return jsonfilename;
        }
        public void JsconCreate()
        {
            FileInfo file = new FileInfo(jsonfilename);
            if (!file.Exists)
            {
                file.Create();
                StreamWriter file1 = File.CreateText(jsonfilename);
                file1.Close();
            }
        }
        public void DeleteJsonFile()
        {
            FileInfo fileInf = new FileInfo(jsonfilename);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
            else
            {
                Console.WriteLine("Данный файл уже удален!");
            }
        }

        public void JsonObject()
        {
            string jsonContent = File.ReadAllText(jsonfilename);

            Console.WriteLine("Введите значение поля 'name':");
            string key = Console.ReadLine();

            Console.WriteLine("Введите значение поля 'value':");
            int argument = int.Parse(Console.ReadLine());
            List<FileJson> jsondata = new List<FileJson>();
            jsondata.Add(new FileJson { Name = key, Value = argument });

            string json = System.Text.Json.JsonSerializer.Serialize(jsondata);

            File.WriteAllText(jsonfilename, json);
        }
        public void JsonRead()
        {
            if (!File.Exists(jsonfilename))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }
            string jsonContent = File.ReadAllText(jsonfilename);
            var jsonData = JsonConvert.DeserializeObject(jsonContent);
            Console.WriteLine(jsonData);
        }

    }
    class FileZip
    {
        string zipfilename;

        public string ZipFileName
        {
            get { return zipfilename; }
            set { zipfilename = value; }
        }

        string filename;

        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }


        public string ZipPath()
        {
            Console.WriteLine("Введите название zip-архива: ");
            zipfilename = Console.ReadLine();
            return zipfilename;
        }
        public string FileNamePath()
        {
            Console.WriteLine("Введите название файла, которое хотите добавить в архив: ");
            filename = Console.ReadLine();
            return filename;
        }

        public void CreateZipFile()
        {
            StreamWriter file = File.CreateText(zipfilename);
            file.Close();
        }
        public void DeleteZipFile()
        {
            FileInfo fileInf = new FileInfo(zipfilename);
            if (fileInf.Exists)
            {
                Console.WriteLine("Данный файл уже удален существует!");
                fileInf.Delete();
            }
        }
        public void AddFile()
        {
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
            {
                Console.WriteLine("Данный файл не существует!");
            }
            else
            {
                using (ZipArchive zipArchive = ZipFile.Open(zipfilename, ZipArchiveMode.Update))
                {
                    zipArchive.CreateEntryFromFile(filename, filename);
                }
            }
            
        }
        public void ExtraxtFile()
        {
            using (ZipArchive zipArchive = ZipFile.OpenRead(zipfilename))
            {
                zipArchive.Entries.FirstOrDefault(x => x.Name == filename)?.
                ExtractToFile(filename);
            } 
        }
        public void InfoFile()
        {
            FileInfo fileInf = new FileInfo(filename);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
        }
        public void InfoZip()
        {
            FileInfo fileInf = new FileInfo(zipfilename);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
        }
        public void Problem()
        {
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
            {
                Console.WriteLine("Данный файл не существует!");
            }
            else
            {
                using (ZipArchive zipArchive = ZipFile.Open(zipfilename, ZipArchiveMode.Update))
                {
                    zipArchive.CreateEntryFromFile(filename, filename);
                }
            }
        }
    }
    class Filename
    {
        string filename1;

        public string FileName1
        {
            get { return filename1; }
            set { filename1 = value; }
        }

        public string Path()
        {
            Console.WriteLine("Введите название файла: ");
            filename1 = Console.ReadLine();
            return filename1;
        }

    }
}
