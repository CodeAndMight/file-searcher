using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSearcher
{
    public delegate void FindedFile(string fileName);

    class Searcher
    {
        public string FolderName { get; set; }
        public string FilePattern { get; set; }
        public string Text { get; set; }

        public FindedFile FindedFileDelegate { get; set; }

        public void Find()
        {
            var fileString = Directory.GetFiles(this.FolderName, this.FilePattern);

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
        }
    }
}
