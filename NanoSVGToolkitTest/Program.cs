namespace NanoSVGToolkitTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var background = (uint)0;
            var scale = (ushort)2;
            var svg_data = File.ReadAllBytes("test.svg");
            var image_data = NanoSVGToolkit.NanoSVG.RenderSVGToRawPixels(svg_data, background, 640, 480, 3);
            var bitmap = NanoSVGToolkit.NanoSVG.ConvertRawPixelsToBitmap(image_data, 640, 480);
            File.WriteAllBytes(@"test.bmp", bitmap);

            Console.WriteLine("Test OK");
        }
    }
}
