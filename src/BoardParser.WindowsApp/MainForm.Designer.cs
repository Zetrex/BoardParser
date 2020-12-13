namespace BoardParser.WindowsApp
{
    partial class MainForm
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
            this.siteRadioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.siteRadioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pageTextBox = new System.Windows.Forms.TextBox();
            this.customPageCheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.logsTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // siteRadioButton1
            // 
            this.siteRadioButton1.AutoSize = true;
            this.siteRadioButton1.Checked = true;
            this.siteRadioButton1.Location = new System.Drawing.Point(6, 22);
            this.siteRadioButton1.Name = "siteRadioButton1";
            this.siteRadioButton1.Size = new System.Drawing.Size(178, 19);
            this.siteRadioButton1.TabIndex = 0;
            this.siteRadioButton1.TabStop = true;
            this.siteRadioButton1.Text = "https://www.rupostings.com";
            this.siteRadioButton1.UseVisualStyleBackColor = true;
            this.siteRadioButton1.CheckedChanged += new System.EventHandler(this.siteRadioButton1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.siteRadioButton2);
            this.groupBox1.Controls.Add(this.siteRadioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 77);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site";
            // 
            // siteRadioButton2
            // 
            this.siteRadioButton2.AutoSize = true;
            this.siteRadioButton2.Enabled = false;
            this.siteRadioButton2.Location = new System.Drawing.Point(6, 47);
            this.siteRadioButton2.Name = "siteRadioButton2";
            this.siteRadioButton2.Size = new System.Drawing.Size(124, 19);
            this.siteRadioButton2.TabIndex = 1;
            this.siteRadioButton2.Text = "https://rusrek.com";
            this.siteRadioButton2.UseVisualStyleBackColor = true;
            this.siteRadioButton2.CheckedChanged += new System.EventHandler(this.siteRadioButton2_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pageTextBox);
            this.groupBox2.Controls.Add(this.customPageCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 80);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Page";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Url:";
            // 
            // pageTextBox
            // 
            this.pageTextBox.Enabled = false;
            this.pageTextBox.Location = new System.Drawing.Point(39, 47);
            this.pageTextBox.Name = "pageTextBox";
            this.pageTextBox.Size = new System.Drawing.Size(355, 23);
            this.pageTextBox.TabIndex = 1;
            this.pageTextBox.TextChanged += new System.EventHandler(this.pageTextBox_TextChanged);
            this.pageTextBox.Leave += new System.EventHandler(this.pageTextBox_Leave);
            // 
            // customPageCheckBox
            // 
            this.customPageCheckBox.AutoSize = true;
            this.customPageCheckBox.Location = new System.Drawing.Point(8, 22);
            this.customPageCheckBox.Name = "customPageCheckBox";
            this.customPageCheckBox.Size = new System.Drawing.Size(97, 19);
            this.customPageCheckBox.TabIndex = 0;
            this.customPageCheckBox.Text = "Custom page";
            this.customPageCheckBox.UseVisualStyleBackColor = true;
            this.customPageCheckBox.CheckedChanged += new System.EventHandler(this.customPageCheckBox_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 253);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 23);
            this.progressBar.TabIndex = 3;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(259, 282);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(153, 35);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.filePathTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 181);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 66);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "File path:";
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Enabled = false;
            this.filePathTextBox.Location = new System.Drawing.Point(67, 25);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(327, 23);
            this.filePathTextBox.TabIndex = 1;
            this.filePathTextBox.TextChanged += new System.EventHandler(this.filePathTextBox_TextChanged);
            this.filePathTextBox.Leave += new System.EventHandler(this.filePathTextBox_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.logsTxtBox);
            this.groupBox4.Location = new System.Drawing.Point(418, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(298, 305);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Logs";
            // 
            // logsTxtBox
            // 
            this.logsTxtBox.Enabled = false;
            this.logsTxtBox.Location = new System.Drawing.Point(7, 22);
            this.logsTxtBox.Multiline = true;
            this.logsTxtBox.Name = "logsTxtBox";
            this.logsTxtBox.Size = new System.Drawing.Size(285, 277);
            this.logsTxtBox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 328);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Board Parser";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton siteRadioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton siteRadioButton2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pageTextBox;
        private System.Windows.Forms.CheckBox customPageCheckBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox logsTxtBox;
    }
}