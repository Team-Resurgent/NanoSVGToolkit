using NanoSVGToolkit.Internal;
using System.Drawing;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace NanoSVGToolkit
{
    public static class NanoSVG
    {
        public unsafe static byte[] RenderSVGToRawPixels(byte[] svg_data, ushort width, ushort height)
        {
            var result = new byte[width * height * 4];
            fixed (byte* svg_data_array = svg_data)
            { 
                fixed (byte* result_array = result)
                {
                    NanoSVGAPI.RenderSVG(svg_data_array, result_array, width, height);
                }
            }
            return result;
        }

        public static byte[] ConvertRawPixelsToBitmap(byte[] rawRgbData, ushort width, ushort height)
        {
            int pitch = width * 4; 
            int imageSize = pitch * height;
            int fileSize = 54 + imageSize;

            using (var ms = new MemoryStream(fileSize))
            using (var writer = new BinaryWriter(ms))
            {
                writer.Write((byte)'B');
                writer.Write((byte)'M');
                writer.Write(fileSize);
                writer.Write((int)0);      
                writer.Write(54);        

                writer.Write(40);          
                writer.Write((int)width);
                writer.Write((int)-height);
                writer.Write((short)1);
                writer.Write((short)32); 
                writer.Write(0); 
                writer.Write(imageSize);
                writer.Write(0);         
                writer.Write(0);        
                writer.Write(0); 
                writer.Write(0);     

                for (int i = 0; i < rawRgbData.Length; i += 4)
                {
                    byte r = rawRgbData[i];
                    byte g = rawRgbData[i + 1];
                    byte b = rawRgbData[i + 2];
                    byte x = rawRgbData[i + 3];

                    writer.Write(b);
                    writer.Write(g);
                    writer.Write(r);
                    writer.Write(x); // Keep X/padding as-is
                }

                return ms.ToArray();
            }
        }
    }
}
