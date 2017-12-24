namespace DIploma_repair.Admin
{
    partial class AdminRoom
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.діїToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.додатиПристрійToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.замовленіДеталіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.додаванняДеталейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вийтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зОбліковогоЗаписуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зПрограмиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.діїToolStripMenuItem,
            this.вийтиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // діїToolStripMenuItem
            // 
            this.діїToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.додатиПристрійToolStripMenuItem,
            this.замовленіДеталіToolStripMenuItem,
            this.додаванняДеталейToolStripMenuItem});
            this.діїToolStripMenuItem.Name = "діїToolStripMenuItem";
            this.діїToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.діїToolStripMenuItem.Text = "Дії";
            // 
            // додатиПристрійToolStripMenuItem
            // 
            this.додатиПристрійToolStripMenuItem.Name = "додатиПристрійToolStripMenuItem";
            this.додатиПристрійToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.додатиПристрійToolStripMenuItem.Text = "Додати пристрій";
            this.додатиПристрійToolStripMenuItem.Click += new System.EventHandler(this.AddDevice);
            // 
            // замовленіДеталіToolStripMenuItem
            // 
            this.замовленіДеталіToolStripMenuItem.Name = "замовленіДеталіToolStripMenuItem";
            this.замовленіДеталіToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.замовленіДеталіToolStripMenuItem.Text = "Замовлення деталей";
            this.замовленіДеталіToolStripMenuItem.Click += new System.EventHandler(this.DetailList);
            // 
            // додаванняДеталейToolStripMenuItem
            // 
            this.додаванняДеталейToolStripMenuItem.Name = "додаванняДеталейToolStripMenuItem";
            this.додаванняДеталейToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.додаванняДеталейToolStripMenuItem.Text = "Додавання нових деталей";
            this.додаванняДеталейToolStripMenuItem.Click += new System.EventHandler(this.AddDetail);
            // 
            // вийтиToolStripMenuItem
            // 
            this.вийтиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.зОбліковогоЗаписуToolStripMenuItem,
            this.зПрограмиToolStripMenuItem});
            this.вийтиToolStripMenuItem.Name = "вийтиToolStripMenuItem";
            this.вийтиToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.вийтиToolStripMenuItem.Text = "Вихід";
            // 
            // зОбліковогоЗаписуToolStripMenuItem
            // 
            this.зОбліковогоЗаписуToolStripMenuItem.Name = "зОбліковогоЗаписуToolStripMenuItem";
            this.зОбліковогоЗаписуToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.зОбліковогоЗаписуToolStripMenuItem.Text = "З облікового запису";
            this.зОбліковогоЗаписуToolStripMenuItem.Click += new System.EventHandler(this.Logout);
            // 
            // зПрограмиToolStripMenuItem
            // 
            this.зПрограмиToolStripMenuItem.Name = "зПрограмиToolStripMenuItem";
            this.зПрограмиToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.зПрограмиToolStripMenuItem.Text = "З програми";
            this.зПрограмиToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 125);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Інформація користувача";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 125);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Інформація користувача";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "#";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "#";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "#";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "#";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Location = new System.Drawing.Point(286, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(330, 119);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Список замовлень";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(318, 95);
            this.listBox1.TabIndex = 0;
            // 
            // AdminRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 159);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AdminRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Кімната адміністратора";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminRoom_FormClosing);
            this.Load += new System.EventHandler(this.AdminRoom_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem діїToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem додатиПристрійToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem замовленіДеталіToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem додаванняДеталейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вийтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зОбліковогоЗаписуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зПрограмиToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox1;
    }
}