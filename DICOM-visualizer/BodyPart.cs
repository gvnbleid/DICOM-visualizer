﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Core;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.DXGI;
using SlimDX.Direct3D11;
using Buffer = SlimDX.Direct3D11.Buffer;
using Debug = System.Diagnostics.Debug;

namespace DICOM_visualizer
{
    using Core.Vertex;

    using Effect = SlimDX.Direct3D11.Effect;
    using Matrix = SlimDX.Matrix;
    using System.Globalization;
    using Dicom.Imaging.Mathematics;

    public class BodyPart : D3DApp
    {
        private Buffer _vb;
        private Buffer _ib;

        private Effect _fx;
        private EffectTechnique _tech;
        private EffectMatrixVariable _fxWVP;

        private InputLayout _inputLayout;

        private RasterizerState _wireframeRS;

        private readonly Matrix _skullWorld;

        private int _skullIndexCount;

        private Matrix _view;
        private Matrix _proj;

        private float _theta;
        private float _phi;
        private float _radius;

        private Point _lastMousePos;


        private bool _disposed;

        private List<Point3D[]> _trianglesList;
        public BodyPart(IntPtr hInstance, List<Point3D[]> trianglesList) : base(hInstance)
        {
            _trianglesList = trianglesList;


            _vb = null;
            _ib = null;
            _fx = null;
            _tech = null;
            _fxWVP = null;
            _inputLayout = null;
            _wireframeRS = null;
            _skullIndexCount = 0;
            _theta = 1.5f * MathF.PI;
            _phi = 0.1f * MathF.PI;
            _radius = 20.0f;

            MainWindowCaption = "Body Part";

            _lastMousePos = new Point(0, 0);

            _view = Matrix.Identity;
            _proj = Matrix.Identity;
            _skullWorld = Matrix.Translation(0.0f, -2.0f, 0.0f);

        }
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Util.ReleaseCom(ref _vb);
                    Util.ReleaseCom(ref _ib);
                    Util.ReleaseCom(ref _fx);
                    Util.ReleaseCom(ref _inputLayout);
                    Util.ReleaseCom(ref _wireframeRS);
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }
        public override bool Init()
        {
            if (!base.Init())
            {
                return false;
            }

            BuildGeometryBuffers();
            BuildFX();
            BuildVertexLayout();

            var wireFrameDesc = new RasterizerStateDescription
            {
                FillMode = FillMode.Wireframe,
                CullMode = CullMode.Back,
                IsFrontCounterclockwise = false,
                IsDepthClipEnabled = true
            };

            _wireframeRS = RasterizerState.FromDescription(Device, wireFrameDesc);

            return true;
        }
        public override void OnResize()
        {
            base.OnResize();
            _proj = Matrix.PerspectiveFovLH(0.25f * MathF.PI, AspectRatio, 1.0f, 1000.0f);
        }
        public override void UpdateScene(float dt)
        {
            base.UpdateScene(dt);

            // Get camera position from polar coords
            var x = _radius * MathF.Sin(_phi) * MathF.Cos(_theta);
            var z = _radius * MathF.Sin(_phi) * MathF.Sin(_theta);
            var y = _radius * MathF.Cos(_phi);

            // Build the view matrix
            var pos = new Vector3(x, y, z);
            var target = new Vector3(0);
            var up = new Vector3(0, 1, 0);
            _view = Matrix.LookAtLH(pos, target, up);
        }
        public override void DrawScene()
        {
            base.DrawScene();
            ImmediateContext.ClearRenderTargetView(RenderTargetView, Color.LightSteelBlue);
            ImmediateContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);

            ImmediateContext.InputAssembler.InputLayout = _inputLayout;
            ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            ImmediateContext.Rasterizer.State = _wireframeRS;

            ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_vb, VertexPC.Stride, 0));
            ImmediateContext.InputAssembler.SetIndexBuffer(_ib, Format.R32_UInt, 0);

            var wvp = _skullWorld * _view * _proj;

            _fxWVP.SetMatrix(wvp);

            for (var i = 0; i < _tech.Description.PassCount; i++)
            {
                _tech.GetPassByIndex(i).Apply(ImmediateContext);
                ImmediateContext.DrawIndexed(_skullIndexCount, 0, 0);
            }
            SwapChain.Present(0, PresentFlags.None);
        }

        protected override void OnMouseDown(object sender, MouseEventArgs mouseEventArgs)
        {
            _lastMousePos = mouseEventArgs.Location;
            Window.Capture = true;
        }
        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            Window.Capture = false;
        }
        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var dx = MathF.ToRadians(0.25f * (e.X - _lastMousePos.X));
                var dy = MathF.ToRadians(0.25f * (e.Y - _lastMousePos.Y));

                _theta += dx;
                _phi += dy;

                _phi = MathF.Clamp(_phi, 0.1f, MathF.PI - 0.1f);
            }
            else if (e.Button == MouseButtons.Right)
            {
                var dx = 0.05f * (e.X - _lastMousePos.X);
                var dy = 0.05f * (e.Y - _lastMousePos.Y);
                _radius += dx - dy;

                _radius = MathF.Clamp(_radius, 5.0f, 50.0f);
            }
            _lastMousePos = e.Location;
        }
        private void BuildGeometryBuffers()
        {
            try
            {
                var vertices = new List<VertexPC>();
                var indices = new List<int>();
                var vcount = _trianglesList.Count * 3;
                var tcount = _trianglesList.Count;


                var c = Color.Black;
                // skip ahead to the vertex data


                // Get the vertices  
                foreach (var triangle in _trianglesList)
                {
                    foreach (var vertex in triangle)
                    {
                        vertices.Add(new VertexPC(
                            new Vector3(
                                (float)vertex.X / 100,
                                (float)vertex.Y / 100,
                                (float)vertex.Z / 100),
                            c));
                        //System.Diagnostics.Debug.WriteLine($"Vertex: {vertex.X}, {vertex.Y}, {vertex.Z}");
                    }
                }
                // skip ahead to the index data
                // Get the indices
                _skullIndexCount = 3 * tcount;
                for (var i = 0; i < _skullIndexCount; i++)
                {                   
                    indices.Add(i);
                }
                

                var vbd = new BufferDescription(VertexPC.Stride * vcount, ResourceUsage.Immutable,
                    BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                _vb = new Buffer(Device, new DataStream(vertices.ToArray(), false, false), vbd);

                var ibd = new BufferDescription(sizeof(int) * _skullIndexCount, ResourceUsage.Immutable,
                    BindFlags.IndexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                _ib = new Buffer(Device, new DataStream(indices.ToArray(), false, false), ibd);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BuildFX()
        {
            ShaderBytecode compiledShader = null;
            try
            {
                compiledShader = new ShaderBytecode(new DataStream(File.ReadAllBytes("fx/color.fxo"), false, false));
                _fx = new Effect(Device, compiledShader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Util.ReleaseCom(ref compiledShader);
            }

            _tech = _fx.GetTechniqueByName("ColorTech");
            _fxWVP = _fx.GetVariableByName("gWorldViewProj").AsMatrix();
        }
        private void BuildVertexLayout()
        {
            var vertexDesc = new[] {
                new InputElement("POSITION", 0, Format.R32G32B32_Float,
                    0, 0, InputClassification.PerVertexData, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float,
                    12, 0, InputClassification.PerVertexData, 0)
            };
            Debug.Assert(_tech != null);
            var passDesc = _tech.GetPassByIndex(0).Description;
            _inputLayout = new InputLayout(Device, passDesc.Signature, vertexDesc);
        }

    }
}
