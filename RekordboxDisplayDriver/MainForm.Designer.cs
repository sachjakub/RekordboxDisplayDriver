namespace RekordboxDisplayDriver
{
    partial class MainForm
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
            mainLayout = new TableLayoutPanel();
            button1 = new Button();
            label1 = new Label();
            deck1picturebox = new PictureBox();
            deck2picturebox = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            previewButton = new Button();
            label3 = new Label();
            fpsCombobox = new ComboBox();
            timer = new System.Windows.Forms.Timer(components);
            preview = new System.Windows.Forms.Timer(components);
            label4 = new Label();
            comboBox1 = new ComboBox();
            label5 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            label6 = new Label();
            mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)deck1picturebox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deck2picturebox).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(button1, 0, 3);
            mainLayout.Controls.Add(label1, 0, 4);
            mainLayout.Controls.Add(deck1picturebox, 0, 0);
            mainLayout.Controls.Add(deck2picturebox, 0, 1);
            mainLayout.Controls.Add(tableLayoutPanel1, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 5;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            mainLayout.Size = new Size(913, 329);
            mainLayout.TabIndex = 0;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(3, 272);
            button1.Name = "button1";
            button1.Size = new Size(907, 34);
            button1.TabIndex = 0;
            button1.Text = "C O N N E C T   A N D   T R A N S M I T";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 309);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 1;
            label1.Text = "Status: ";
            // 
            // deck1picturebox
            // 
            deck1picturebox.BackColor = Color.White;
            deck1picturebox.Dock = DockStyle.Fill;
            deck1picturebox.Location = new Point(3, 3);
            deck1picturebox.Name = "deck1picturebox";
            deck1picturebox.Size = new Size(907, 64);
            deck1picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            deck1picturebox.TabIndex = 2;
            deck1picturebox.TabStop = false;
            // 
            // deck2picturebox
            // 
            deck2picturebox.BackColor = Color.White;
            deck2picturebox.Dock = DockStyle.Fill;
            deck2picturebox.Location = new Point(3, 73);
            deck2picturebox.Name = "deck2picturebox";
            deck2picturebox.Size = new Size(907, 64);
            deck2picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            deck2picturebox.TabIndex = 3;
            deck2picturebox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(previewButton, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(fpsCombobox, 0, 3);
            tableLayoutPanel1.Controls.Add(label4, 1, 0);
            tableLayoutPanel1.Controls.Add(comboBox1, 1, 1);
            tableLayoutPanel1.Controls.Add(label5, 1, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 3);
            tableLayoutPanel1.Controls.Add(label6, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 143);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(907, 123);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 0;
            label2.Text = "Preview:";
            // 
            // previewButton
            // 
            previewButton.Dock = DockStyle.Fill;
            previewButton.Location = new Point(3, 23);
            previewButton.Name = "previewButton";
            previewButton.Size = new Size(296, 34);
            previewButton.TabIndex = 1;
            previewButton.Text = "Turn on";
            previewButton.UseVisualStyleBackColor = true;
            previewButton.Click += previewButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 60);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 2;
            label3.Text = "FPS:";
            label3.Click += label3_Click;
            // 
            // fpsCombobox
            // 
            fpsCombobox.Dock = DockStyle.Fill;
            fpsCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            fpsCombobox.FormattingEnabled = true;
            fpsCombobox.Items.AddRange(new object[] { "1", "5", "15", "30", "60" });
            fpsCombobox.Location = new Point(3, 83);
            fpsCombobox.MaxDropDownItems = 6;
            fpsCombobox.Name = "fpsCombobox";
            fpsCombobox.Size = new Size(296, 23);
            fpsCombobox.TabIndex = 3;
            fpsCombobox.SelectedIndexChanged += fpsCombobox_SelectedIndexChanged;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // preview
            // 
            preview.Interval = 50;
            preview.Tick += preview_Tick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(305, 0);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 4;
            label4.Text = "Boundaries:";
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(305, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(296, 23);
            comboBox1.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(305, 60);
            label5.Name = "label5";
            label5.Size = new Size(112, 15);
            label5.TabIndex = 6;
            label5.Text = "Display ( left - right)";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(comboBox2, 0, 0);
            tableLayoutPanel2.Controls.Add(comboBox3, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(305, 83);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(296, 34);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // comboBox2
            // 
            comboBox2.Dock = DockStyle.Fill;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(3, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(142, 23);
            comboBox2.TabIndex = 0;
            // 
            // comboBox3
            // 
            comboBox3.Dock = DockStyle.Fill;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(151, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(142, 23);
            comboBox3.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(607, 0);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 8;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(913, 329);
            Controls.Add(mainLayout);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RekordboxDisplayDriver";
            Load += MainForm_Load;
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)deck1picturebox).EndInit();
            ((System.ComponentModel.ISupportInitialize)deck2picturebox).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Button button1;
        private Label label1;
        private PictureBox deck1picturebox;
        private PictureBox deck2picturebox;
        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer preview;
        private Label label2;
        private Button previewButton;
        private Label label3;
        private ComboBox fpsCombobox;
        private Label label4;
        private ComboBox comboBox1;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel2;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Label label6;
    }
}
