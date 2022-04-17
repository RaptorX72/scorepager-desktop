using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scorepager_desktop.Classes {
	public class Score {
		private string composer;
		private string title;
		private string storageFolder;
		private string url;
		private bool rented;
		private long timestamp;

		public string Composer { get => composer; }
		public string Title { get => title; }
		public string StorageFolder { get => storageFolder; set => storageFolder = value; }
		public string Url { get => url; set => url = value; }
		public bool Rented { get => rented; set => rented = value; }
		public long RentDate {
			get => timestamp;
			set { if (!rented) timestamp = value; }
		} 


		public Score(string composer, string title, string storageFolder, string url, bool rented = false, long timestamp = 0) {
			this.composer = composer;
			this.title = title;
			this.storageFolder = storageFolder;
			this.url = url;
			this.rented = rented;
			this.timestamp = timestamp;
		}

		public override string ToString() {
			return $"{this.composer}:{this.title}" + (this.rented ? "(Rented)" : "");
		}
	}
}
