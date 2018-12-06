#r "System.Drawing"

using System;
using System.Drawing;
using ImageProcessor;

private static readonly Size size = new Size(200,200);

public static void Run(Stream myBlobOriginal, Stream myBlobSmall)
{
    using (var imageFactory = new ImageFactory())
    {
        imageFactory
            .Load(myBlobOriginal)
            .Resize(size)
            .Save(myBlobSmall);
    }
}


