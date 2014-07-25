using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FileSearcher
{
    public partial class Form1 : Form
    {
        private Searcher searcher;
        private Thread currentThread;

        delegate void AddNodeCallback(string text);

        public Form1()
        {
            InitializeComponent();

            searcher = new Searcher();

            searcher.FindedFileDelegate = new FindedFile(actionFindedFile);
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            fileTreeView.Nodes.Clear();

            searcher.FolderName = this.starterDirTextBox.Text;
            searcher.FilePattern = this.filePatternTextBox.Text;
            searcher.Text = this.searcherTextBox.Text;

            this.currentThread = new Thread(searcher.Find);
            this.currentThread.Start();
        }

        public void actionFindedFile(string fileName)
        {
            this.addNode(fileName);
        }

        private void addNode(string fileName)
        {
            if (this.fileTreeView.InvokeRequired)
            {
                AddNodeCallback d = new AddNodeCallback(addNode);
                this.Invoke(d, new object[] { fileName });
            }
            else
            {                
                fileTreeView.BeginUpdate();
                fileTreeView.Nodes.Add(fileName);
                fileTreeView.EndUpdate();
            }
        }
    }
}
