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
        private Searcher searcher; // Вызывается потоком для поиска подходящих файлов
        private Thread currentThread; // Запускающий поток
        private bool isSearching; // Индикатор работы поиска
        private System.Timers.Timer timer; // Отсчитывает прошедшее время
        private DateTime now; // Старт поиска

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

        /// <summary>
        /// Выводит текущий файл
        /// </summary>
        /// <param name="fileName"></param>
        public void DisplayCurrentFileLabel(string fileName)
        {
            this.currentFileLabel.BeginInvoke(new MethodInvoker(delegate()
            {
                this.currentFileLabel.Text = fileName;
            }));
        }

        /// <summary>
        /// Выводит общее количество обработанных файлов
        /// </summary>
        /// <param name="total"></param>
        public void DisplayTotalFiles(int total)
        {
            this.fileAmountLabel.BeginInvoke(new MethodInvoker(delegate()
            {
                this.fileAmountLabel.Text = total.ToString();
            }));
        }

        /// <summary>
        /// Выводит прошедшее время поиска
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            this.remainTimeLabel.BeginInvoke(new MethodInvoker(delegate()
            {
                this.remainTimeLabel.Text = Math.Floor((e.SignalTime - this.now).TotalSeconds).ToString();
            }));
        }

        /// <summary>
        /// Запускает/Останавливает поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Завершающий этап поиска, остановка таймера, возврат полей интерфейса в исходное состояние
        /// </summary>
        public void finishedSearch()
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                isSearching = false;
                this.actionButton.Text = "Найти";
                timer.Stop();
            }));            
        }

        /// <summary>
        /// Вызывается, когда найден подходящий файл. Добавляет имя файла в TreeView в соответствии с иерархией пути файла
        /// </summary>
        /// <param name="fileName"></param>
        public void actionFindedFile(string fileName)
        {
            if (this.fileTreeView.InvokeRequired)
            {
                this.fileTreeView.Invoke((MethodInvoker)delegate()
                {
                    this.actionFindedFile(fileName);
                });
            }
            else
            {
                this.AddItem(fileName);
            }
        }

        public void AddItem(string fileName)
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
        }

        /// <summary>
        /// Сохраняет настройки, когда закрывается приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.currentThread != null && this.currentThread.IsAlive)
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

        /// <summary>
        /// Загружает настройки с предыдущей сессии, если настройки ранее сохранялись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Метод вызывает диалоговое окно с выбором начальной директории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;

                this.starterDirTextBox.Text = folderName;
            }
        }
    }
}
