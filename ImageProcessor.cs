using System;
using System.Drawing;

namespace BlurFaceBrowser
{
    internal class ImageProcessor
    {
        internal static Bitmap Blur(Bitmap image, Rectangle rectangle, int strength)
        {
            throw new NotImplementedException();
        }

        public static Bitmap Pixelate(Bitmap image, int blurSize)
        {
            return ImageProcessor.Pixelate(image, new Rectangle(0, 0, image.Width, image.Height));
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