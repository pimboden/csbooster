namespace _4screen.CSB.Monitor
{
  partial class LoginForm
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
        this.usernameLabel = new DevExpress.XtraEditors.LabelControl();
        this.passwordLabel = new DevExpress.XtraEditors.LabelControl();
        this.usernameTextBox = new DevExpress.XtraEditors.TextEdit();
        this.passwordTextBox = new DevExpress.XtraEditors.TextEdit();
        this.loginButton = new DevExpress.XtraEditors.SimpleButton();
        ((System.ComponentModel.ISupportInitialize)(this.usernameTextBox.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.passwordTextBox.Properties)).BeginInit();
        this.SuspendLayout();
        // 
        // usernameLabel
        // 
        this.usernameLabel.Location = new System.Drawing.Point(12, 12);
        this.usernameLabel.Name = "usernameLabel";
        this.usernameLabel.Size = new System.Drawing.Size(52, 13);
        this.usernameLabel.TabIndex = 0;
        this.usernameLabel.Text = "Username:";
        // 
        // passwordLabel
        // 
        this.passwordLabel.Location = new System.Drawing.Point(12, 38);
        this.passwordLabel.Name = "passwordLabel";
        this.passwordLabel.Size = new System.Drawing.Size(48, 13);
        this.passwordLabel.TabIndex = 1;
        this.passwordLabel.Text = "Passwort:";
        // 
        // usernameTextBox
        // 
        this.usernameTextBox.EditValue = "";
        this.usernameTextBox.Location = new System.Drawing.Point(93, 9);
        this.usernameTextBox.Name = "usernameTextBox";
        this.usernameTextBox.Size = new System.Drawing.Size(120, 20);
        this.usernameTextBox.TabIndex = 3;
        this.usernameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnLoginKeyPress);
        // 
        // passwordTextBox
        // 
        this.passwordTextBox.EditValue = "";
        this.passwordTextBox.Location = new System.Drawing.Point(94, 35);
        this.passwordTextBox.Name = "passwordTextBox";
        this.passwordTextBox.Properties.PasswordChar = '*';
        this.passwordTextBox.Size = new System.Drawing.Size(120, 20);
        this.passwordTextBox.TabIndex = 4;
        this.passwordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnLoginKeyPress);
        // 
        // loginButton
        // 
        this.loginButton.Location = new System.Drawing.Point(139, 70);
        this.loginButton.Name = "loginButton";
        this.loginButton.Size = new System.Drawing.Size(75, 23);
        this.loginButton.TabIndex = 7;
        this.loginButton.Text = "OK";
        this.loginButton.Click += new System.EventHandler(this.OnLoginButtonClick);
        // 
        // LoginForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(226, 105);
        this.Controls.Add(this.passwordLabel);
        this.Controls.Add(this.usernameTextBox);
        this.Controls.Add(this.passwordTextBox);
        this.Controls.Add(this.loginButton);
        this.Controls.Add(this.usernameLabel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LoginForm";
        this.Text = "Anmeldung";
        this.TopMost = true;
        ((System.ComponentModel.ISupportInitialize)(this.usernameTextBox.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.passwordTextBox.Properties)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.LabelControl usernameLabel;
    private DevExpress.XtraEditors.LabelControl passwordLabel;
    private DevExpress.XtraEditors.TextEdit usernameTextBox;
    private DevExpress.XtraEditors.TextEdit passwordTextBox;
    private DevExpress.XtraEditors.SimpleButton loginButton;
  }
}