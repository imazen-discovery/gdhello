// Read in a JPEG image file and print out its dimensions.

// Example which loads a file via the ImageData class.

using System;
using System.IO;

using LibGD;
using System.Diagnostics;

class Hello
{
    static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: loadimg.exe <filename>");
            return 1;
        }/* if */


        gdImageStruct im;


        var fp = C.fopen(args[0], "rb");

        if (fp == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open file");
            return 1;
        }

        im = gd.gdImageCreateFromJpeg(new _iobuf(fp));

        C.fclose(fp);

        if (im == null)
        {
            Console.WriteLine("Failed to decode file");
            return 1;
        }

        Stopwatch sw = new Stopwatch();

        sw.Start();
        gd.gdImageSetInterpolationMethod(im, gdInterpolationMethod.GD_BICUBIC);

        var newim = gd.gdImageScale(im, 500, 300);
        sw.Stop();


        
        var fpOut = C.fopen(args[0] + ".small.jpg", "wb");

        if (fpOut == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open output file");
            return 1;
        }
        gd.gdImageJpeg(newim, new _iobuf(fpOut), 100);
        C.fclose(fpOut);
        Console.WriteLine("Resized image from {0}x{1} to {2}x{3} in {4}ms", im.sx, im.sy, newim.sx, newim.sy, sw.ElapsedMilliseconds);

        gd.gdImageDestroy(im);
        gd.gdImageDestroy(newim);


 
        Console.ReadKey();
        return 0;
    }
}

