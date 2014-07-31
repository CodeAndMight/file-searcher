using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSearcher
{
    public delegate void FindedFile(string fileName);
    public delegate void FinishedSearch();
    public delegate void TotalFilesInfo(int total);
    public delegate void CurrentFileProcess(string fileName);

    class Searcher
    {
        public string FolderName { get; set; } // Директория для поиска
        public string FilePattern { get; set; } // Шаблон для поиска файлов
        public string Text { get; set; } // Искомый текст в файле
        public int TotalFiles { get; set; } // Обработанное количество файлов

        public FindedFile FindedFileDelegate { get; set; } // Сообщает о найденном файле
        public FinishedSearch FinishedSearchDelegate { get; set; } // Сообщает о завершении работы поиска
        public TotalFilesInfo TotalFilesInfoDelegate { get; set; } // Сообщает о текущем количестве файлов
        public CurrentFileProcess CurrentFileProcessDelegate { get; set; } // Сообщает о файле

        /// <summary>
        /// Вызывается потоком для поиска подходящих файлов
        /// </summary>
        public void Find()
        {
            TotalFiles = 0;

            if (!Directory.Exists(this.FolderName))
            {
                if (FinishedSearchDelegate != null) FinishedSearchDelegate();
                return;
            }

            this.ProccessDir(this.FolderName);

            if (FinishedSearchDelegate != null) FinishedSearchDelegate();
        }

        /// <summary>
        /// Обходит все каталоги
        /// </summary>
        /// <param name="dirName"></param>
        public void ProccessDir(string dirName)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(dirName);

                foreach (string dir in dirs)
                {
                    this.ProccessFiles(dir);
                    this.ProccessDir(dir);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Ignore Sys Dirs
            }
            //Console.WriteLine("==============="+dirName);
        }

        /// <summary>
        /// Обходит все файлы
        /// </summary>
        /// <param name="dirName"></param>
        public void ProccessFiles(string dirName)
        {
            try
            {
                string[] files = Directory.GetFiles(dirName, this.FilePattern);

                this.Lookup(files);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Ignore Sys Files
            }
            //Console.WriteLine("++++++++++++++"+dirName);
        }

        public void Lookup(string[] files)
        {
            foreach (string file in files)
            {
                if (CurrentFileProcessDelegate != null) CurrentFileProcessDelegate((string)file.Clone());
                using (StreamReader streamReader = new StreamReader(file))
                {
                    TotalFiles++;
                    while (streamReader.Peek() > -1)
                    {
                        string line = streamReader.ReadLine();

                        if (line.Contains(Text))
                        {
                            if (FindedFileDelegate != null) FindedFileDelegate((string)file.Clone());
                            break;
                        }
                    }
                    if (TotalFilesInfoDelegate != null) TotalFilesInfoDelegate(TotalFiles);
                }
            }
            //Console.WriteLine("-----------------");
        }
    }
}
