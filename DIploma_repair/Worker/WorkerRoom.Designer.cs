namespace DIploma_repair.Worker
{
    partial class WorkerRoom
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.діїToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.провестиЗаняттяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новеЗамовленняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вийтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зобліковогоЗаписуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зПрограмиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 74);
            this.button1.TabIndex = 14;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(0, 216);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 78);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Всього замовлень";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.діїToolStripMenuItem,
            this.вийтиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(951, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // діїToolStripMenuItem
            // 
            this.діїToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.провестиЗаняттяToolStripMenuItem,
            this.новеЗамовленняToolStripMenuItem});
            this.діїToolStripMenuItem.Name = "діїToolStripMenuItem";
            this.діїToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.діїToolStripMenuItem.Text = "Дії";
            // 
            // провестиЗаняттяToolStripMenuItem
            // 
            this.провестиЗаняттяToolStripMenuItem.Name = "провестиЗаняттяToolStripMenuItem";
            this.провестиЗаняттяToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.провестиЗаняттяToolStripMenuItem.Text = "Список замовлень";
            this.провестиЗаняттяToolStripMenuItem.Click += new System.EventHandler(this.OrderList);
            // 
            // новеЗамовленняToolStripMenuItem
            // 
            this.новеЗамовленняToolStripMenuItem.Name = "новеЗамовленняToolStripMenuItem";
            this.новеЗамовленняToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.новеЗамовленняToolStripMenuItem.Text = "Замовлення деталей";
            this.новеЗамовленняToolStripMenuItem.Click += new System.EventHandler(this.OrderDetail);
            // 
            // вийтиToolStripMenuItem
            // 
            this.вийтиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.зобліковогоЗаписуToolStripMenuItem,
            this.зПрограмиToolStripMenuItem});
            this.вийтиToolStripMenuItem.Name = "вийтиToolStripMenuItem";
            this.вийтиToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.вийтиToolStripMenuItem.Text = "Вийти";
            // 
            // зобліковогоЗаписуToolStripMenuItem
            // 
            this.зобліковогоЗаписуToolStripMenuItem.Name = "зобліковогоЗаписуToolStripMenuItem";
            this.зобліковогоЗаписуToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.зобліковогоЗаписуToolStripMenuItem.Text = "Зоблікового запису";
            this.зобліковогоЗаписуToolStripMenuItem.Click += new System.EventHandler(this.Logout);
            // 
            // зПрограмиToolStripMenuItem
            // 
            this.зПрограмиToolStripMenuItem.Name = "зПрограмиToolStripMenuItem";
            this.зПрограмиToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.зПрограмиToolStripMenuItem.Text = "З програми";
            this.зПрограмиToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(657, 242);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_ColumnHeaderMouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(270, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(677, 267);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Замовлення";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 183);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Інформація користувача";
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
            // WorkerRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 299);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WorkerRoom";
            this.Text = "WorkerRoom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkerRoom_FormClosing);
            this.Load += new System.EventHandler(this.WorkerRoom_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem діїToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem провестиЗаняттяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новеЗамовленняToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вийтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зобліковогоЗаписуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зПрограмиToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
    }
}