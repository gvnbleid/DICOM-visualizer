using SlimDX.Windows;
using System.Drawing;

namespace DICOM_visualizer
{
    partial class DicomForm
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
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dicomPictureBox = new System.Windows.Forms.PictureBox();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.visualizeButton = new System.Windows.Forms.Button();
            this.breakPanel2 = new System.Windows.Forms.Panel();
            this.maximalValueLabel = new System.Windows.Forms.Label();
            this.minimalValueLabel = new System.Windows.Forms.Label();
            this.maximalLabel = new System.Windows.Forms.Label();
            this.minimalLabel = new System.Windows.Forms.Label();
            this.maximalValueTrackbar = new System.Windows.Forms.TrackBar();
            this.minimalValueTrackbar = new System.Windows.Forms.TrackBar();
            this.breakPanel1 = new System.Windows.Forms.Panel();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathValueLabel = new System.Windows.Forms.Label();
            this.loadDicomButton = new System.Windows.Forms.Button();
            this.mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dicomPictureBox)).BeginInit();
            this.controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximalValueTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimalValueTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.57143F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.mainTableLayoutPanel.Controls.Add(this.dicomPictureBox, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.controlsPanel, 1, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 1;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(784, 562);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // dicomPictureBox
            // 
            this.dicomPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dicomPictureBox.Location = new System.Drawing.Point(3, 3);
            this.dicomPictureBox.Name = "dicomPictureBox";
            this.dicomPictureBox.Size = new System.Drawing.Size(609, 556);
            this.dicomPictureBox.TabIndex = 0;
            this.dicomPictureBox.TabStop = false;
            // 
            // controlsPanel
            // 
            this.controlsPanel.BackColor = System.Drawing.Color.LightGray;
            this.controlsPanel.Controls.Add(this.visualizeButton);
            this.controlsPanel.Controls.Add(this.breakPanel2);
            this.controlsPanel.Controls.Add(this.maximalValueLabel);
            this.controlsPanel.Controls.Add(this.minimalValueLabel);
            this.controlsPanel.Controls.Add(this.maximalLabel);
            this.controlsPanel.Controls.Add(this.minimalLabel);
            this.controlsPanel.Controls.Add(this.maximalValueTrackbar);
            this.controlsPanel.Controls.Add(this.minimalValueTrackbar);
            this.controlsPanel.Controls.Add(this.breakPanel1);
            this.controlsPanel.Controls.Add(this.pathLabel);
            this.controlsPanel.Controls.Add(this.pathValueLabel);
            this.controlsPanel.Controls.Add(this.loadDicomButton);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsPanel.Location = new System.Drawing.Point(618, 3);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(163, 556);
            this.controlsPanel.TabIndex = 1;
            // 
            // visualizeButton
            // 
            this.visualizeButton.Location = new System.Drawing.Point(3, 168);
            this.visualizeButton.Name = "visualizeButton";
            this.visualizeButton.Size = new System.Drawing.Size(157, 23);
            this.visualizeButton.TabIndex = 11;
            this.visualizeButton.Text = "Visualize in 3D";
            this.visualizeButton.UseVisualStyleBackColor = true;
            this.visualizeButton.Click += new System.EventHandler(this.visualizeButton_Click);
            // 
            // breakPanel2
            // 
            this.breakPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.breakPanel2.Location = new System.Drawing.Point(0, 157);
            this.breakPanel2.Name = "breakPanel2";
            this.breakPanel2.Size = new System.Drawing.Size(163, 5);
            this.breakPanel2.TabIndex = 10;
            // 
            // maximalValueLabel
            // 
            this.maximalValueLabel.AutoSize = true;
            this.maximalValueLabel.Location = new System.Drawing.Point(122, 138);
            this.maximalValueLabel.Name = "maximalValueLabel";
            this.maximalValueLabel.Size = new System.Drawing.Size(25, 13);
            this.maximalValueLabel.TabIndex = 9;
            this.maximalValueLabel.Text = "255";
            // 
            // minimalValueLabel
            // 
            this.minimalValueLabel.AutoSize = true;
            this.minimalValueLabel.Location = new System.Drawing.Point(122, 90);
            this.minimalValueLabel.Name = "minimalValueLabel";
            this.minimalValueLabel.Size = new System.Drawing.Size(13, 13);
            this.minimalValueLabel.TabIndex = 8;
            this.minimalValueLabel.Text = "0";
            // 
            // maximalLabel
            // 
            this.maximalLabel.AutoSize = true;
            this.maximalLabel.Location = new System.Drawing.Point(11, 138);
            this.maximalLabel.Name = "maximalLabel";
            this.maximalLabel.Size = new System.Drawing.Size(110, 13);
            this.maximalLabel.TabIndex = 7;
            this.maximalLabel.Text = "Maximal pixel intensity";
            // 
            // minimalLabel
            // 
            this.minimalLabel.AutoSize = true;
            this.minimalLabel.Location = new System.Drawing.Point(11, 90);
            this.minimalLabel.Name = "minimalLabel";
            this.minimalLabel.Size = new System.Drawing.Size(107, 13);
            this.minimalLabel.TabIndex = 6;
            this.minimalLabel.Text = "Minimal pixel intensity";
            // 
            // maximalValueTrackbar
            // 
            this.maximalValueTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.maximalValueTrackbar.Location = new System.Drawing.Point(3, 106);
            this.maximalValueTrackbar.Maximum = 255;
            this.maximalValueTrackbar.Name = "maximalValueTrackbar";
            this.maximalValueTrackbar.Size = new System.Drawing.Size(160, 45);
            this.maximalValueTrackbar.TabIndex = 5;
            this.maximalValueTrackbar.Value = 255;
            this.maximalValueTrackbar.Scroll += new System.EventHandler(this.maximalValueTrackbar_Scroll);
            // 
            // minimalValueTrackbar
            // 
            this.minimalValueTrackbar.Location = new System.Drawing.Point(3, 58);
            this.minimalValueTrackbar.Maximum = 255;
            this.minimalValueTrackbar.Name = "minimalValueTrackbar";
            this.minimalValueTrackbar.Size = new System.Drawing.Size(160, 45);
            this.minimalValueTrackbar.TabIndex = 4;
            this.minimalValueTrackbar.Scroll += new System.EventHandler(this.minimalValueTrackbar_Scroll);
            // 
            // breakPanel1
            // 
            this.breakPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.breakPanel1.Location = new System.Drawing.Point(0, 48);
            this.breakPanel1.Name = "breakPanel1";
            this.breakPanel1.Size = new System.Drawing.Size(163, 5);
            this.breakPanel1.TabIndex = 3;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.BackColor = System.Drawing.Color.LightGray;
            this.pathLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pathLabel.Location = new System.Drawing.Point(3, 3);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(34, 15);
            this.pathLabel.TabIndex = 2;
            this.pathLabel.Text = "Path:";
            // 
            // pathValueLabel
            // 
            this.pathValueLabel.AutoSize = true;
            this.pathValueLabel.BackColor = System.Drawing.Color.LightGray;
            this.pathValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pathValueLabel.Location = new System.Drawing.Point(43, 3);
            this.pathValueLabel.MaximumSize = new System.Drawing.Size(114, 15);
            this.pathValueLabel.MinimumSize = new System.Drawing.Size(114, 15);
            this.pathValueLabel.Name = "pathValueLabel";
            this.pathValueLabel.Size = new System.Drawing.Size(114, 15);
            this.pathValueLabel.TabIndex = 1;
            // 
            // loadDicomButton
            // 
            this.loadDicomButton.Location = new System.Drawing.Point(3, 19);
            this.loadDicomButton.Name = "loadDicomButton";
            this.loadDicomButton.Size = new System.Drawing.Size(157, 23);
            this.loadDicomButton.TabIndex = 0;
            this.loadDicomButton.Text = "Select DICOM file";
            this.loadDicomButton.UseVisualStyleBackColor = true;
            this.loadDicomButton.Click += new System.EventHandler(this.loadDicomButton_Click);
            // 
            // DicomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DicomForm";
            this.Text = "Dicom inspector";
            this.mainTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dicomPictureBox)).EndInit();
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximalValueTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimalValueTrackbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.PictureBox dicomPictureBox;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button loadDicomButton;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label pathValueLabel;
        private System.Windows.Forms.Panel breakPanel1;
        private System.Windows.Forms.Label maximalLabel;
        private System.Windows.Forms.Label minimalLabel;
        private System.Windows.Forms.TrackBar maximalValueTrackbar;
        private System.Windows.Forms.TrackBar minimalValueTrackbar;
        private System.Windows.Forms.Label maximalValueLabel;
        private System.Windows.Forms.Label minimalValueLabel;
        private System.Windows.Forms.Panel breakPanel2;
        private System.Windows.Forms.Button visualizeButton;
    }
}

