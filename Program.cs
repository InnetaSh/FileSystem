using System.IO;


//Вам нужно написать программу которая запрашивает у пользователя папку телеграмма в которой сохраняются все файлы. 
//    Вам нужно отсортировать данную папку по критерию (документы, медиа, аудио, фото) 
//    Для каждой категории создать папку в которую будут перемещены соответствующие файлы




Console.WriteLine("Введите путь к папке Telegram:");
string userPath = Console.ReadLine();

DirectoryInfo directory = new DirectoryInfo(userPath);
if (directory.Exists)
{
    Console.WriteLine($"папка {directory.FullName} найдена");

    FileInfo[] files = directory.GetFiles();
    if (files.Length > 0)
    {
        foreach (FileInfo file in files)
        {
            Console.WriteLine(file.Name);
        }
        Console.WriteLine("--------------------------------");
        FileInfo[] filesMP3 = directory.GetFiles("*.mp3");
        FileInfo[] filesMP4 = directory.GetFiles("*.mp4");
        FileInfo[] filesJPG = directory.GetFiles("*.jpg");
        FileInfo[] filesPDF = directory.GetFiles("*.pdf");

        void SortFile(FileInfo[] files,string FileExtension, string nameFile)
        {
            
            if (files.Length > 0)
            {
                Console.WriteLine($"{nameFile} файлы:");
                foreach (FileInfo file in files)
                {
                    Console.WriteLine(file.Name);
                }
            }
            else
            {
                Console.WriteLine($"файлы с расширением {FileExtension} не найдены");
            }
            Console.WriteLine("--------------------------------");
        }

        SortFile(filesMP3,"mp3", "аудио");
        SortFile(filesMP4, "mp4", "медиа");
        SortFile(filesJPG, "jpg", "фото");
        SortFile(filesPDF, "pdf", "документы");


        void CreateFolder(FileInfo[] files, string userPath, DirectoryInfo UserDir, string subPath)
        {
            if (files.Length > 0)
            {
                DirectoryInfo dir = new DirectoryInfo(@$"{userPath}\{subPath}");
                if (!dir.Exists)
                {
                    UserDir.CreateSubdirectory(subPath);
                    Console.WriteLine($"папка {subPath} создана");
                }
                else
                    Console.WriteLine($"папка {subPath} существует");
                Console.WriteLine("-------------------------------------");
            }

        }

        CreateFolder(filesMP3, userPath, directory, "audio");
        CreateFolder(filesMP4, userPath, directory, "media");
        CreateFolder(filesJPG, userPath, directory, "image");
        CreateFolder(filesPDF, userPath, directory, "document");



        void MoveFiles(FileInfo[] files, string userPath, string subPath)
        {
            if (files.Length > 0)
            {
                DirectoryInfo dir = new DirectoryInfo(@$"{userPath}\{subPath}");
                if (dir.Exists)
                {
                    foreach (var file in files)
                    {
                        File.Move(@$"{file.FullName}", @$"{userPath}\{subPath}\{file.Name}");
                    }
                    Console.WriteLine($"файлы перемещены в папку {subPath}");
                    Console.WriteLine("--------------------------------------------");
                }
                else
                {
                    Console.WriteLine($"папки {subPath} не существует");
                }
            }
        }

        MoveFiles(filesMP3, userPath, "audio");
        MoveFiles(filesMP4, userPath, "media");
        MoveFiles(filesJPG, userPath, "image");
        MoveFiles(filesPDF, userPath, "document");

       
    }
    else
    {
        Console.WriteLine($"папка {directory.Name} не содержит файлы.");
    }
}
else
{
    Console.WriteLine($"папка с названием {directory.Name} не существует");
   
}