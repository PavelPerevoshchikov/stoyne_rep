using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace visualizacia
{
       public class Vizualization
    {
        public int Width;
        public int Height;
        public Bitmap picture;
        public Graphics g;
        public SolidBrush greenBrush = new SolidBrush(Color.Green);
        public SolidBrush redBrush = new SolidBrush(Color.Red);
        public Rectangle ellipseBounds;
        public Pen myPen = new Pen(Color.Black, 3);
        public Vizualization(int w, int h) {
            Width = w;
            Height = h;
            picture = new Bitmap(w, h);
            g = Graphics.FromImage(picture);
        }
        public Bitmap DrawLine(Point p1, Point p2) 
        {
            g.DrawLine(myPen,p1, p2);
            return picture;
        }
        public Bitmap Background()
        {
            g.FillRectangle(Brushes.Blue, 0, 0, Width, Height);
            return picture;
        }
    }
}
