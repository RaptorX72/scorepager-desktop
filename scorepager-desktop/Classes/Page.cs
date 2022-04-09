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
		private List<Layer> layers;

		public Bitmap Bitmap { get => bitmap; }
		public int Number { get => number; }
		public List<Layer> Layers { get => layers; set => layers = value; }

		public Page(Bitmap bitmap, int number, List<Layer> layers) {
			this.bitmap = bitmap;
			this.number = number;
			this.layers = layers;
		}
	}
}
