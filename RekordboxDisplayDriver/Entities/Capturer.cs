using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RekordboxDisplayDriver.Entities
{
    public class Capturer
    {
        private int _startHeight;
        private int _captureHeight;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight,
            IntPtr hdcSrc, int nXSrc, int nYSrc,
            CopyPixelOperation rop);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public Capturer(int startHeight, int captureHeight)
        {
            _startHeight = startHeight;
            _captureHeight = captureHeight;
        }

        public Bitmap Capture()
        {
            IntPtr windowHandle = FindWindow(null, "rekordbox");

            if (windowHandle == IntPtr.Zero)
            {
                throw new Exception("Please start rekordbox");
            }

            if (!GetWindowRect(windowHandle, out RECT rect))
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }

            int windowWidth = rect.Right - rect.Left;
            int windowHeight = rect.Bottom - rect.Top;

            // Ensure the requested rectangle is within the window bounds
            int captureHeight = _captureHeight;
            if (_startHeight + _captureHeight > windowHeight)
            {
                captureHeight = windowHeight - _startHeight;
            }

            IntPtr hWndDC = GetWindowDC(windowHandle);
            if (hWndDC == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }

            Bitmap bitmap = new Bitmap(windowWidth, captureHeight, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                IntPtr hDC = graphics.GetHdc();

                // Copy from (0, _startHeight) in source to (0, 0) in destination
                if (!BitBlt(
                    hDC, 0, 0,
                    windowWidth, captureHeight,
                    hWndDC, 0, _startHeight,
                    CopyPixelOperation.SourceCopy))
                {
                    graphics.ReleaseHdc(hDC);
                    ReleaseDC(windowHandle, hWndDC);
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }
                graphics.ReleaseHdc(hDC);
            }

            ReleaseDC(windowHandle, hWndDC);
            return bitmap;
        }

        public void ChangeCaptureBoundaries(int start, int height)
        {
            _startHeight = start;
            _captureHeight = height;
        }
    }
}
