using System;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;

namespace RekordboxDisplayDriver.Entities
{
    public class Transmiter
    {
        private SerialPort _serialPort = null!;


        public Transmiter()
        {
            
        }

        public List<string> GetAvailablePorts()
        {
            return new List<string>(SerialPort.GetPortNames());
        }

        public void SetTransmiter(string portName, int baudRate)
        {
            Close();
            _serialPort = new SerialPort(portName, baudRate);
        }

        public void Open()
        {
            if (_serialPort == null)
                throw new Exception("Serial port is not set.");

            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error opening serial port: {ex.Message}");
            }
        }

        public void Close()
        {
            if (_serialPort == null)
                return;

            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        public void SendBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                return;

            if (_serialPort.IsOpen)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        byte[] bitmapBytes = ms.ToArray();

                        //_serialPort.Write(bitmapBytes, 0, bitmapBytes.Length);
                        _serialPort.Write(new byte[] { 0x00 }, 0, 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while sending bitmap: {ex.Message}");
                }
            }
            else
            {
                throw new Exception("Serial port is not open.");
            }
        }
    }




}
