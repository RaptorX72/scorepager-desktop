using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Apitron.PDF.Rasterizer;
using Apitron.PDF.Rasterizer.Configuration;

namespace scorepager_desktop.Classes {
	class PDFFile {
		private List<Page> pages = new List<Page>();
		private int pageCount = 0;

		public int PageCount { get => pageCount; }
		public List<Page> Pages { get => pages; }

		public PDFFile(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Open))
			using (Document document = new Document(fs)) {
				for (int i = 0; i < document.Pages.Count; i++) {
					Bitmap bitmap = document.Pages[i].Render((int)document.Pages[i].Width, (int)document.Pages[i].Height, new RenderingSettings());
					Page page = new Page(bitmap, ++pageCount, new Layer(null, Structures.LayerOwner.USER) ,new List<Layer>() { new Layer(null, Structures.LayerOwner.OTHER) });
					pages.Add(page);
				}
			}
		}

		public Page GetPageByPageNumber(int pageNumber) {
			foreach (Page page in pages) if (page.Number == pageNumber) return page;
			return null;
		}

		public bool UpdatePageByIndex(Page page, int index) {
			if (index >= 0 && index < pages.Count) {
				pages[index] = page;
				return true;
			}
			return false;
		}

		public bool UpdatePageByNumber(Page page, int number) {
			for (int i = 0; i < pages.Count; i++) {
				if (pages[i].Number == number) {
					pages[i] = page;
					return true;
				}
			}
			return false;
		}
	}
}
