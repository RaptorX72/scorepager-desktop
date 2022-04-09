using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace scorepager_desktop.Classes {
	class PDFFile {
		private List<Page> pages;
		private int pageCount;

		public int PageCount { get => pageCount; }
		public List<Page> Pages { get => pages; }

		public PDFFile(List<Page> pages, int pageCount) {
			this.pages = pages;
			this.pageCount = pageCount;
		}

		public bool UpdatePage(int index, Page page) {
			if (index >= 0 && index < pages.Count) {
				pages[index] = page;
				return true;
			}
			return false;
		}
	}
}
