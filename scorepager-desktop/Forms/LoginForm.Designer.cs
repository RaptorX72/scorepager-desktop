
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
			this.linkLabelRegister = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// emailTextBox
			// 
			this.emailTextBox.Location = new System.Drawing.Point(12, 121);
			this.emailTextBox.Name = "emailTextBox";
			this.emailTextBox.Size = new System.Drawing.Size(200, 20);
			this.emailTextBox.TabIndex = 0;
			// 
			// passWordMaskedTextBox
			// 
			this.passWordMaskedTextBox.Location = new System.Drawing.Point(12, 163);
			this.passWordMaskedTextBox.Name = "passWordMaskedTextBox";
			this.passWordMaskedTextBox.PasswordChar = '*';
			this.passWordMaskedTextBox.Size = new System.Drawing.Size(200, 20);
			this.passWordMaskedTextBox.TabIndex = 1;
			this.passWordMaskedTextBox.UseSystemPasswordChar = true;
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(12, 191);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(200, 35);
			this.loginButton.TabIndex = 2;
			this.loginButton.Text = "Login";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// linkLabelRegister
			// 
			this.linkLabelRegister.AutoSize = true;
			this.linkLabelRegister.Location = new System.Drawing.Point(101, 232);
			this.linkLabelRegister.Name = "linkLabelRegister";
			this.linkLabelRegister.Size = new System.Drawing.Size(111, 13);
			this.linkLabelRegister.TabIndex = 3;
			this.linkLabelRegister.TabStop = true;
			this.linkLabelRegister.Text = "Register new account";
			this.linkLabelRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRegister_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 105);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "e-mail address";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 147);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Password";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(79, 20);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(224, 261);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabelRegister);
			this.Controls.Add(this.loginButton);
			this.Controls.Add(this.passWordMaskedTextBox);
			this.Controls.Add(this.emailTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LoginForm";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox emailTextBox;
		private System.Windows.Forms.MaskedTextBox passWordMaskedTextBox;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.LinkLabel linkLabelRegister;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}