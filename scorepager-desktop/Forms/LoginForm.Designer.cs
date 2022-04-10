
namespace scorepager_desktop.Forms {
	partial class LoginForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.emailTextBox = new System.Windows.Forms.TextBox();
			this.passWordMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.loginButton = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// emailTextBox
			// 
			this.emailTextBox.Location = new System.Drawing.Point(12, 12);
			this.emailTextBox.Name = "emailTextBox";
			this.emailTextBox.Size = new System.Drawing.Size(200, 20);
			this.emailTextBox.TabIndex = 0;
			// 
			// passWordMaskedTextBox
			// 
			this.passWordMaskedTextBox.Location = new System.Drawing.Point(12, 38);
			this.passWordMaskedTextBox.Name = "passWordMaskedTextBox";
			this.passWordMaskedTextBox.Size = new System.Drawing.Size(200, 20);
			this.passWordMaskedTextBox.TabIndex = 1;
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(12, 64);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(200, 23);
			this.loginButton.TabIndex = 2;
			this.loginButton.Text = "Login";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(101, 106);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(111, 13);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Register new account";
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(230, 130);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.loginButton);
			this.Controls.Add(this.passWordMaskedTextBox);
			this.Controls.Add(this.emailTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LoginForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox emailTextBox;
		private System.Windows.Forms.MaskedTextBox passWordMaskedTextBox;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}