using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using scorepager_desktop.Structures;

namespace scorepager_desktop.UserControls {
	public partial class SymbolSelector : UserControl {
		private List<Button> buttons = new List<Button>();
		private SymbolType type = SymbolType.NONE;

		public SymbolType Type { get => type; }

		public SymbolSelector(Size size) {
			InitializeComponent();
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");

			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");

			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			CreateButton(Properties.Resources.icon_violin, "violin");
			CreateButton(Properties.Resources.icon_sharp, "sharp");
			ResizeControl(size);
		}

		private void CreateButton(Image image, string tag) {
			Button button = new Button();
			button.Size = new Size(1, 1);
			button.FlatStyle = FlatStyle.Flat;
			button.Tag = tag;
			button.BackgroundImageLayout = ImageLayout.Stretch;
			button.BackgroundImage = image;
			button.Margin = new Padding(0, 0, 0, 0);
			button.Click += symbolButton_Click;
			buttons.Add(button);
			this.mainFlowLayoutPanel.Controls.Add(button);
		}

		public void ResizeControl(Size size) {
			this.Size = size;
			int widthSize = (int)Math.Round((double)(mainFlowLayoutPanel.Width - 20) / 8);
			for (int i = 0; i < buttons.Count; i++) buttons[i].Size = new Size(widthSize, mainFlowLayoutPanel.Height);
		}

		private void symbolButton_Click(object sender, EventArgs e) {
			Button btn = (Button)sender;
			if (btn.Tag.ToString() == "violin") type = SymbolType.VIOLIN_KEY;
			else if (btn.Tag.ToString() == "sharp") type = SymbolType.SHARP;
			else type = SymbolType.NONE;
		}
	}
}
