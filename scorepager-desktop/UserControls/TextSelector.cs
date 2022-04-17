using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
