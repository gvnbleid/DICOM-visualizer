using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOM_visualizer.Helpers
{
    public class Slice
    {
        private int _rowCount;
        private int _columnCount;
        private DicomFile _dicomFile;
        private int[,] hounsfieldBuffer;
        private Bitmap _image;

        public int RowCount => _rowCount;
        public int ColumnCount => _columnCount;

        public Slice(DicomFile dicomFile)
        {
            _dicomFile = dicomFile;
        }

        public double SliceLocation => _dicomFile.Dataset.Get<double>(DicomTag.SliceLocation);

        public Bitmap RenderImage()
        {
            if (_image != null) return _image;
            DicomImage dicomImage = new DicomImage(_dicomFile.Dataset);
            _image = dicomImage.RenderImage().AsBitmap();
            return _image;
        }

        public int GetHounsfieldPixelValue(int rowIndex, int columnIndex)
        {
            if (hounsfieldBuffer != null)
                return hounsfieldBuffer[rowIndex, columnIndex];

            int rescaleIntercept = _dicomFile.Dataset.Get<int>(DicomTag.RescaleIntercept);
            int rescaleSlope = _dicomFile.Dataset.Get<int>(DicomTag.RescaleSlope);
            bool pixelPaddingValuePresent = _dicomFile.Dataset.Contains(DicomTag.PixelPaddingValue);
            int pixelPaddingValue = 0;
            if(pixelPaddingValuePresent)
                 pixelPaddingValue = _dicomFile.Dataset.Get<int>(DicomTag.PixelPaddingValue);

            var pixelData = PixelDataFactory.Create(DicomPixelData.Create(_dicomFile.Dataset), 0);
            _rowCount = pixelData.Width;
            _columnCount = pixelData.Height;

            hounsfieldBuffer = new int[_rowCount, _columnCount];

            for (int row = 0; row < _rowCount; row++)
                for (int col = 0; col < _columnCount; col++)
                {
                    int pixelValue = (int)pixelData.GetPixel(col, row);
                    if (pixelPaddingValuePresent)
                        if (pixelValue == pixelPaddingValue)
                            pixelValue = Int16.MinValue;

                    pixelValue = pixelValue * rescaleSlope + rescaleIntercept;
                    hounsfieldBuffer[row, col] = pixelValue;
                }
            return hounsfieldBuffer[rowIndex, columnIndex];
        }
    }
}
 
