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

    class Searcher
    {
        public string FolderName { get; set; }
        public string FilePattern { get; set; }
        public string Text { get; set; }
        public int TotalFiles { get; set; }

        public FindedFile FindedFileDelegate { get; set; }
        public FinishedSearch FinishedSearchDelegate { get; set; }

        public void Find()
        {
            TotalFiles = 0;

            var fileString = Directory.EnumerateFiles(this.FolderName, this.FilePattern, SearchOption.AllDirectories);

            foreach (string file in fileString)
            {
                using (StreamReader streamReader = new StreamReader(file))
                {
                    while (streamReader.Peek() > -1)
                    {
                        string line = streamReader.ReadLine();

                        if (line.Contains(Text))
                        {
                            FindedFileDelegate(file);
                            break;
                        }
                    }
                }
                    
            }
            FinishedSearchDelegate();
        }
    }
}
