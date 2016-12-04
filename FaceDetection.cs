using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BlurFaceBrowser
{
    class FaceDetection
    {
        private static Rectangle[] Detect(Image<Bgr, byte> image, string fileName)
        {
            using ( CascadeClassifier cascadeClassifier = new CascadeClassifier(fileName) )
            {
                using ( Image<Gray, byte> grayframe = image.Convert<Gray, byte>() )
                {
                    return cascadeClassifier.DetectMultiScale(grayframe, 1.2, 10, new Size(20, 20));                    
                }
            }
        }

        public static Rectangle[] FrontalDetect(Image<Bgr, byte> image)
        {
            return Detect(image, "haarcascade_frontalface_alt_tree.xml");
        }
        public static Rectangle[] ProfileDetect(Image<Bgr, byte> image)
        {
            return Detect(image, "haarcascade_profileface.xml");
        }
        public static List<Rectangle> DetectAllFromBitmap(Bitmap bitmap)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(bitmap);

            List<Rectangle> rectangleList = new List<Rectangle>();
            rectangleList.AddRange(FrontalDetect(image));
            rectangleList.AddRange(ProfileDetect(image));
            return rectangleList;
        }
    }
}
