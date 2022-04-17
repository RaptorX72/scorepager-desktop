using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using scorepager_desktop.Structures;

namespace scorepager_desktop.Classes {
	public class Layer {
		private Bitmap bitmap;
		private LayerOwner owner;
		public Bitmap Bitmap { get => bitmap; set => bitmap = value; }
		public LayerOwner Owner { get => owner; }

		public Layer(Bitmap bitmap, LayerOwner owner) {
			this.bitmap = bitmap;
			this.owner = owner;
		}
	}
}
