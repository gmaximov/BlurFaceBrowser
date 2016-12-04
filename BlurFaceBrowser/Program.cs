// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlurFaceBrowser
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Cef.EnableHighDPISupport();

            if ( !Cef.Initialize() )
            {
                throw new Exception("Unable to Initialize Cef");
            }

            Application.Run(new SimpleBrowserForm());
        }
    }
}
