using SlimDX.DXGI;
using SlimDX.Direct3D11;
using SlimDX.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DICOM_visualizer
{
    public partial class RenderForm : Form
    {
        private SlimDX.Direct3D11.Device _device;
        private SwapChain _swapChain;
        public RenderForm()
        {
            InitializeComponent();            
        }
    }
}
