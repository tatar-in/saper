namespace Saper
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.countButtonX = new System.Windows.Forms.NumericUpDown();
            this.countButtonY = new System.Windows.Forms.NumericUpDown();
            this.countMine = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.timerOfGame = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.countButtonX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countButtonY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countMine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerOfGame)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // countButtonX
            // 
            this.countButtonX.Location = new System.Drawing.Point(161, 8);
            this.countButtonX.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.countButtonX.Name = "countButtonX";
            this.countButtonX.Size = new System.Drawing.Size(68, 22);
            this.countButtonX.TabIndex = 1;
            this.countButtonX.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // countButtonY
            // 
            this.countButtonY.Location = new System.Drawing.Point(161, 36);
            this.countButtonY.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.countButtonY.Name = "countButtonY";
            this.countButtonY.Size = new System.Drawing.Size(68, 22);
            this.countButtonY.TabIndex = 2;
            this.countButtonY.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // countMine
            // 
            this.countMine.Location = new System.Drawing.Point(161, 64);
            this.countMine.Name = "countMine";
            this.countMine.Size = new System.Drawing.Size(68, 22);
            this.countMine.TabIndex = 3;
            this.countMine.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Кол-во клеток по Х";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Кол-во клеток по У";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Кол-во мин в игре";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(257, 33);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(82, 33);
            this.buttonStartGame.TabIndex = 0;
            this.buttonStartGame.Tag = "";
            this.buttonStartGame.Text = "Старт";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Время, сек";
            // 
            // timerOfGame
            // 
            this.timerOfGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timerOfGame.Location = new System.Drawing.Point(373, 32);
            this.timerOfGame.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.timerOfGame.Name = "timerOfGame";
            this.timerOfGame.Size = new System.Drawing.Size(85, 41);
            this.timerOfGame.TabIndex = 4;
            this.timerOfGame.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timerOfGame.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(500, 100);
            this.Controls.Add(this.timerOfGame);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.countMine);
            this.Controls.Add(this.countButtonY);
            this.Controls.Add(this.countButtonX);
            this.Name = "Form1";
            this.Text = "Сапер";
            ((System.ComponentModel.ISupportInitialize)(this.countButtonX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countButtonY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countMine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerOfGame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown countButtonX;
        private System.Windows.Forms.NumericUpDown countButtonY;
        private System.Windows.Forms.NumericUpDown countMine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown timerOfGame;
    }
}

