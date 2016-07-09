﻿using Halak;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowStream
{
    public partial class Form1 : Form
    {
        private Graphics graphics = null;
        private SoulStream soulStream = null;

        public Form1()
        {
            InitializeComponent();
            graphics = Graphics.FromHwnd(Handle);
            soulStream = new SoulStream();

            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("sample_data.json"))
                {
                    // Read the stream to a string, and try json parse.
                    var json = JValue.Parse(sr.ReadToEnd());

                    foreach (var page in json["pages"].Array())
                    {
                        soulStream.AddNode(page.AsString());
                    }

                    foreach (var link in json["links"].Array())
                    {
                        soulStream.AddLink(link["from"].AsString(), link["to"].AsString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (graphics != null)
                graphics.Dispose();

            graphics = Graphics.FromHwnd(Handle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush b = new SolidBrush(Color.Black);

            Pen pen = new Pen(b, 20.5f);
            Point p1 = new Point(0, 0);
            Point p2 = new Point(this.Width, this.Height);

            graphics.Clear(Color.White);
            graphics.DrawLine(pen, p1, p2);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Console.WriteLine(e.Delta);
        }
    }
}
