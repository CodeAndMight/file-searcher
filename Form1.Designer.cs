namespace FileSearcher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.starterDirTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filePatternTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searcherTextBox = new System.Windows.Forms.TextBox();
            this.actionButton = new System.Windows.Forms.Button();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.currentFileLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.fileAmountLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.remainTimeLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Стартовая директория:";
            // 
            // starterDirTextBox
            // 
            this.starterDirTextBox.Location = new System.Drawing.Point(16, 30);
            this.starterDirTextBox.Name = "starterDirTextBox";
            this.starterDirTextBox.Size = new System.Drawing.Size(225, 20);
            this.starterDirTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Шаблон имени файла:";
            // 
            // filePatternTextBox
            // 
            this.filePatternTextBox.Location = new System.Drawing.Point(247, 30);
            this.filePatternTextBox.Name = "filePatternTextBox";
            this.filePatternTextBox.Size = new System.Drawing.Size(116, 20);
            this.filePatternTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Текст:";
            // 
            // searcherTextBox
            // 
            this.searcherTextBox.Location = new System.Drawing.Point(372, 30);
            this.searcherTextBox.Name = "searcherTextBox";
            this.searcherTextBox.Size = new System.Drawing.Size(159, 20);
            this.searcherTextBox.TabIndex = 5;
            // 
            // actionButton
            // 
            this.actionButton.Location = new System.Drawing.Point(537, 28);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(75, 23);
            this.actionButton.TabIndex = 6;
            this.actionButton.Text = "Найти";
            this.actionButton.UseVisualStyleBackColor = true;
            // 
            // fileTreeView
            // 
            this.fileTreeView.Location = new System.Drawing.Point(17, 56);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.Size = new System.Drawing.Size(595, 280);
            this.fileTreeView.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.remainTimeLabel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.fileAmountLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.currentFileLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(17, 342);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 88);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Текущий файл:";
            // 
            // currentFileLabel
            // 
            this.currentFileLabel.AutoSize = true;
            this.currentFileLabel.Location = new System.Drawing.Point(98, 20);
            this.currentFileLabel.Name = "currentFileLabel";
            this.currentFileLabel.Size = new System.Drawing.Size(27, 13);
            this.currentFileLabel.TabIndex = 1;
            this.currentFileLabel.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Количество обработанных файлов:";
            // 
            // fileAmountLabel
            // 
            this.fileAmountLabel.AutoSize = true;
            this.fileAmountLabel.Location = new System.Drawing.Point(198, 42);
            this.fileAmountLabel.Name = "fileAmountLabel";
            this.fileAmountLabel.Size = new System.Drawing.Size(27, 13);
            this.fileAmountLabel.TabIndex = 3;
            this.fileAmountLabel.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Прошло времени:";
            // 
            // remainTimeLabel
            // 
            this.remainTimeLabel.AutoSize = true;
            this.remainTimeLabel.Location = new System.Drawing.Point(110, 63);
            this.remainTimeLabel.Name = "remainTimeLabel";
            this.remainTimeLabel.Size = new System.Drawing.Size(27, 13);
            this.remainTimeLabel.TabIndex = 5;
            this.remainTimeLabel.Text = "N/A";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fileTreeView);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.searcherTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.filePatternTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.starterDirTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.Text = "File Searcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox starterDirTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox filePatternTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searcherTextBox;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label remainTimeLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label fileAmountLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label currentFileLabel;
        private System.Windows.Forms.Label label4;
    }
}

