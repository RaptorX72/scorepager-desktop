using System;
using System.Drawing;
using System.Drawing.Text;

namespace scorepager_desktop.UserControls {
	public partial class TextSelector : MasterUserControl {
		private string selectedFont = "";

		public string SelectedFont { get => selectedFont; }

		public TextSelector() {
			InitializeComponent();
			using (InstalledFontCollection col = new InstalledFontCollection()) {
				foreach (FontFamily ff in col.Families) FontComboBox.Items.Add(ff.Name);
			}
			FontComboBox.SelectedIndex = 0;
		}

		private void FontComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			selectedFont = FontComboBox.Items[FontComboBox.SelectedIndex].ToString();
		}
	}
}
