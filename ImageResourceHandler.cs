using CefSharp;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BlurFaceBrowser
{
    public class ImageResourceHandler : ResourceHandler
    {
        public override bool ProcessRequestAsync(IRequest request, ICallback callback)
        {
            string requestUrl = request.Url;
            Task.Run(() =>
            {
                using ( callback )
                {
                    try
                    {
                        var httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUrl);
                        var httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                        // Get the stream associated with the response.
                        var receiveStream = httpWebResponse.GetResponseStream();
                        var mime = httpWebResponse.ContentType;

                        var stream = new MemoryStream();
                        receiveStream.CopyTo(stream);
                        httpWebResponse.Close();

                        stream.Position = 0;
                        Bitmap bitmap = new Bitmap(stream);

                        List<Rectangle> rectangleList = new List<Rectangle>();
                        rectangleList = FaceDetection.DetectAllFromBitmap(bitmap);

                        foreach ( Rectangle rectangle in rectangleList )
                        {
                            bitmap = ImageProcessor.Pixelate(bitmap, rectangle);
                        }

                        if ( rectangleList.Count > 0 )
                        {
                            stream.Position = 0;
                            bitmap.Save(stream, ImageFormat.Jpeg);
                        }

                        stream.Position = 0;
                        ResponseLength = stream.Length;
                        MimeType = mime;
                        StatusCode = (int) HttpStatusCode.OK;
                        Stream = stream;
                    }
                    catch
                    {
                    }
                    callback.Continue();
                }
            });

            return true;
        }
    }
}
