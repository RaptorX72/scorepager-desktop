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
			bool success = client.Login(emailTextBox.Text, passWordMaskedTextBox.Text);
			if (success) {
				emailTextBox.Clear();
				passWordMaskedTextBox.Clear();
				ScoreBrowser sb = new ScoreBrowser();
				this.Hide();
				sb.ShowDialog();
				sb.Dispose();
				this.Show();
			}
		}

		private void TextBoxTextChanged(object sender, EventArgs e) {
			loginButton.Enabled = emailTextBox.Text.Length > 0 && passWordMaskedTextBox.Text.Length > 0;
		}
	}
}
