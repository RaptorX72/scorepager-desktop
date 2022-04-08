using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using scorepager_desktop.Structures;

namespace scorepager_desktop {
	public partial class MainForm : Form {
		Bitmap backgroundImage;
		Bitmap bm;
		Graphics graphics;
		Image bufferImage;
		PointSet pointSet;
		GraphicsPath path;
		bool canPaint = false;
		bool isTyping = false;
		const int ICON_SIZE = 50;
		DrawType drawType = DrawType.PEN;
		Color color = Color.Black;
		Pen p;
		SolidBrush sb;
		List<Point> currentPoints = new List<Point>();
		PictureBox pb = new PictureBox();
		Point textPoint;

		TextBox textBoxCanvasText;

		public MainForm() {
			InitializeComponent();

			textBoxCanvasText = new TextBox();

			textBoxCanvasText.GotFocus += OnFocus;
			textBoxCanvasText.LostFocus += OnDefocus;
			textBoxCanvasText.Size = new Size(0, 0);

			sb = new SolidBrush(color);
			p = new Pen(sb, Convert.ToInt32(widthNumericUpDown.Value));

			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK) {
				backgroundImage = (Bitmap)Bitmap.FromFile(ofd.FileName);
				canvasPictureBox.BackgroundImage = backgroundImage;
				canvasPictureBox.Size = new Size(backgroundImage.Width, backgroundImage.Height);
			} else Environment.Exit(0);

			bm = new Bitmap(backgroundImage.Width, backgroundImage.Height);
			graphics = Graphics.FromImage(bm);
			graphics.Clear(Color.Transparent);
			canvasPictureBox.Image = bm;
			path = new GraphicsPath();
			graphics.CompositingMode = CompositingMode.SourceOver;

			/*PaintCanvas pc = new PaintCanvas(0, 150, 200);
			pc.AddPicture(@"D:\Temp\red.png");
			pc.AddPicture(@"D:\Temp\green.png");
			mainTableLayoutPanel.Controls.Container.Controls.Add(pc);*/
			//MessageBox.Show(mainTableLayoutPanel.Controls.Container.Controls[1].Name);
		}

		private void MergeImages(Bitmap background, Bitmap foreground) {
			Bitmap tmp = new Bitmap(background.Width, background.Height);
			for (int i = 0; i < background.Width; i++) {
				for (int j = 0; j < background.Height; j++) {
					if (foreground.GetPixel(i, j).A != 0) {
						tmp.SetPixel(i, j, foreground.GetPixel(i, j));
					} else {
						tmp.SetPixel(i, j, background.GetPixel(i, j));
					}
				}
			}
			//return tmp;

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
				/*SymbolSelector si = new SymbolSelector();
				si.ShowDialog();
				SymbolType st = si.Symbol;
				si.Dispose();
				Bitmap img = null;
				switch (st) {
					case SymbolType.NONE:
						return;
					case SymbolType.VIOLIN_KEY:
						//img = ResizeImage(Properties.Resources.violin_key_icon);
						break;
					case SymbolType.VALAMI:
						break;
				}
				graphics.DrawImage(RecolorImage(img), e.Location);
				canvasPictureBox.Refresh();*/
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
				if (drawType != DrawType.ERASER) {
					graphics.DrawPath(p, path);
				} else {
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
						p.MiterLimit = p.MiterLimit / 4;
						p.LineJoin = LineJoin.Round;
						p.StartCap = LineCap.Round;
						p.EndCap = LineCap.Round;
						graphics.DrawLines(p, currentPoints.ToArray());
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
		}

		private void textSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.TEXT;
			graphics.CompositingMode = CompositingMode.SourceOver;
		}

		private void eraserSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.ERASER;
			graphics.CompositingMode = CompositingMode.SourceCopy;
		}

		private void highLighterSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.HIGHLIGHTER;
			graphics.CompositingMode = CompositingMode.SourceOver;
		}

		private void symbolSelectButton_Click(object sender, EventArgs e) {
			drawType = DrawType.SYMBOL;
			graphics.CompositingMode = CompositingMode.SourceOver;
		}

		private void opacityTrackBar_ValueChanged(object sender, EventArgs e) {
			SetGlobalColorOpacity();
		}

		private void SetGlobalColor(Color _color) {
			color = Color.FromArgb(opacityTrackBar.Value, _color);
			colorPreviewPanel.BackColor = color;
			sb = new SolidBrush(color);
			p = new Pen(sb, Convert.ToInt32(widthNumericUpDown.Value));
		}

		private void SetGlobalColorOpacity() {
			SetGlobalColor(color);
		}

		private void SetPenWidth() {
			p = new Pen(sb, Convert.ToInt32(widthNumericUpDown.Value));
		}

		private void canvasPictureBox_Paint(object sender, PaintEventArgs e) {

		}

		private void widthNumericUpDown_ValueChanged(object sender, EventArgs e) {
			SetPenWidth();
		}

		private void colorPreviewPanel_MouseClick(object sender, MouseEventArgs e) {
			ColorDialog cd = new ColorDialog();
			cd.Color = color;
			if (cd.ShowDialog() == DialogResult.OK) SetGlobalColor(cd.Color);
		}

		private Bitmap ResizeImage(Image imgToResize) {
			return new Bitmap(imgToResize, new Size(ICON_SIZE, ICON_SIZE));
		}

		private Image RecolorImage(Bitmap img) {
			for (int i = 0; i < img.Width; i++)
				for (int j = 0; j < img.Height; j++)
					if (img.GetPixel(i, j).A != 0)
						img.SetPixel(i, j, color);
			return (Image)img;
		}

		private void button1_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK) {
				backgroundImage = (Bitmap)Bitmap.FromFile(ofd.FileName);
				graphics = Graphics.FromImage(backgroundImage);
				canvasPictureBox.Image = backgroundImage;
				pb.Image = backgroundImage;
			}
		}



		private void OnFocus(object sender = null, EventArgs e = null) {
			SetBufferImage(canvasPictureBox.Image);
		}

		private void OnDefocus(object sender = null, EventArgs e = null) {
			InsertStringToCanvas(textBoxCanvasText.Text);
			isTyping = false;
		}

		private void textBoxCanvasText_TextChanged(object sender, EventArgs e) {
			if (textBoxCanvasText.Focused && isTyping && textBoxCanvasText.Text != "") DrawStringToCanvas(textBoxCanvasText.Text);
		}

		private void SetBufferImage(Image img) {
			bufferImage = new Bitmap(img);
		}

		private void DrawStringToCanvas(string text) {
			graphics.Clear(Color.Transparent);
			graphics.DrawImage(bufferImage, 0, 0);
			graphics.DrawString(text, new Font(new FontFamily("Calibri"), 16f), sb, textPoint);
			canvasPictureBox.Refresh();
		}

		private void InsertStringToCanvas(string text) {
			DrawStringToCanvas(text);
			textBoxCanvasText.Text = "";
		}
	}
}