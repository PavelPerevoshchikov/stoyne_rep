using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace visualizacia
{
    class DrawWithSize:DrawWithScal
    {
        public DrawWithSize(IGetCoordinat g,Vizualization viz):base(g,viz) {
        }
        public DrawWithSize(Vizualization viz,int Size):base(viz,Size)
        {
        }
        public override Bitmap DrawGraph(Graph graph)
        {
            Vertex[] v = graph.vertexs;
            Find(v);
            int n = v.Length;
            bool[] used = new bool[n];
            kx =(xmax - xmin)/ (Xmax-Xmin);
            ky = (ymax - ymin)/(Ymax - Ymin);
            if (kx > ky)
            {
                ky = ky /kx;
                kx = 1;
            }
            else {
                kx = kx /ky;
                ky = 1;
            }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (graph.edges[i, j] == 1)
                        subject.g.DrawLine(subject.myPen, toPoint(v[i].x, v[i].y), toPoint(v[j].x, v[j].y));
                }
            foreach (Vertex p in v)
            {
                Point centre = toPoint(p.x, p.y);
                subject.ellipseBounds = new Rectangle(new Point(centre.X-size/2,centre.Y-size/2), new Size(size, size));
                subject.g.FillEllipse(subject.redBrush, subject.ellipseBounds);
            }
            SetConst();
            return subject.picture;
        }
    }
}
