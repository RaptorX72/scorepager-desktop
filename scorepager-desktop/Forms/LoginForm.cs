using System;
using System.Windows.Forms;
using scorepager_desktop.Classes;

namespace scorepager_desktop.Forms {
	public partial class LoginForm : Form {
		private FirebaseClient client;

		public LoginForm() {
			InitializeComponent();
			client = FirebaseClient.GetInstance();
			loginButton.Enabled = false;
			emailTextBox.TextChanged += TextBoxTextChanged;
			passWordMaskedTextBox.TextChanged += TextBoxTextChanged;
		}

		private void loginButton_Click(object sender, EventArgs e) {
			if (!client.Login(emailTextBox.Text, passWordMaskedTextBox.Text)) {
				MessageBox.Show("Incorrect user credentials!");
				passWordMaskedTextBox.Clear();
				return;
			}
			emailTextBox.Clear();
			passWordMaskedTextBox.Clear();
			Hide();
			using (ScoreBrowser sb = new ScoreBrowser()) {
				sb.ShowDialog();
			}
			Show();
		}

		private void TextBoxTextChanged(object sender, EventArgs e) {
			loginButton.Enabled = emailTextBox.Text.Length > 0 && passWordMaskedTextBox.Text.Length > 0;
		}

		private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://kottatar-szte.web.app/signup"); 
		}
	}
}
