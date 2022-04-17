using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using scorepager_desktop.Structures;
using scorepager_desktop.Classes;
using scorepager_desktop.UserControls;

namespace scorepager_desktop.Forms {
	public partial class MainForm : Form {
		private PDFFile file;
		private Page currentPage;
		private int currentPageNumber = 0;

		private Bitmap bm;
		private Image bufferImage;
		private Graphics graphics;
		private GraphicsPath path = new GraphicsPath();
		private TextBox textBoxCanvasText;
		private Point textPoint;
		private PointSet pointSet;
		private List<Point> currentPoints = new List<Point>();

		private Pen pen;
		private SolidBrush solidBrush;
		private DrawType drawType = DrawType.PEN;
		private Color color = Color.Black;

		private bool canPaint = false;
		private bool isTyping = false;

		#region UserControls
		List<MasterUserControl> userControls = new List<MasterUserControl>() {
			new SymbolSelector(),
			new TextSelector()
		};
		#endregion
		public MainForm() {
			InitializeComponent();

			textBoxCanvasText = new TextBox();
			textBoxCanvasText.GotFocus += OnFocus;
			textBoxCanvasText.LostFocus += OnDefocus;
			textBoxCanvasText.Size = new Size(0, 0);
			textBoxCanvasText.Location = new Point(0, 0);
			textBoxCanvasText.TextChanged += textBoxCanvasText_TextChanged;
			this.Controls.Add(textBoxCanvasText);

			solidBrush = new SolidBrush(color);
			pen = new Pen(solidBrush, Convert.ToInt32(widthNumericUpDown.Value));

			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK) file = new PDFFile(ofd.FileName);
			else Environment.Exit(0);

			SetCurrentPageAndLayer(1);
		}

		private void SetCurrentPageAndLayer(int pageNumber) {
			if (bm != null) currentPage.SetUserLayer(bm);
			currentPage = file.GetPageByPageNumber(pageNumber);
			currentPageNumber = pageNumber;
			canvasPictureBox.BackgroundImage = currentPage.Bitmap;
			canvasPictureBox.Size = new Size(currentPage.Bitmap.Width, currentPage.Bitmap.Height);
			if (currentPage.GetUserLayer() != null) {
				bm = currentPage.GetUserLayer();
				graphics = Graphics.FromImage(bm);
				canvasPictureBox.Image = bm;
			} else {
				bm = new Bitmap(currentPage.Bitmap.Width, currentPage.Bitmap.Height);
				graphics = Graphics.FromImage(bm);
				graphics.Clear(Color.Transparent);
				canvasPictureBox.Image = bm;
			}
			graphics.CompositingMode = CompositingMode.SourceOver;
			UpdatePageCounterLabel();
		}

		private void UpdatePageCounterLabel() {
			labelPageCount.Text = $"{currentPageNumber} / {file.PageCount}";
		}

		private void MergeImages(Bitmap background, Bitmap foreground) {
			Bitmap tmp = new Bitmap(background.Width, background.Height);
			for (int i = 0; i < background.Width; i++) {
				for (int j = 0; j < background.Height; j++) {
					if (foreground.GetPixel(i, j).A != 0)
						tmp.SetPixel(i, j, foreground.GetPixel(i, j));
					else
						tmp.SetPixel(i, j, background.GetPixel(i, j));
				}
			}
			canvasPictureBox.Image = background;
		}

		private void canvasPictureBox_MouseDown(object sender, MouseEventArgs e) {
			if (drawType == DrawType.TEXT) {
				if (textBoxCanvasText.Focused) {
					isTyping = false;
					InsertStringToCanvas(textBoxCanvasText.Text);
					SetBufferImage(canvasPictureBox.Image);
					isTyping = true;
				} else {
					textBoxCanvasText.Focus();
					isTyping = true;
				}
				textPoint = e.Location;
			} else if (drawType == DrawType.SYMBOL) {
				Image img = null;
				switch (((SymbolSelector)userControls[0]).Type) {
					case SymbolType.NONE:
						return;
					case SymbolType.VIOLIN_KEY:
						img = ResizeImage(Properties.Resources.icon_violin);
						break;
					case SymbolType.SHARP:
						img = ResizeImage(Properties.Resources.icon_sharp);
						break;
				}
				graphics.DrawImage(
					RecolorBitmap((Bitmap)img, color),
					new Point(
						e.Location.X - (int)widthNumericUpDown.Value / 2,
						e.Location.Y - (int)widthNumericUpDown.Value / 2
					)
				);
				canvasPictureBox.Refresh();
			} else {
				currentPoints.Clear();
				canPaint = true;
				pointSet.Start = e.Location;
				SetBufferImage(canvasPictureBox.Image);
			}
		}

		private void canvasPictureBox_MouseMove(object sender, MouseEventArgs e) {
			if (canPaint) {
				currentPoints.Add(e.Location);
				pointSet.End = e.Location;
				path.AddLine(pointSet.Start, pointSet.End);
				if (drawType != DrawType.ERASER) graphics.DrawPath(pen, path);
				else {
					SolidBrush erasesb = new SolidBrush(Color.FromArgb(0, Color.Red));
					Pen erasep = new Pen(erasesb, Convert.ToInt32(widthNumericUpDown.Value));
					graphics.DrawPath(erasep, path);
				}
				pointSet.Start = pointSet.End;
				canvasPictureBox.Refresh();
				Invalidate();
			}
		}

