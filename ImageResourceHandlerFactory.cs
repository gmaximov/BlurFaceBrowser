using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlurFaceBrowser
{
    public class ImageResourceHandlerFactory : IResourceHandlerFactory
    {
        bool IResourceHandlerFactory.HasHandlers
        {
            get { return true; }
        }

        IResourceHandler IResourceHandlerFactory.GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            if ( request.ResourceType == ResourceType.Image )
            {
                Console.WriteLine(request.Url);
                return new ImageResourceHandler();
            }
            return null;
        }
    }
}
