namespace scorepager_desktop.UserControls
{
    partial class TextSelector
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FontComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // FontComboBox
            // 
            this.FontComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FontComboBox.DropDownHeight = 300;
            this.FontComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontComboBox.FormattingEnabled = true;
            this.FontComboBox.IntegralHeight = false;
            this.FontComboBox.Location = new System.Drawing.Point(3, 40);
            this.FontComboBox.Name = "FontComboBox";
            this.FontComboBox.Size = new System.Drawing.Size(300, 21);
            this.FontComboBox.TabIndex = 0;
            this.FontComboBox.SelectedIndexChanged += new System.EventHandler(this.FontComboBox_SelectedIndexChanged);
            // 
            // TextSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FontComboBox);
            this.Name = "TextSelector";
            this.Size = new System.Drawing.Size(400, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox FontComboBox;
    }
}
