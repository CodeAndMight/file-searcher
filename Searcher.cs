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
        public string FolderName { get; set; }
        public string FilePattern { get; set; }
        public string Text { get; set; }
        public int TotalFiles { get; set; }

        public FindedFile FindedFileDelegate { get; set; }
        public FinishedSearch FinishedSearchDelegate { get; set; }
        public TotalFilesInfo TotalFilesInfoDelegate { get; set; }
        public CurrentFileProcess CurrentFileProcessDelegate { get; set; }

        public void Find()
        {
            TotalFiles = 0;

            if (!Directory.Exists(this.FolderName))
            {
                if (FinishedSearchDelegate != null) FinishedSearchDelegate();
                return;
            }

            var fileString = Directory.EnumerateFiles(this.FolderName, this.FilePattern, SearchOption.AllDirectories);

            foreach (string file in fileString)
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
            if (FinishedSearchDelegate != null) FinishedSearchDelegate();
        }
    }
}
