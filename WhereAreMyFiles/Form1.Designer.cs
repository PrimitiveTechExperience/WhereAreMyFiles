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
            this.labelCurrentPath = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panelNav.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pieChart1
            // 
            this.pieChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pieChart1.Location = new System.Drawing.Point(0, 0);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(1112, 599);
            this.pieChart1.TabIndex = 1;
            this.pieChart1.InnerRadius = 60;
            this.pieChart1.HoverPushOut = 15;
            this.pieChart1.LegendLocation = LegendLocation.Right;
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
            this.labelCurrentPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentPath.Location = new System.Drawing.Point(75, 0);
            this.labelCurrentPath.Name = "labelCurrentPath";
            this.labelCurrentPath.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelCurrentPath.Size = new System.Drawing.Size(345, 44);
            this.labelCurrentPath.TabIndex = 1;
            this.labelCurrentPath.Text = "This PC";
            this.labelCurrentPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonBack
            // 
            this.buttonBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonBack.Enabled = false;
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 44);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "Back";
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
            this.panelLeft.Size = new System.Drawing.Size(420, 599);
            this.panelLeft.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(420, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(692, 599);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
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
            this.Text = "File Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelNav.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        //private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private LiveCharts.WinForms.PieChart pieChart1;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Label labelCurrentPath;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ListView listView1;
    }
}

