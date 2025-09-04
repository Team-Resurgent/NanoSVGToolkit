using System.Runtime.InteropServices;

namespace NanoSVGToolkit.Internal
{
    internal unsafe static class NanoSVGAPI
    {
        [DllImport("libNanoSVG", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int RenderSVG(byte* svg_data, byte* image_data, ushort width, ushort height, ushort scale);
    }
}
