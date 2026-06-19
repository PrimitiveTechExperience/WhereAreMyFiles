//using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.WinForms;
using System.Drawing;

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
            this.listView1 = new ReaLTaiizor.Controls.MaterialListView();
            this.overlayPanel = new System.Windows.Forms.Panel();
            this.settingsCard = new ReaLTaiizor.Controls.MaterialCard();
            this.panelNav = new System.Windows.Forms.Panel();
            this.labelCurrentPath = new ReaLTaiizor.Controls.MaterialLabel();
            this.buttonBack = new ReaLTaiizor.Controls.MaterialButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.masterPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.settingsLabel = new ReaLTaiizor.Controls.MaterialLabel();
            this.showFilesSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            this.showHiddenSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            this.closeButton = new ReaLTaiizor.Controls.MaterialButton();
            this.overlayPanel.SuspendLayout();
            this.settingsCard.SuspendLayout();
            this.panelNav.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.masterPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
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
            this.listView1.Location = new System.Drawing.Point(420, 0);
            this.listView1.MinimumSize = new System.Drawing.Size(200, 100);
            this.listView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listView1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(692, 532);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // overlayPanel
            // 
            this.overlayPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.overlayPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.overlayPanel.Controls.Add(this.settingsCard);
            this.overlayPanel.Location = new System.Drawing.Point(0, 0);
            this.overlayPanel.Name = "overlayPanel";
            this.overlayPanel.Size = new System.Drawing.Size(1112, 532);
            this.overlayPanel.TabIndex = 2;
            this.overlayPanel.Visible = false;
            this.overlayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.overlayPanel_Paint);
            // 
            // settingsCard
            // 
            this.settingsCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.settingsCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.settingsCard.Controls.Add(this.closeButton);
            this.settingsCard.Controls.Add(this.showHiddenSwitch);
            this.settingsCard.Controls.Add(this.showFilesSwitch);
            this.settingsCard.Controls.Add(this.settingsLabel);
            this.settingsCard.Depth = 0;
            this.settingsCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.settingsCard.Location = new System.Drawing.Point(307, 14);
            this.settingsCard.Margin = new System.Windows.Forms.Padding(14);
            this.settingsCard.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.settingsCard.Name = "settingsCard";
            this.settingsCard.Padding = new System.Windows.Forms.Padding(14);
            this.settingsCard.Size = new System.Drawing.Size(461, 504);
            this.settingsCard.TabIndex = 0;
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
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(420, 532);
            this.panelLeft.TabIndex = 0;
            // 
            // pieChart1
            // 
            this.pieChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pieChart1.Location = new System.Drawing.Point(0, 0);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(420, 532);
            this.pieChart1.TabIndex = 1;
            // 
            // masterPanel
            // 
            this.masterPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.masterPanel.Controls.Add(this.overlayPanel);
            this.masterPanel.Controls.Add(this.mainPanel);
            this.masterPanel.Location = new System.Drawing.Point(0, 64);
            this.masterPanel.Name = "masterPanel";
            this.masterPanel.Size = new System.Drawing.Size(1112, 532);
            this.masterPanel.TabIndex = 1;
            this.masterPanel.Visible = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.listView1);
            this.mainPanel.Controls.Add(this.panelLeft);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1112, 532);
            this.mainPanel.TabIndex = 1;
            // 
            // settingsLabel
            // 
            this.settingsLabel.AutoSize = true;
            this.settingsLabel.Depth = 0;
            this.settingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.settingsLabel.Location = new System.Drawing.Point(18, 18);
            this.settingsLabel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Size = new System.Drawing.Size(59, 19);
            this.settingsLabel.TabIndex = 0;
            this.settingsLabel.Text = "Settings";
            // 
            // showFilesSwitch
            // 
            this.showFilesSwitch.AutoSize = true;
            this.showFilesSwitch.Depth = 0;
            this.showFilesSwitch.Location = new System.Drawing.Point(21, 64);
            this.showFilesSwitch.Margin = new System.Windows.Forms.Padding(0);
            this.showFilesSwitch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.showFilesSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.showFilesSwitch.Name = "showFilesSwitch";
            this.showFilesSwitch.Ripple = true;
            this.showFilesSwitch.Size = new System.Drawing.Size(171, 37);
            this.showFilesSwitch.TabIndex = 1;
            this.showFilesSwitch.Text = "Show Only Files";
            this.showFilesSwitch.UseAccentColor = false;
            this.showFilesSwitch.UseVisualStyleBackColor = true;
            // 
            // showHiddenSwitch
            // 
            this.showHiddenSwitch.AutoSize = true;
            this.showHiddenSwitch.Depth = 0;
            this.showHiddenSwitch.Location = new System.Drawing.Point(21, 101);
            this.showHiddenSwitch.Margin = new System.Windows.Forms.Padding(0);
            this.showHiddenSwitch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.showHiddenSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.showHiddenSwitch.Name = "showHiddenSwitch";
            this.showHiddenSwitch.Ripple = true;
            this.showHiddenSwitch.Size = new System.Drawing.Size(240, 37);
            this.showHiddenSwitch.TabIndex = 2;
            this.showHiddenSwitch.Text = "Show Hidden Folder/Files";
            this.showHiddenSwitch.UseAccentColor = false;
            this.showHiddenSwitch.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.closeButton.Depth = 0;
            this.closeButton.HighEmphasis = true;
            this.closeButton.Icon = null;
            this.closeButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.closeButton.Location = new System.Drawing.Point(386, 9);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.closeButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.closeButton.Name = "closeButton";
            this.closeButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.closeButton.Size = new System.Drawing.Size(66, 36);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.closeButton.UseAccentColor = false;
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1112, 599);
            this.Controls.Add(this.masterPanel);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 64, 0, 0);
            this.Text = "File Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.overlayPanel.ResumeLayout(false);
            this.settingsCard.ResumeLayout(false);
            this.settingsCard.PerformLayout();
            this.panelNav.ResumeLayout(false);
            this.panelNav.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.masterPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.MaterialListView listView1;
        private System.Windows.Forms.Panel overlayPanel;
        private ReaLTaiizor.Controls.MaterialCard settingsCard;
        private System.Windows.Forms.Panel panelNav;
        private ReaLTaiizor.Controls.MaterialLabel labelCurrentPath;
        private ReaLTaiizor.Controls.MaterialButton buttonBack;
        private System.Windows.Forms.Panel panelLeft;
        private PieChart pieChart1;
        private System.Windows.Forms.Panel masterPanel;
        private System.Windows.Forms.Panel mainPanel;
        private ReaLTaiizor.Controls.MaterialLabel settingsLabel;
        private ReaLTaiizor.Controls.MaterialButton closeButton;
        private ReaLTaiizor.Controls.MaterialSwitch showHiddenSwitch;
        private ReaLTaiizor.Controls.MaterialSwitch showFilesSwitch;
    }
}

