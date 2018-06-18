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

        public GridCell(Slice backSlice, Slice frontSlice, int rowIndex, int columnIndex, int sliceIndex)
        {
            float xSpacing = backSlice.ColumnCount / 113f;
            float ySpacing = backSlice.RowCount / 113f;
            float zSpacing = 249 / 113f;
            float xLeft = columnIndex * xSpacing;
            float xLeftTransposed = xLeft - backSlice.ColumnCount / 2f;
            float xRight = (columnIndex + 1) * xSpacing;
            float xRightTransposed = xRight - backSlice.ColumnCount / 2f;
            float yBottom = sliceIndex * zSpacing;
            float yBottomTransposed = yBottom - 249 / 2f;
            float yTop = (sliceIndex + 1) * zSpacing; /// 113.0f * (552 - 283) + 253 - (552 - 283) / 2;
            float yTopTransposed = yTop - 249 / 2f;
            float zBack = rowIndex * ySpacing;
            float zBackTransposed = zBack - backSlice.RowCount / 2f;
            float zFront = (rowIndex + 1) * ySpacing;
            float zFrontTransposed = zFront - backSlice.RowCount / 2f;

            vertex0 = (new Vector3(xLeftTransposed, yBottomTransposed, zBackTransposed), backSlice.GetHounsfieldPixelValue((int)(xLeft), (int)(zBack)));
            vertex1 = (new Vector3(xRightTransposed, yBottomTransposed, zBackTransposed), backSlice.GetHounsfieldPixelValue((int)(xRight), (int)(zBack)));
            vertex2 = (new Vector3(xRightTransposed, yBottomTransposed, zFrontTransposed), backSlice.GetHounsfieldPixelValue((int)(xRight), (int)(zFront)));
            vertex3 = (new Vector3(xLeftTransposed, yBottomTransposed, zFrontTransposed),backSlice.GetHounsfieldPixelValue((int)(xLeft), (int)(zFront)));
            vertex4 = (new Vector3(xLeftTransposed, yTopTransposed, zBackTransposed), frontSlice.GetHounsfieldPixelValue((int)(xLeft), (int)(zBack)));
            vertex5 = (new Vector3(xRightTransposed, yTopTransposed, zBackTransposed), frontSlice.GetHounsfieldPixelValue((int)(xRight), (int)(zBack)));
            vertex6 = (new Vector3(xRightTransposed, yTopTransposed, zFrontTransposed), frontSlice.GetHounsfieldPixelValue((int)(xRight), (int)(zFront)));
            vertex7 = (new Vector3(xLeftTransposed, yTopTransposed, zFrontTransposed), frontSlice.GetHounsfieldPixelValue((int)(xLeft), (int)(zFront)));
        }


        

    }
}
