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
using System.IO;
using System.Timers;

namespace FileSearcher
{
    public partial class Form1 : Form
    {
        private Searcher searcher;
        private Thread currentThread;
        private bool isSearching;
        private System.Timers.Timer timer;
        private DateTime now;

        public Form1()
        {
            InitializeComponent();

            isSearching = false;

            searcher = new Searcher();
            searcher.FindedFileDelegate = new FindedFile(actionFindedFile);
            searcher.FinishedSearchDelegate = new FinishedSearch(finishedSearch);
            searcher.TotalFilesInfoDelegate = new TotalFilesInfo(DisplayTotalFiles);
            searcher.CurrentFileProcessDelegate = new CurrentFileProcess(DisplayCurrentFileLabel);

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
        }

        public void DisplayCurrentFileLabel(string fileName)
        {
            this.currentFileLabel.BeginInvoke(new MethodInvoker(delegate()
            {
                this.currentFileLabel.Text = fileName;
            }));
        }

        public void DisplayTotalFiles(int total)
        {
            this.fileAmountLabel.BeginInvoke(new MethodInvoker(delegate()
            {
                this.fileAmountLabel.Text = total.ToString();
            }));
        }

        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            this.remainTimeLabel.BeginInvoke(new MethodInvoker(delegate()
            {
                this.remainTimeLabel.Text = Math.Floor((e.SignalTime - this.now).TotalSeconds).ToString();
            }));
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                currentThread.Abort();
                this.finishedSearch();
                return;
            }

            now = DateTime.Now;
            timer.Start();
            isSearching = true;
            this.actionButton.Text = "Остановить";
            this.remainTimeLabel.Text = "0";

            fileTreeView.Nodes.Clear();

            searcher.FolderName = this.starterDirTextBox.Text;
            searcher.FilePattern = this.filePatternTextBox.Text;
            searcher.Text = this.searcherTextBox.Text;

            this.currentThread = new Thread(searcher.Find);
            this.currentThread.Start();
        }

        public void finishedSearch()
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                isSearching = false;
                this.actionButton.Text = "Найти";
                timer.Stop();
            }));            
        }

        public void actionFindedFile(string fileName)
        {
            this.fileTreeView.BeginInvoke(new MethodInvoker(delegate()
            {
                string[] filePath = fileName.Split(new char[] { '\\' });

                fileTreeView.BeginUpdate();

                TreeNodeCollection currentNode = fileTreeView.Nodes;
                TreeNode[] nodes = null;

                foreach (string path in filePath)
                {
                    nodes = currentNode.Find(path, false);
                    if (nodes.Length > 0)
                    {
                        currentNode = nodes[0].Nodes;
                    }
                    else
                    {
                        TreeNode node = currentNode.Add(path, path);
                        currentNode = node.Nodes;
                    }
                }
                
                fileTreeView.EndUpdate();
            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.currentThread.IsAlive)
            {
                this.currentThread.Abort();
                this.currentThread.Join();
            }

            StreamWriter writer = new StreamWriter("FormSettings.txt");

            writer.WriteLine(this.starterDirTextBox.Text);
            writer.WriteLine(this.filePatternTextBox.Text);
            writer.WriteLine(this.searcherTextBox.Text);

            writer.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("FormSettings.txt"))
            {
                StreamReader reader = new StreamReader("FormSettings.txt");

                string buf = reader.ReadLine();
                if (buf != null)
                {
                    this.starterDirTextBox.Text = buf;
                }

                buf = reader.ReadLine();
                if (buf != null)
                {
                    this.filePatternTextBox.Text = buf;
                }

                buf = reader.ReadLine();
                if (buf != null)
                {
                    this.searcherTextBox.Text = buf;
                }

                reader.Close();
            }
        }
    }
}
