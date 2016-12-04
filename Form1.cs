
using CefSharp.WinForms;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace BlurFaceBrowser
{
    public partial class Form1 : Form
    {
        public IWinFormsWebBrowser Browser { get; private set; }
        private Capture cap;

        public Form1()
        {
            InitializeComponent();

            var browser = new ChromiumWebBrowser("www.google.com")
            {
                Dock = DockStyle.Fill
            };


            panel1.Controls.Add(browser);
            Browser = browser;
            browser.ResourceHandlerFactory = new ImageResourceHandlerFactory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUrl(textBox1.Text);
        }

        private void LoadUrl(string url)
        {
            if ( Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute) )
            {
                Browser.Load(url);
            }
        }
    }
}
