//using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.WinForms;

namespace WhereAreMyFiles
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.panelNav = new System.Windows.Forms.Panel();
            this.labelCurrentPath = new ReaLTaiizor.Controls.MaterialLabel();
            this.buttonBack = new ReaLTaiizor.Controls.MaterialButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.listView1 = new ReaLTaiizor.Controls.MaterialListView();
            this.panelNav.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pieChart1
            // 
            this.pieChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pieChart1.Location = new System.Drawing.Point(0, 0);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(420, 535);
            this.pieChart1.TabIndex = 1;
            // 
            // panelNav
            // 
            this.panelNav.Controls.Add(this.labelCurrentPath);
            this.panelNav.Controls.Add(this.buttonBack);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(420, 44);
            this.panelNav.TabIndex = 0;
            // 
            // labelCurrentPath
            // 
            this.labelCurrentPath.Depth = 0;
            this.labelCurrentPath.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelCurrentPath.Location = new System.Drawing.Point(64, 0);
            this.labelCurrentPath.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.labelCurrentPath.Name = "labelCurrentPath";
            this.labelCurrentPath.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelCurrentPath.Size = new System.Drawing.Size(356, 44);
            this.labelCurrentPath.TabIndex = 1;
            this.labelCurrentPath.Text = "This PC";
            this.labelCurrentPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonBack
            // 
            this.buttonBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonBack.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.buttonBack.Depth = 0;
            this.buttonBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonBack.Enabled = false;
            this.buttonBack.HighEmphasis = true;
            this.buttonBack.Icon = null;
            this.buttonBack.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(0, 6, 4, 6);
            this.buttonBack.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.NoAccentTextColor = System.Drawing.Color.Empty;
            this.buttonBack.Size = new System.Drawing.Size(64, 44);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "Back";
            this.buttonBack.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.buttonBack.UseAccentColor = false;
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelLeft.Controls.Add(this.panelNav);
            this.panelLeft.Controls.Add(this.pieChart1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 64);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(420, 535);
            this.panelLeft.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.AutoSizeTable = false;
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Depth = 0;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(420, 64);
            this.listView1.MinimumSize = new System.Drawing.Size(200, 100);
            this.listView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listView1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(692, 535);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1112, 599);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panelLeft);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 64, 0, 0);
            this.Text = "File Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelNav.ResumeLayout(false);
            this.panelNav.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        //private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private LiveCharts.WinForms.PieChart pieChart1;
        private System.Windows.Forms.Panel panelNav;
        private ReaLTaiizor.Controls.MaterialLabel labelCurrentPath;
        private ReaLTaiizor.Controls.MaterialButton buttonBack;
        private System.Windows.Forms.Panel panelLeft;
        private ReaLTaiizor.Controls.MaterialListView listView1;
    }
}

