namespace Diplom_Work_try1
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadbutton = new System.Windows.Forms.Button();
            this.CountOfShots = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.clearbutton = new System.Windows.Forms.Button();
            this.notecentbutton = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfShots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // loadbutton
            // 
            this.loadbutton.Location = new System.Drawing.Point(348, 425);
            this.loadbutton.Name = "loadbutton";
            this.loadbutton.Size = new System.Drawing.Size(128, 23);
            this.loadbutton.TabIndex = 1;
            this.loadbutton.Text = "Загрузить";
            this.loadbutton.UseVisualStyleBackColor = true;
            this.loadbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // CountOfShots
            // 
            this.CountOfShots.Location = new System.Drawing.Point(278, 428);
            this.CountOfShots.Name = "CountOfShots";
            this.CountOfShots.Size = new System.Drawing.Size(37, 20);
            this.CountOfShots.TabIndex = 4;
            this.CountOfShots.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CountOfShots.ValueChanged += new System.EventHandler(this.CountOfShots_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Число снимков :";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 403);
            this.panel1.TabIndex = 6;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(535, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // clearbutton
            // 
            this.clearbutton.Location = new System.Drawing.Point(183, 465);
            this.clearbutton.Name = "clearbutton";
            this.clearbutton.Size = new System.Drawing.Size(128, 23);
            this.clearbutton.TabIndex = 8;
            this.clearbutton.Text = "Очистить";
            this.clearbutton.UseVisualStyleBackColor = true;
            this.clearbutton.Click += new System.EventHandler(this.button2_Click);
            // 
            // notecentbutton
            // 
            this.notecentbutton.Location = new System.Drawing.Point(12, 465);
            this.notecentbutton.Name = "notecentbutton";
            this.notecentbutton.Size = new System.Drawing.Size(152, 23);
            this.notecentbutton.TabIndex = 9;
            this.notecentbutton.Text = "Отметить центр глаза";
            this.notecentbutton.UseVisualStyleBackColor = true;
            this.notecentbutton.Click += new System.EventHandler(this.button3_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(348, 465);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(128, 23);
            this.save_button.TabIndex = 10;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(70, 428);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Шаг(мм) :";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 524);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.notecentbutton);
            this.Controls.Add(this.clearbutton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CountOfShots);
            this.Controls.Add(this.loadbutton);
            this.Name = "Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CountOfShots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadbutton;
        private System.Windows.Forms.NumericUpDown CountOfShots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button clearbutton;
        private System.Windows.Forms.Button notecentbutton;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
    }
}

