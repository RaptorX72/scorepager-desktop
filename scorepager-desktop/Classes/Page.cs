using System.Drawing;

namespace scorepager_desktop.Classes {
	public class Page {
		private Bitmap bitmap;
		private int number;
		private Layer userLayer;
		private bool pageEdited;

		public Bitmap Bitmap { get => bitmap; }

		public int Number { get => number; }

		public bool PageEdited { get => pageEdited; set => pageEdited = value; }

		public Page(Bitmap bitmap, int number, Layer userLayer) {
			this.bitmap = bitmap;
			this.number = number;
			this.userLayer = userLayer;
			pageEdited = false;
		}

		public void SetUserLayer(Bitmap bitmap) {
			userLayer.Bitmap = bitmap;
		}

		public Bitmap GetUserLayer() {
			return userLayer.Bitmap;
		}
	}
}
