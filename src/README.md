# setwall

setwall is a simple C# utility designed to change the current user's wallpaper to a solid black color. This is 
especially useful in situations where you need to quickly set a minimalist, non-distracting desktop environment.
Or when your company forces that ugly corporate wallpaper on your laptop.

## How It Works

When run, setwall creates a black bitmap that matches the size of your screen resolution. This bitmap is then 
saved to a temporary file and immediately set as the current user's wallpaper. 

## Usage

Only for Windows.

Simply run the executable file, and your wallpaper will be changed to a solid black color. It may not reflect
immediately after running, because of caches and other Windows shenanigans. The call to the OS to change
the wallpaper asks for a refresh, however, on my machine I often have to run it twice. If you run it once, log 
out and then log back on, the wallpaper will be black.

Also, this code might require administrator privileges to run successfully, 
and won't work with certain versions of Windows 10 that don't support changing the wallpaper 
programmatically, like Windows 10 S.

*Always be careful and considerate when changing system parameters programmatically.*

## Code

Here's a basic look at what the code does:

```csharp
using System;
using System.Runtime.InteropServices;
using System.Drawing;

class Program
{
    // Signatures for unmanaged calls
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    const int SPI_SETDESKWALLPAPER = 20;
    const int SPIF_UPDATEINIFILE = 0x1;
    const int SPIF_SENDCHANGE = 0x2;

    static void Main(string[] args)
    {
        // Create a black bitmap
        Bitmap blackBitmap = new Bitmap(1920, 1080); // Adjust the resolution as per your screen size
        Graphics graphics = Graphics.FromImage(blackBitmap);
        graphics.Clear(Color.Black);

        // Save the black bitmap to a file
        string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "black.bmp");
        blackBitmap.Save(tempPath);

        // Set the black bitmap as the wallpaper
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
    }
}
```

This application uses the `SystemParametersInfo` function from the `user32.dll` library to set the wallpaper, 
and the `Bitmap` class from `System.Drawing` to create the black image. Thats why it only works on Windows. 

## License

setwall is free software: you can redistribute it and/or modify it under the terms that you see fit. No need to acknowledge the original source when doing so.
