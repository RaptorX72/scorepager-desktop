using System.Drawing;
using System.Windows.Forms;

namespace scorepager_desktop.UserControls {
	public partial class MasterUserControl : UserControl {
		protected bool active = false;

		public bool IsActive { get => active; }

		public MasterUserControl() {
			InitializeComponent();
		}

		public void SetActive(bool active) {
			this.active = active;
		}

		public virtual void ResizeControl(Size size) {
			Size = size;
		}
	}
}
