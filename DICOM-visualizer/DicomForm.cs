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
using Dicom;
using Dicom.Imaging;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;

namespace DICOM_visualizer
{
    public partial class DicomForm : Form
    {
        private List<DicomImage> _slices;

        public DicomForm()
        {
            InitializeComponent();
            _slices = new List<DicomImage>();
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

        private void loadDicomButton_Click(object sender, EventArgs e)
        {
            string[] files = null;

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && 
                    !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    try
                    {
                        files = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                        System.Diagnostics.Debug.WriteLine("Files found: " + files.Length.ToString());
                    }
                    catch (Exception ex)
                    {
                        ShowErrorWindow(ex.Message, "Error opening DICOM folder");
                    }                    
                }

                if (result == DialogResult.Cancel || result == DialogResult.Abort)
                    return;

                if (files.Length == 0)
                {
                    ShowErrorWindow("Selected directory is empty", "Error: empty directory");
                }

                try
                {
                    //assuming DICOM files in folder are in order
                    foreach (string fileName in files)
                    {
                        var file = DicomFile.Open(fileName);
                        var image = new DicomImage(file.Dataset);
                        //decimal position = file.Dataset.Get<decimal>(DicomTag.ZEffective);
                        //Debug.WriteLine($"Add item to directory, key: {position}, value: {image.ToString()}");
                        _slices.Add(image);
                    }
                }
                catch(Exception ex)
                {
                    ShowErrorWindow(ex.Message, "Error opening DICOM file");
                }
                

                System.Diagnostics.Debug.WriteLine("Files added to dictionary: " + _slices.Count);
            }                
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
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += (o, args) =>
            {
                var form = new SlimDX.Windows.RenderForm("Visualization");
                SlimDX.Direct3D11.Device device;
                SwapChain swapChain;
                ShaderSignature inputSignature;
                VertexShader vertexShader;
                PixelShader pixelShader;

                var description = new SwapChainDescription()
                {
                    BufferCount = 2,
                    Usage = Usage.RenderTargetOutput,
                    OutputHandle = form.Handle,
                    IsWindowed = true,
                    ModeDescription = new ModeDescription(0, 0, new SlimDX.Rational(60, 1), Format.R8G8B8A8_UNorm),
                    SampleDescription = new SampleDescription(1, 0),
                    Flags = SwapChainFlags.AllowModeSwitch,
                    SwapEffect = SwapEffect.Discard
                };

                SlimDX.Direct3D11.Device.CreateWithSwapChain(
                    SlimDX.Direct3D11.DriverType.Hardware,
                    DeviceCreationFlags.Debug,
                    description,
                    out device,
                    out swapChain);

                using (var factory = swapChain.GetParent<Factory>())
                    factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAltEnter);

                form.KeyDown += (_, ev) =>
                {
                    if (ev.Alt && ev.KeyCode == Keys.Enter)
                        swapChain.IsFullScreen = !swapChain.IsFullScreen;
                };

                var context = device.ImmediateContext;

                var viewport = new Viewport(0.0f, 0.0f, form.ClientSize.Width, form.ClientSize.Height);

                RenderTargetView renderTarget;
                using (var resource = SlimDX.Direct3D11.Resource.FromSwapChain<Texture2D>(swapChain, 0))
                    renderTarget = new RenderTargetView(device, resource);

                context.Rasterizer.SetViewports(viewport);
                context.OutputMerger.SetTargets(renderTarget);

                using (var bytecode = ShaderBytecode.CompileFromFile("C:\\Users\\gvnbleid\\Source\\Repos\\DICOM-visualizer\\DICOM-visualizer\\triangle.fx", "VShader", "vs_4_0", ShaderFlags.None, EffectFlags.None))
                {
                    inputSignature = ShaderSignature.GetInputSignature(bytecode);
                    vertexShader = new VertexShader(device, bytecode);
                }

                using (var bytecode = ShaderBytecode.CompileFromFile("C:\\Users\\gvnbleid\\Source\\Repos\\DICOM-visualizer\\DICOM-visualizer\\triangle.fx", "PShader", "ps_4_0", ShaderFlags.None, EffectFlags.None))
                    pixelShader = new PixelShader(device, bytecode);

                var vertices = new DataStream(12 * 3, true, true);
                vertices.Write(new Vector3(0.0f, 0.5f, 0.5f));
                vertices.Write(new Vector3(0.5f, -0.5f, 0.5f));
                vertices.Write(new Vector3(-0.5f, -0.5f, 0.5f));
                vertices.Position = 0;

                var elements = new[] { new InputElement("POSITION", 0, Format.R32G32B32_Float, 0) };
                var layout = new InputLayout(device, inputSignature, elements);
                var vertexBuffer = new SlimDX.Direct3D11.Buffer(device, vertices, 12 * 3, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

                context.InputAssembler.InputLayout = layout;
                context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 12, 0));

                context.VertexShader.Set(vertexShader);
                context.PixelShader.Set(pixelShader);

                MessagePump.Run(form, () =>
                {
                    context.ClearRenderTargetView(renderTarget, new SlimDX.Color4(.5f, .5f, 1.0f));

                    context.Draw(3, 0);
                    swapChain.Present(0, PresentFlags.None);
                });

                form.FormClosing += (_, ev) =>
                {
                    vertices.Close();
                    vertexBuffer.Dispose();
                    layout.Dispose();
                    inputSignature.Dispose();
                    vertexShader.Dispose();
                    pixelShader.Dispose();
                    renderTarget.Dispose();
                    swapChain.Dispose();
                    device.Dispose();
                };
            };

            backgroundWorker.RunWorkerCompleted += (ob, args) =>
            {
                MessageBox.Show("Finiszed");
            };

            backgroundWorker.RunWorkerAsync();
            
        }

        //Device.Crea
    }
}
