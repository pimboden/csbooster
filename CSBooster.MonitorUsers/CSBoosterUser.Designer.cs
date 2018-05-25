namespace CSBooster.MonitorUsers
{
	partial class CSBoosterUser
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
			this.lblUserName = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.lblRole = new System.Windows.Forms.Label();
			this.lblPWD = new System.Windows.Forms.Label();
			this.txtPWD = new System.Windows.Forms.TextBox();
			this.cbxRole1 = new System.Windows.Forms.CheckBox();
			this.cbxRole2 = new System.Windows.Forms.CheckBox();
			this.lblMsg = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblUserName
			// 
			this.lblUserName.AutoSize = true;
			this.lblUserName.Location = new System.Drawing.Point(13, 13);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(75, 13);
			this.lblUserName.TabIndex = 0;
			this.lblUserName.Text = "Benutzername";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(106, 10);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(275, 20);
			this.txtUserName.TabIndex = 1;
			// 
			// lblRole
			// 
			this.lblRole.AutoSize = true;
			this.lblRole.Location = new System.Drawing.Point(16, 87);
			this.lblRole.Name = "lblRole";
			this.lblRole.Size = new System.Drawing.Size(73, 13);
			this.lblRole.TabIndex = 2;
			this.lblRole.Text = "Monitor Rolen";
			// 
			// lblPWD
			// 
			this.lblPWD.AutoSize = true;
			this.lblPWD.Location = new System.Drawing.Point(16, 49);
			this.lblPWD.Name = "lblPWD";
			this.lblPWD.Size = new System.Drawing.Size(84, 13);
			this.lblPWD.TabIndex = 3;
			this.lblPWD.Text = "Neues Passwort";
			// 
			// txtPWD
			// 
			this.txtPWD.Location = new System.Drawing.Point(106, 46);
			this.txtPWD.Name = "txtPWD";
			this.txtPWD.Size = new System.Drawing.Size(126, 20);
			this.txtPWD.TabIndex = 4;
			// 
			// cbxRole1
			// 
			this.cbxRole1.AutoSize = true;
			this.cbxRole1.Location = new System.Drawing.Point(106, 86);
			this.cbxRole1.Name = "cbxRole1";
			this.cbxRole1.Size = new System.Drawing.Size(100, 17);
			this.cbxRole1.TabIndex = 6;
			this.cbxRole1.Text = "StatisticsViewer";
			this.cbxRole1.UseVisualStyleBackColor = true;
			// 
			// cbxRole2
			// 
			this.cbxRole2.AutoSize = true;
			this.cbxRole2.Location = new System.Drawing.Point(106, 109);
			this.cbxRole2.Name = "cbxRole2";
			this.cbxRole2.Size = new System.Drawing.Size(90, 17);
			this.cbxRole2.TabIndex = 7;
			this.cbxRole2.Text = "UserManager";
			this.cbxRole2.UseVisualStyleBackColor = true;
			// 
			// lblMsg
			// 
			this.lblMsg.AutoSize = true;
			this.lblMsg.ForeColor = System.Drawing.Color.Red;
			this.lblMsg.Location = new System.Drawing.Point(103, 147);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(37, 13);
			this.lblMsg.TabIndex = 8;
			this.lblMsg.Text = "lblMsg";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(238, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(143, 44);
			this.label1.TabIndex = 9;
			this.label1.Text = "Leer lassen falls das Passwort nicht geändert werden soll";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(106, 174);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 10;
			this.btnOk.Text = "ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// CSBoosterUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 243);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblMsg);
			this.Controls.Add(this.cbxRole2);
			this.Controls.Add(this.cbxRole1);
			this.Controls.Add(this.txtPWD);
			this.Controls.Add(this.lblPWD);
			this.Controls.Add(this.lblRole);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.lblUserName);
			this.Name = "CSBoosterUser";
			this.Text = "CSBoosterUser";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label lblRole;
		private System.Windows.Forms.Label lblPWD;
		private System.Windows.Forms.TextBox txtPWD;
		private System.Windows.Forms.CheckBox cbxRole1;
		private System.Windows.Forms.CheckBox cbxRole2;
		private System.Windows.Forms.Label lblMsg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOk;
	}
}