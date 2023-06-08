// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x1;
    private const int SPIF_SENDCHANGE = 0x2;

    static void Main(string[] args)
    {
        // Create a black bitmap
        var blackBitmap = new Bitmap(1920, 1080); // Adjust the resolution as per your screen size
        var graphics = Graphics.FromImage(blackBitmap);
        graphics.Clear(Color.Black);

        // Save the black bitmap to a file
        var tempPath = Path.Combine(Path.GetTempPath(), "black.bmp");
        blackBitmap.Save(tempPath);

        // Set the black bitmap as the wallpaper
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        Console.WriteLine($"Wallpaper set to {tempPath}.");
    }
}