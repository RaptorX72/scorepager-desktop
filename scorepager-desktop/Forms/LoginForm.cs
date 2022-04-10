using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				MainForm mf = new MainForm();
				this.Hide();
				mf.ShowDialog();
			}
		}
	}
}
