﻿namespace CSBooster.Language.ExportImport
{
    partial class main
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxDefaultText = new System.Windows.Forms.CheckBox();
            this.cbxMultiLang = new System.Windows.Forms.CheckBox();
            this.cbxMultiFile = new System.Windows.Forms.CheckBox();
            this.lbxLanguage = new System.Windows.Forms.ListBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnGo = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pfad";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(382, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "..";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDefaultText);
            this.groupBox1.Controls.Add(this.cbxMultiLang);
            this.groupBox1.Controls.Add(this.cbxMultiFile);
            this.groupBox1.Controls.Add(this.lbxLanguage);
            this.groupBox1.Location = new System.Drawing.Point(15, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 220);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export einstellungen";
            // 
            // cbxDefaultText
            // 
            this.cbxDefaultText.AutoSize = true;
            this.cbxDefaultText.Location = new System.Drawing.Point(260, 46);
            this.cbxDefaultText.Name = "cbxDefaultText";
            this.cbxDefaultText.Size = new System.Drawing.Size(144, 17);
            this.cbxDefaultText.TabIndex = 13;
            this.cbxDefaultText.Text = "default Text für sprachen";
            this.cbxDefaultText.UseVisualStyleBackColor = true;
            // 
            // cbxMultiLang
            // 
            this.cbxMultiLang.AutoSize = true;
            this.cbxMultiLang.Location = new System.Drawing.Point(260, 92);
            this.cbxMultiLang.Name = "cbxMultiLang";
            this.cbxMultiLang.Size = new System.Drawing.Size(177, 17);
            this.cbxMultiLang.TabIndex = 12;
            this.cbxMultiLang.Text = "pro sprache file ein csv erstellen";
            this.cbxMultiLang.UseVisualStyleBackColor = true;
            this.cbxMultiLang.Visible = false;
            // 
            // cbxMultiFile
            // 
            this.cbxMultiFile.AutoSize = true;
            this.cbxMultiFile.Location = new System.Drawing.Point(260, 69);
            this.cbxMultiFile.Name = "cbxMultiFile";
            this.cbxMultiFile.Size = new System.Drawing.Size(154, 17);
            this.cbxMultiFile.TabIndex = 11;
            this.cbxMultiFile.Text = "pro xml file ein csv erstellen";
            this.cbxMultiFile.UseVisualStyleBackColor = true;
            this.cbxMultiFile.Visible = false;
            // 
            // lbxLanguage
            // 
            this.lbxLanguage.FormattingEnabled = true;
            this.lbxLanguage.Location = new System.Drawing.Point(15, 46);
            this.lbxLanguage.Name = "lbxLanguage";
            this.lbxLanguage.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxLanguage.Size = new System.Drawing.Size(228, 160);
            this.lbxLanguage.TabIndex = 10;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(15, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(55, 17);
            this.radioButton2.TabIndex = 11;
            this.radioButton2.Text = "Export";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(76, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.Text = "Import";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(383, 291);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 12;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.Personal;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(136, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(129, 17);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.Text = "Import (Merge in XML)";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 320);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "main";
            this.Text = "Sieme Language management";
            this.Load += new System.EventHandler(this.main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxMultiLang;
        private System.Windows.Forms.CheckBox cbxMultiFile;
        private System.Windows.Forms.ListBox lbxLanguage;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox cbxDefaultText;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}