		private void canvasPictureBox_MouseUp(object sender, MouseEventArgs e) {
			if (canPaint) {
				canPaint = false;
				graphics.Clear(Color.Transparent);
				graphics.DrawImage(bufferImage, 0, 0);
				if (currentPoints.Count > 1) {
					if (drawType == DrawType.ERASER) {
						SolidBrush erasesb = new SolidBrush(Color.FromArgb(0, Color.Red));
						Pen erasep = new Pen(erasesb, Convert.ToInt32(widthNumericUpDown.Value));
						graphics.DrawLines(erasep, currentPoints.ToArray());
					} else {
						pen.MiterLimit = pen.MiterLimit / 4;
						pen.LineJoin = LineJoin.Round;
						pen.StartCap = LineCap.Round;
						pen.EndCap = LineCap.Round;
						graphics.DrawLines(pen, currentPoints.ToArray());
					}
				}
				path.Reset();
				currentPoints.Clear();
				canvasPictureBox.Refresh();
			}
		}

		private void penSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.PEN;
			graphics.CompositingMode = CompositingMode.SourceOver;
			LoadUCToToolbarPanel();
		}

		private void textSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.TEXT;
			graphics.CompositingMode = CompositingMode.SourceOver;
			LoadUCToToolbarPanel();
		}

		private void eraserSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.ERASER;
			graphics.CompositingMode = CompositingMode.SourceCopy;
			LoadUCToToolbarPanel();
		}

		private void highLighterSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.HIGHLIGHTER;
			graphics.CompositingMode = CompositingMode.SourceOver;
			LoadUCToToolbarPanel();
		}

		private void symbolSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.SYMBOL;
			graphics.CompositingMode = CompositingMode.SourceOver;
			LoadUCToToolbarPanel();
		}

		private void opacityTrackBar_ValueChanged(object sender, EventArgs e) {
			SetGlobalColorOpacity();
		}

		private void SetGlobalColor(Color _color) {
			color = Color.FromArgb(opacityTrackBar.Value, _color);
			colorPreviewPanel.BackColor = color;
			solidBrush.Color = color;
			pen.Brush = solidBrush;
			pen.Width = Convert.ToInt32(widthNumericUpDown.Value);
		}

		private void SetGlobalColorOpacity() {
			SetGlobalColor(color);
		}

		private void canvasPictureBox_Paint(object sender, PaintEventArgs e) {

		}

		private void widthNumericUpDown_ValueChanged(object sender, EventArgs e) {
			pen.Width = Convert.ToInt32(widthNumericUpDown.Value);
		}

		private void colorPreviewPanel_MouseClick(object sender, MouseEventArgs e) {
			ColorDialog cd = new ColorDialog();
			cd.Color = color;
			if (cd.ShowDialog() == DialogResult.OK) SetGlobalColor(cd.Color);
		}

		private Image ResizeImage(Image imgToResize) {
			return (Image) new Bitmap(imgToResize, new Size((int)widthNumericUpDown.Value, (int)widthNumericUpDown.Value));
		}

		private Bitmap RecolorBitmap(Bitmap img, Color color) {
			for (int i = 0; i < img.Width; i++)
				for (int j = 0; j < img.Height; j++)
					if (img.GetPixel(i, j).A != 0)
						img.SetPixel(i, j, color);
			return img;
		}

		private void OnFocus(object sender = null, EventArgs e = null) {
			SetBufferImage(canvasPictureBox.Image);
		}

		private void OnDefocus(object sender = null, EventArgs e = null) {
			InsertStringToCanvas(textBoxCanvasText.Text);
			isTyping = false;
		}

		private void textBoxCanvasText_TextChanged(object sender, EventArgs e) {
			if (textBoxCanvasText.Focused && isTyping) DrawStringToCanvas(textBoxCanvasText.Text);
		}

		private void SetBufferImage(Image img) {
			bufferImage = new Bitmap(img);
		}

		private void DrawStringToCanvas(string text) {
			graphics.Clear(Color.Transparent);
			graphics.DrawImage(bufferImage, 0, 0);
			graphics.DrawString(text, new Font(new FontFamily(((TextSelector)userControls[1]).SelectedFont), (int)widthNumericUpDown.Value), solidBrush, textPoint);
			canvasPictureBox.Refresh();
		}

		private void InsertStringToCanvas(string text) {
			DrawStringToCanvas(text);
			textBoxCanvasText.Text = "";
		}

		private void PreviousPageButton_Click(object sender, EventArgs e) {
			if (currentPageNumber - 1 == 0) return;
			SetCurrentPageAndLayer(--currentPageNumber);
		}

		private void nextPageButton_Click(object sender, EventArgs e) {
			if (currentPageNumber + 1 > file.PageCount) return;
			SetCurrentPageAndLayer(++currentPageNumber);
		}

		private void toolBarPanel_Resize(object sender, EventArgs e) {
			userControls.ForEach(control => { if (control.IsActive) control.ResizeControl(toolBarPanel.Size); });
		}

		private void LoadUCToToolbarPanel() {
			userControls.ForEach(control => control.SetActive(false));
			toolBarPanel.Controls.Clear();
			int index = -1;
			switch (drawType) {
				case DrawType.PEN:
					break;
				case DrawType.HIGHLIGHTER:
					break;
				case DrawType.TEXT:
					index = 1;
					break;
				case DrawType.SYMBOL:
					index = 0;
					break;
				case DrawType.ERASER:
					break;
			}
			if (index == -1) return;
			userControls[index].SetActive(true);
			userControls[index].ResizeControl(toolBarPanel.Size);
			toolBarPanel.Controls.Add(userControls[index]);
		}
	}
}
