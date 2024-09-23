using System;
using System.Windows.Forms;

namespace SpaceGameTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Console.WriteLine("Form Initialized");
            InitializeComponent();
            Console.WriteLine("Form Initialized");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
