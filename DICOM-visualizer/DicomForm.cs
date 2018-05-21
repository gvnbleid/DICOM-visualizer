using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dicom;

namespace DICOM_visualizer
{
    public partial class DicomForm : Form
    {
        public DicomForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void minimalValueTrackbar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            trackBar.Value = Math.Min(trackBar.Value, maximalValueTrackbar.Value);
            minimalValueLabel.Text = trackBar.Value.ToString();
        }

        private void maximalValueTrackbar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            trackBar.Value = Math.Max(trackBar.Value, minimalValueTrackbar.Value);
            maximalValueLabel.Text = trackBar.Value.ToString();
        }

        private async void loadDicomButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DICOM files | *.dcm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DicomFile dicomBitmap = await Dicom.DicomFile.OpenAsync(openFileDialog.FileName);
                }
            }
                
        }
    }
}
