using Dicom.Imaging.Mathematics;
using Dicom.Imaging.Render;
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
        private (Point3D point3D, double value) vertex7;
        private (Point3D point3D, double value) vertex0;
        private (Point3D point3D, double value) vertex1;
        private (Point3D point3D, double value) vertex2;
        private (Point3D point3D, double value) vertex3;
        private (Point3D point3D, double value) vertex4;
        private (Point3D point3D, double value) vertex5;
        private (Point3D point3D, double value) vertex6;

        public (Point3D point3D, double value) Vertex0 { get => vertex0; set => vertex0 = value; }
        public (Point3D point3D, double value) Vertex1 { get => vertex1; set => vertex1 = value; }
        public (Point3D point3D, double value) Vertex2 { get => vertex2; set => vertex2 = value; }
        public (Point3D point3D, double value) Vertex3 { get => vertex3; set => vertex3 = value; }
        public (Point3D point3D, double value) Vertex4 { get => vertex4; set => vertex4 = value; }
        public (Point3D point3D, double value) Vertex5 { get => vertex5; set => vertex5 = value; }
        public (Point3D point3D, double value) Vertex6 { get => vertex6; set => vertex6 = value; }
        public (Point3D point3D, double value) Vertex7 { get => vertex7; set => vertex7 = value; }

        public GridCell(IPixelData backSlice, IPixelData frontSlice, int rowIndex, int columnIndex, int sliceIndex)
        {
            int xSpacing = backSlice.Width / 100;
            int ySpacing = backSlice.Height / 100;
            int xLeft = columnIndex * xSpacing - backSlice.Width/2;
            int xRight = (columnIndex + 1) * xSpacing - backSlice.Width / 2;
            double yBottom = sliceIndex / 113.0 * (552 - 283) + 253 - (552 - 283) / 2;
            double yTop = (sliceIndex + 1) / 113.0 * (552 - 283) + 253 - (552 - 283) / 2;
            int zBack = rowIndex * ySpacing - backSlice.Height / 2;
            int zFront = (rowIndex + 1) * ySpacing - backSlice.Height / 2;

            vertex0 = (new Point3D(xLeft, yBottom, zBack), backSlice.GetPixel(xLeft + backSlice.Width/2, zBack + backSlice.Height / 2));
            vertex1 = (new Point3D(xRight, yBottom, zBack), backSlice.GetPixel(xRight + backSlice.Width / 2, zBack + backSlice.Height / 2));
            vertex2 = (new Point3D(xRight, yBottom, zFront), backSlice.GetPixel(xRight + backSlice.Width / 2, zFront + backSlice.Height / 2));
            vertex3 = (new Point3D(xLeft, yBottom, zFront),backSlice.GetPixel(xLeft + backSlice.Width / 2, zFront + backSlice.Height / 2));
            vertex4 = (new Point3D(xLeft, yTop, zBack), frontSlice.GetPixel(xLeft + backSlice.Width / 2, zBack + backSlice.Height / 2));
            vertex5 = (new Point3D(xRight, yTop, zBack), frontSlice.GetPixel(xRight + backSlice.Width / 2, zBack + backSlice.Height / 2));
            vertex6 = (new Point3D(xRight, yTop, zFront), frontSlice.GetPixel(xRight + backSlice.Width / 2, zFront + backSlice.Height / 2));
            vertex7 = (new Point3D(xLeft, yTop, zFront), frontSlice.GetPixel(xLeft + backSlice.Width / 2, zFront + backSlice.Height / 2));
        }

    }
}
