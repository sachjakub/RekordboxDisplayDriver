using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class Optimizer
{
    // Method to convert bitmap to 1 byte per pixel (8-bit grayscale)
    public Bitmap ConvertTo1BytePerPixel(Bitmap original)
    {
        Bitmap newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format8bppIndexed);

        // Set the palette to grayscale
        ColorPalette palette = newBitmap.Palette;
        for (int i = 0; i < 256; i++)
        {
            palette.Entries[i] = Color.FromArgb(i, i, i);
        }
        newBitmap.Palette = palette;

        // Copy pixel data
        BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, original.PixelFormat);
        BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

        int originalStride = originalData.Stride;
        int newStride = newData.Stride;
        IntPtr originalScan0 = originalData.Scan0;
        IntPtr newScan0 = newData.Scan0;

        unsafe
        {
            byte* originalPtr = (byte*)originalScan0.ToPointer();
            byte* newPtr = (byte*)newScan0.ToPointer();

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = Color.FromArgb(originalPtr[y * originalStride + x * 4 + 2], originalPtr[y * originalStride + x * 4 + 1], originalPtr[y * originalStride + x * 4]);
                    byte grayValue = (byte)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                    newPtr[y * newStride + x] = grayValue;
                }
            }
        }

        original.UnlockBits(originalData);
        newBitmap.UnlockBits(newData);

        return newBitmap;
    }

    // Method to change resolution to half
    public Bitmap ChangeResolutionToHalf(Bitmap original)
    {
        int newWidth = original.Width / 2;
        int newHeight = original.Height / 2;
        Bitmap newBitmap = new Bitmap(newWidth, newHeight);

        using (Graphics g = Graphics.FromImage(newBitmap))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, 0, 0, newWidth, newHeight);
        }

        return newBitmap;
    }

    // Method to use PNG compression on bitmap
    public Bitmap CompressToPng(Bitmap original)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            original.Save(memoryStream, ImageFormat.Png);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new Bitmap(memoryStream);
        }
    }
}
