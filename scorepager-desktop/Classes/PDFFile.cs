using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Apitron.PDF.Rasterizer;
using Apitron.PDF.Rasterizer.Configuration;

namespace scorepager_desktop.Classes {
	public class PDFFile {
		private Score score;
		private List<Page> pages = new List<Page>();
		private int pageCount = 0;

		public int PageCount { get => pageCount; }

		public List<Page> Pages { get => pages; }

		public PDFFile(Score score) {
			this.score = score;
			using (FileStream fs = new FileStream(this.score.Url, FileMode.Open))
			using (Document document = new Document(fs)) {
				for (int i = 0; i < document.Pages.Count; i++) {
					Bitmap bitmap = document.Pages[i].Render((int)document.Pages[i].Width, (int)document.Pages[i].Height, new RenderingSettings());
					Page page = new Page(
						bitmap,
						++pageCount,
						new Layer(StorageManager.GetLayerForScore(this.score, PageCount), Structures.LayerOwner.USER)
					);
					pages.Add(page);
				}
			}
		}

		public Page GetPageByPageNumber(int pageNumber) {
			foreach (Page page in pages) if (page.Number == pageNumber) return page;
			return null;
		}

		public bool UpdatePageByIndex(Page page, int index) {
			if (index < 0 || index >= pages.Count) return false;
			pages[index] = page;
			return true;
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

		public void Save() {
			FirebaseClient client = FirebaseClient.GetInstance();
			foreach (Page item in pages)
				if (item.PageEdited)
					StorageManager.SaveLayerForScore(score, item.Number, item.GetUserLayer());
			client.UploadBitmaps(client.UserID, score, pages);
		}
	}
}
