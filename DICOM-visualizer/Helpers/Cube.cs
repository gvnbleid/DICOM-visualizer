using Dicom.Imaging.Mathematics;
using Dicom.Imaging.Render;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOM_visualizer.Helpers
{
    public class GridCell
    {
        private (Vector3 point3D, double value) vertex7;
        private (Vector3 point3D, double value) vertex0;
        private (Vector3 point3D, double value) vertex1;
        private (Vector3 point3D, double value) vertex2;
        private (Vector3 point3D, double value) vertex3;
        private (Vector3 point3D, double value) vertex4;
        private (Vector3 point3D, double value) vertex5;
        private (Vector3 point3D, double value) vertex6;

        public (Vector3 point3D, double value) Vertex0 { get => vertex0; set => vertex0 = value; }
        public (Vector3 point3D, double value) Vertex1 { get => vertex1; set => vertex1 = value; }
        public (Vector3 point3D, double value) Vertex2 { get => vertex2; set => vertex2 = value; }
        public (Vector3 point3D, double value) Vertex3 { get => vertex3; set => vertex3 = value; }
        public (Vector3 point3D, double value) Vertex4 { get => vertex4; set => vertex4 = value; }
        public (Vector3 point3D, double value) Vertex5 { get => vertex5; set => vertex5 = value; }
        public (Vector3 point3D, double value) Vertex6 { get => vertex6; set => vertex6 = value; }
        public (Vector3 point3D, double value) Vertex7 { get => vertex7; set => vertex7 = value; }

        public GridCell(IPixelData backSlice, IPixelData frontSlice, int rowIndex, int columnIndex, int sliceIndex)
        {
            float xSpacing = backSlice.Width / 100;
            float ySpacing = backSlice.Height / 100;
            float xLeft = columnIndex * xSpacing - backSlice.Width/2;
            float xRight = (columnIndex + 1) * xSpacing - backSlice.Width / 2;
            float yBottom = sliceIndex / 113.0f * (552 - 283) + 253 - (552 - 283) / 2;
            float yTop = (sliceIndex + 1) / 113.0f * (552 - 283) + 253 - (552 - 283) / 2;
            float zBack = rowIndex * ySpacing - backSlice.Height / 2;
            float zFront = (rowIndex + 1) * ySpacing - backSlice.Height / 2;

            vertex0 = (new Vector3(xLeft, yBottom, zBack), backSlice.GetPixel((int)(xLeft + backSlice.Width/2), (int)(zBack + backSlice.Height / 2)));
            vertex1 = (new Vector3(xRight, yBottom, zBack), backSlice.GetPixel((int)(xRight + backSlice.Width / 2), (int)(zBack + backSlice.Height / 2)));
            vertex2 = (new Vector3(xRight, yBottom, zFront), backSlice.GetPixel((int)(xRight + backSlice.Width / 2), (int)(zFront + backSlice.Height / 2)));
            vertex3 = (new Vector3(xLeft, yBottom, zFront),backSlice.GetPixel((int)(xLeft + backSlice.Width / 2), (int)(zFront + backSlice.Height / 2)));
            vertex4 = (new Vector3(xLeft, yTop, zBack), frontSlice.GetPixel((int)(xLeft + backSlice.Width / 2), (int)(zBack + backSlice.Height / 2)));
            vertex5 = (new Vector3(xRight, yTop, zBack), frontSlice.GetPixel((int)(xRight + backSlice.Width / 2), (int)(zBack + backSlice.Height / 2)));
            vertex6 = (new Vector3(xRight, yTop, zFront), frontSlice.GetPixel((int)(xRight + backSlice.Width / 2), (int)(zFront + backSlice.Height / 2)));
            vertex7 = (new Vector3(xLeft, yTop, zFront), frontSlice.GetPixel((int)(xLeft + backSlice.Width / 2), (int)(zFront + backSlice.Height / 2)));
        }

    }
}
