using ConvRse.ConsoleHost;
using Microsoft.Owin.Hosting;
using System;
using System.Windows.Forms;

namespace WindowsFormsHot
{
    public partial class Form1 : Form
    {
        private IDisposable app;

        public Form1()
        {
            InitializeComponent();
        }

        private void bnStart_Click(object sender, EventArgs e)
        {
            app = WebApplication.Start<Startup>("http://127.0.0.1:7777/");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (app != null)
            {
                app.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
