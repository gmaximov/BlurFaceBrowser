using System;
using System.Drawing;

namespace BlurFaceBrowser
{
    internal class ImageProcessor
    {
        public static Bitmap Blur(Bitmap image, Rectangle rectangle, int pixelateSize = 6)
        {
            pixelateSize = Math.Max(image.Size.Height / 100, 1) * Math.Max(image.Size.Width / 100, 1) * pixelateSize;

            Bitmap blurred = new Bitmap(image);   //image.Width, image.Height);
            using ( Graphics graphics = Graphics.FromImage(blurred) )
            {
                // look at every pixel in the blur rectangle
                for ( Int32 xx = rectangle.Left; xx < rectangle.Right; xx += pixelateSize )
                {
                    for ( Int32 yy = rectangle.Top; yy < rectangle.Bottom; yy += pixelateSize )
                    {
                        Int32 avgR = 0, avgG = 0, avgB = 0;
                        Int32 blurPixelCount = 0;
                        Rectangle currentRect = new Rectangle(xx, yy, pixelateSize, pixelateSize);

                        // average the color of the red, green and blue for each pixel in the
                        // blur size while making sure you don't go outside the image bounds
                        for ( Int32 x = currentRect.Left; (x < currentRect.Right && x < image.Width); x++ )
                        {
                            for ( Int32 y = currentRect.Top; (y < currentRect.Bottom && y < image.Height); y++ )
                            {
                                Color pixel = blurred.GetPixel(x, y);

                                avgR += pixel.R;
                                avgG += pixel.G;
                                avgB += pixel.B;

                                blurPixelCount++;
                            }
                        }

                        avgR = avgR / blurPixelCount;
                        avgG = avgG / blurPixelCount;
                        avgB = avgB / blurPixelCount;

                        // now that we know the average for the blur size, set each pixel to that color
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(avgR, avgG, avgB)), currentRect);
                    }
                }
                graphics.Flush();
            }
            return blurred;
        }

        public static Bitmap Pixelate(Bitmap image, Rectangle rectangle)
        {
            int pixelateSize = Math.Max(image.Size.Height / 500, 1) * Math.Max(image.Size.Width / 800, 1) * 10;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            using ( Graphics graphics = Graphics.FromImage((Image) bitmap) )
                graphics.DrawImage((Image) image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            int x1 = rectangle.X;
            while ( x1 < rectangle.X + rectangle.Width && x1 < image.Width )
            {
                int y1 = rectangle.Y;
                while ( y1 < rectangle.Y + rectangle.Height && y1 < image.Height )
                {
                    int num1 = pixelateSize / 2;
                    int num2 = pixelateSize / 2;
                    while ( x1 + num1 >= image.Width )
                        --num1;
                    while ( y1 + num2 >= image.Height )
                        --num2;
                    Color pixel = bitmap.GetPixel(x1 + num1, y1 + num2);
                    for ( int x2 = x1; x2 < x1 + pixelateSize && x2 < image.Width; ++x2 )
                    {
                        for ( int y2 = y1; y2 < y1 + pixelateSize && y2 < image.Height; ++y2 )
                            bitmap.SetPixel(x2, y2, pixel);
                    }
                    y1 += pixelateSize;
                }
                x1 += pixelateSize;
            }
            return bitmap;
        }
    }
}