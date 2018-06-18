using Dicom.Imaging;
using Dicom.Imaging.Mathematics;
using Dicom.Imaging.Render;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOM_visualizer.Helpers
{
    public static class MarchingCubes
    {
        public static void TryAlgorithm(List<Slice> slices, double minIsoLevel, double maxIsoLevel, out List<Vector3[]> triangleList)
        {
            triangleList = new List<Vector3[]>();
            List<GridCell> gridCells = new List<GridCell>();
            for (int i = 0; i < slices.Count - 1; i++)
                ConvertToGridCells(slices[i], slices[i + 1], i, ref gridCells);
            System.Diagnostics.Debug.WriteLine("Number of grid cells: " + gridCells.Count);
            foreach (var cell in gridCells)
                Polygonise(cell, minIsoLevel, maxIsoLevel, ref triangleList);
        }

        private static void ConvertToGridCells(Slice backSlice, Slice frontSlice, int sliceIndex, ref List<GridCell> gridCells)
        {
            List<GridCell> cells = new List<GridCell>();
            for (int row = 0; row < 100; row++)
                for (int column = 0; column < 100; column++)
                    gridCells.Add(new GridCell(backSlice, frontSlice, row, column, sliceIndex));
        }

        private static void Polygonise(GridCell grid, double minIsoLevel, double maxIsoLevel, ref List<Vector3[]> theTriangleList)
        {
            int cubeIndex = 0;
            if (grid.Vertex0.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 1;
            if (grid.Vertex1.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 2;
            if (grid.Vertex2.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 4;
            if (grid.Vertex3.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 8;
            if (grid.Vertex4.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 16;
            if (grid.Vertex5.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 32;
            if (grid.Vertex6.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 64;
            if (grid.Vertex7.value >= minIsoLevel && grid.Vertex0.value <= maxIsoLevel)
                cubeIndex |= 128;

            if (EdgeTable.LookupTable[cubeIndex] == 0)
                return;

            Vector3[] verticesList = new Vector3[12];

            if ((EdgeTable.LookupTable[cubeIndex] & 1) > 0)
                verticesList[0] = VertexInterpolation(grid.Vertex0.point3D, grid.Vertex1.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 2) > 0)
                verticesList[1] = VertexInterpolation(grid.Vertex1.point3D, grid.Vertex2.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 4) > 0)
                verticesList[2] = VertexInterpolation(grid.Vertex2.point3D, grid.Vertex3.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 8) > 0)
                verticesList[3] = VertexInterpolation(grid.Vertex3.point3D, grid.Vertex0.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 16) > 0)
                verticesList[4] = VertexInterpolation(grid.Vertex4.point3D, grid.Vertex5.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 32) > 0)
                verticesList[5] = VertexInterpolation(grid.Vertex5.point3D, grid.Vertex6.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 64) > 0)
                verticesList[6] = VertexInterpolation(grid.Vertex6.point3D, grid.Vertex7.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 128) > 0)
                verticesList[7] = VertexInterpolation(grid.Vertex7.point3D, grid.Vertex4.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 256) > 0)
                verticesList[8] = VertexInterpolation(grid.Vertex0.point3D, grid.Vertex4.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 512) > 0)
                verticesList[9] = VertexInterpolation(grid.Vertex1.point3D, grid.Vertex5.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 1024) > 0)
                verticesList[10] = VertexInterpolation(grid.Vertex2.point3D, grid.Vertex6.point3D);
            if ((EdgeTable.LookupTable[cubeIndex] & 2048) > 0)
                verticesList[11] = VertexInterpolation(grid.Vertex3.point3D, grid.Vertex7.point3D);

            for(int i=0;TriTable.LookupTable[cubeIndex,i] != -1; i+=3)
            {
                Vector3[] triangle = new Vector3[3];
                triangle[0] = verticesList[TriTable.LookupTable[cubeIndex, i]];
                triangle[1] = verticesList[TriTable.LookupTable[cubeIndex, i + 1]];
                triangle[2] = verticesList[TriTable.LookupTable[cubeIndex, i + 2]];

                theTriangleList.Add(triangle);
            }
        }

        private static Vector3 VertexInterpolation(Vector3 p1, Vector3 p2)
        {
            float x = (p1.X + p2.X) / 2f;
            float y = (p1.Y + p2.Y) / 2f;
            float z = (p1.Z + p2.Z) / 2f;
            return new Vector3(x, y, z);
        }
    }
}
