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
            string[] dirs = null;

            try
            {
                dirs = Directory.GetDirectories(dirName);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Ignore Sys Dirs
                dirs = new string[0];
            }

            this.ProccessFiles(dirName);

            if (dirs.Length == 0) return;

            foreach (string dir in dirs)
            {
                this.ProccessFiles(dir);
                this.ProccessDir(dir);
            }
        }

        /// <summary>
        /// Обходит все файлы
        /// </summary>
        /// <param name="dirName"></param>
        public void ProccessFiles(string dirName)
        {
            string[] files = null;

            try
            {
                files = Directory.GetFiles(dirName, this.FilePattern);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Ignore Sys Files
            }

            if (files == null) return;

            foreach (string file in files)
            {
                if (CurrentFileProcessDelegate != null) CurrentFileProcessDelegate(file);
                using (StreamReader streamReader = new StreamReader(file))
                {
                    TotalFiles++;
                    while (streamReader.Peek() > -1)
                    {
                        string line = streamReader.ReadLine();

                        if (line.Contains(Text))
                        {
                            if (FindedFileDelegate != null) FindedFileDelegate(file);
                            break;
                        }
                    }
                    if (TotalFilesInfoDelegate != null) TotalFilesInfoDelegate(TotalFiles);
                }
            }
        }
    }
}
