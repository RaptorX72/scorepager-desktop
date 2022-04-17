using System;
using System.Windows.Forms;
using scorepager_desktop.Classes;

namespace scorepager_desktop.Forms {
	public partial class LoginForm : Form {
		private FirebaseClient client;

		public LoginForm() {
			InitializeComponent();
			client = FirebaseClient.GetInstance();
		}

		private void loginButton_Click(object sender, EventArgs e) {
			bool success = client.Login(emailTextBox.Text, passWordMaskedTextBox.Text);
			if (success) {
				emailTextBox.Clear();
				passWordMaskedTextBox.Clear();
				MainForm mf = new MainForm();
				this.Hide();
				mf.ShowDialog();
				this.Show();
			}
		}
	}
}
