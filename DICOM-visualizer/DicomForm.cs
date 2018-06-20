using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Vertex;
using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.Mathematics;
using Dicom.Imaging.Render;
using DICOM_visualizer.Helpers;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;

namespace DICOM_visualizer
{
    public partial class DicomForm : Form
    {
        private List<Slice> _slices;
        private List<VertexPC[]> _triangles;
        private int _index = 0;
        private int _minVal = 0, _maxVal = 255;

        public DicomForm()
        {
            InitializeComponent();
            _slices = new List<Slice>();
        }
       
        private void minimalValueTrackbar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            trackBar.Value = Math.Min(trackBar.Value, maximalValueTrackbar.Value);
            minimalValueLabel.Text = trackBar.Value.ToString();
            _minVal = trackBar.Value;
        }

        private void maximalValueTrackbar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            trackBar.Value = Math.Max(trackBar.Value, minimalValueTrackbar.Value);
            maximalValueLabel.Text = trackBar.Value.ToString();
            _maxVal = trackBar.Value;
        }

        private void loadDicomButton_Click(object sender, EventArgs e)
        {
            string[] files = null;
            string selectedPath = pathValueLabel.Text;
            _slices.Clear();
            if (selectedPath == String.Empty)
            {
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();

                    if (result == DialogResult.OK &&
                        !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                        selectedPath = folderBrowserDialog.SelectedPath;
                    if (result == DialogResult.Cancel || result == DialogResult.Abort)
                        return;
                }
            }
            try
            {
                files = Directory.GetFiles(selectedPath);
                System.Diagnostics.Debug.WriteLine("Files found: " + files.Length.ToString());
            }
            catch (Exception ex)
            {
                ShowErrorWindow(ex.Message, "Error opening DICOM folder");
            }
            pathValueLabel.Text = selectedPath;


            if (files.Length == 0)
            {
                ShowErrorWindow("Selected directory is empty", "Error: empty directory");
                return;
            }

            try
            {
                foreach (string fileName in files)
                {
                    var file = DicomFile.Open(fileName);
                    System.Diagnostics.Debug.WriteLine("spacing: " + file.Dataset.Get<string>(DicomTag.ImagePositionPatient));
                    _slices.Add(new Slice(file));
                }
            }
            catch (Exception ex)
            {
                ShowErrorWindow(ex.Message, "Error opening DICOM file");
                return;
            }
            _slices = _slices.OrderBy(slice => slice.SliceLocation).ToList();
            System.Diagnostics.Debug.WriteLine("Files added to dictionary: " + _slices.Count);
            dicomPictureBox.Image = _slices[0].RenderImage();
            nextButton.Enabled = true;

        }

        private void ShowErrorWindow(string text, string caption)
        {
            MessageBox.Show(
                        this,
                        text,
                        caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void visualizeButton_Click(object sender, EventArgs e)
        {
            MarchingCubes.TryAlgorithm(_slices, _minVal, _maxVal, out _triangles);
            Configuration.EnableObjectTracking = true;
            var app = new BodyPart.BodyPart(Process.GetCurrentProcess().Handle);
            app.Triangles = _triangles;
            if (!app.Init())
            {
                return;
            }
            app.Run();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            dicomPictureBox.Image = _slices[--_index].RenderImage();
            if (_index == 0) previousButton.Enabled = false;
            nextButton.Enabled = true;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            dicomPictureBox.Image = _slices[++_index].RenderImage();
            if (_index == _slices.Count) nextButton.Enabled = false;
            previousButton.Enabled = true;
        }
    }
}
