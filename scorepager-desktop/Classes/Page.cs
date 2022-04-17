using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scorepager_desktop.Classes {
	class Page {
		private Bitmap bitmap;
		private int number;
		private Layer userLayer;

		public Bitmap Bitmap { get => bitmap; }
		public int Number { get => number; }

		public Page(Bitmap bitmap, int number, Layer userLayer) {
			this.bitmap = bitmap;
			this.number = number;
			this.userLayer = userLayer;
		}

		public void SetUserLayer(Bitmap bitmap) {
			userLayer.Bitmap = bitmap;
		}

		public Bitmap GetUserLayer() {
			return userLayer.Bitmap;
		}
	}
}
