
namespace scorepager_desktop.UserControls {
	partial class SymbolSelector {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// mainFlowLayoutPanel
			// 
			this.mainFlowLayoutPanel.AutoScroll = true;
			this.mainFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.mainFlowLayoutPanel.Name = "mainFlowLayoutPanel";
			this.mainFlowLayoutPanel.Size = new System.Drawing.Size(400, 100);
			this.mainFlowLayoutPanel.TabIndex = 0;
			// 
			// SymbolSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainFlowLayoutPanel);
			this.Name = "SymbolSelector";
			this.Size = new System.Drawing.Size(400, 100);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel mainFlowLayoutPanel;
	}
}
