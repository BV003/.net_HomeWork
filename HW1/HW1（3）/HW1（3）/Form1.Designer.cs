namespace HW1_3_
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            button1 = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            fileSystemWatcher1 = new FileSystemWatcher();
            label8 = new Label();
            button2 = new Button();
            label9 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(411, 158);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 0;
            label1.Text = "问题";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(593, 158);
            label2.Name = "label2";
            label2.Size = new Size(0, 24);
            label2.TabIndex = 1;
            label2.Click += label2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(576, 298);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 30);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(411, 298);
            label3.Name = "label3";
            label3.Size = new Size(136, 24);
            label3.TabIndex = 3;
            label3.Text = "请输入你的答案";
            label3.Click += label3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(726, 679);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 4;
            button1.Text = "下一题";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(411, 441);
            label4.Name = "label4";
            label4.Size = new Size(82, 24);
            label4.TabIndex = 5;
            label4.Text = "当前得分";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(593, 441);
            label5.Name = "label5";
            label5.Size = new Size(0, 24);
            label5.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(411, 573);
            label6.Name = "label6";
            label6.Size = new Size(82, 24);
            label6.TabIndex = 7;
            label6.Text = "剩余时间";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(593, 573);
            label7.Name = "label7";
            label7.Size = new Size(32, 24);
            label7.TabIndex = 8;
            label7.Text = "10";
            label7.Click += label7_Click;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(858, 301);
            label8.Name = "label8";
            label8.Size = new Size(64, 24);
            label8.TabIndex = 9;
            label8.Text = "未回答";
            // 
            // button2
            // 
            button2.Location = new Point(474, 679);
            button2.Name = "button2";
            button2.Size = new Size(107, 34);
            button2.TabIndex = 10;
            button2.Text = "提交";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(675, 441);
            label9.Name = "label9";
            label9.Size = new Size(51, 24);
            label9.TabIndex = 11;
            label9.Text = "/100";
            label9.Click += label9_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // timer2
            // 
            timer2.Tick += timer2_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1792, 858);
            Controls.Add(label9);
            Controls.Add(button2);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private Button button1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label8;
        private Button button2;
        private Label label9;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}
