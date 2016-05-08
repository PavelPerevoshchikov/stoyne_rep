using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace visualizacia
{
    public class DrawWithScal:IDraw
    {
        public IGetCoordinat balancer;
        public Vizualization subject;
        public double xmin = 1000000;
        public double xmax = 0;
        public double ymin = 100000;
        public int Xmax{get;set;}
        public int Xmin{get;set;}
        public int Ymax{get;set;}
        public int Ymin{get;set;}
        public double ymax = 0;
        public double kx = 1;
        public double ky = 1;
        public double zoom { get; set; }
        public int size { get; set; }
        public DrawWithScal(IGetCoordinat g,Vizualization viz) {
            balancer = g;
            subject = viz;
            zoom = 1.0;
        }
        public DrawWithScal(Vizualization viz,int Size)
        {
            subject = viz;
            Xmax = viz.Width-Size/2;
            Ymax = viz.Height-Size/2;
            Xmin = Ymin = Size / 2;
            size = Size;
            zoom = 1.0;
        }
        public void ZoomDown() 
        {
            Xmax = Convert.ToInt32(Xmax*0.75);
            Xmin = Convert.ToInt32(Xmin*1.25);
            Ymax = Convert.ToInt32(Ymax*0.75);
            Ymin = Convert.ToInt32( Ymin * 1.25);

        }
        public void ZoomUp() 
        {
            Xmax = Convert.ToInt32(Xmax * 1.25);
            Xmin = Convert.ToInt32(Xmin * 0.75);
            Ymax = Convert.ToInt32(Ymax * 1.25);
            Ymin = Convert.ToInt32(Ymin * 0.75);
        }
        public void SetIDraw(IGetCoordinat idraw)
        {
            balancer = idraw;
        }
        public Bitmap getPicture() 
        {
            return subject.picture;
        }
        public bool hasNext() {
            return balancer.hasNextPosition();
        }
        public virtual Bitmap DrawGraph(Graph graph){
            Vertex[] v = graph.vertexs;
            Find(v);
            double l = graph.getSizeEdge()/10;
            int n = v.Length;
            xmin = xmin - l / 2;
            xmax = xmax + l / 2;
            ymin = ymin - l / 2;
            ymax = ymax + l / 2;
            bool[] used = new bool[n];
            Point p1 = toPoint(v[0].x, v[0].y);
            Point p2 = toPoint(v[0].x + l, v[0].y - l);
            int dx = Math.Abs(p1.X - p2.X);
            int dy = Math.Abs(p1.Y - p2.Y);
            size = Math.Min(dx, dy);
            kx = size * 1.0 / dx;
            ky = size * 1.0 / dy;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (graph.edges[i, j] == 1)
                        subject.g.DrawLine(subject.myPen, toPoint(v[i].x, v[i].y), toPoint(v[j].x, v[j].y));
                }
            foreach (Vertex p in v)
            {
                Point centre = toPoint(p.x - l / 2, p.y + l / 2);
                subject.ellipseBounds = new Rectangle(centre, new Size(size, size));
                subject.g.FillEllipse(subject.redBrush, subject.ellipseBounds);
            }
            SetConst();
            return subject.picture;
        }
        public Bitmap DrawGraphWitnNewCoordinat()
        {
            
            Graph graph = balancer.GetNewCoordinats();
            return DrawGraph(graph);

        }
        public void SetConst()
        {
            xmin = 1000000;
            xmax = 0;
            ymin = 100000;
            ymax = 0;
            kx = 1;
            ky = 1;
        }
        public Bitmap Background(){
            return subject.Background();
        }
        public void DrawLine(Point p1, Point p2) {
            subject.DrawLine(p1, p2);
        }
        public Point toPoint(Vertex p)
        {
            int xx = Convert.ToInt32(((Xmax - Xmin) * ((p.x - xmin) / (xmax - xmin))) * kx) + Xmin;
            int yy = Convert.ToInt32((Ymax-Ymin) * (1 - ((p.y - ymin) / (ymax - ymin)))*ky) + Ymin;
            return new Point(xx, yy);
        }
        public Point toPoint(double x, double y)
        {
            int xx = Convert.ToInt32(((Xmax - Xmin) * ((x - xmin) / (xmax - xmin))) * kx) +Xmin;
            int yy = Convert.ToInt32((Ymax - Ymin) * (1 - ((y - ymin) / (ymax - ymin))) * ky) + Ymin;
            return new Point(xx, yy);
        }
        public Point toPointScalX(double x, double y,double dx,double dy)
        {
            int xx = Convert.ToInt32((x - xmin) * dx) +Xmin;
            int yy = Convert.ToInt32((y - ymin)*dx) + Ymin;
            return new Point(xx, yy);
        }
        public Point toPointScalY(double x, double y,double dx,double dy)
        {
            int xx = Convert.ToInt32((x - xmin)*dy) +Xmin;
            int yy = Convert.ToInt32((y - ymin) * dy) +Ymin;
            return new Point(xx, yy);
        }
        public Point toPointScal(double x,double y){
            double dx = (Xmax-Xmin) / (xmax - xmin) * 1.0;
            double dy = (Ymax-Ymin) / (ymax - ymin) * 1.0;
            if (dx < dy) return toPointScalX(x, y, dx, dy);
            return toPointScalY(x, y, dx, dy);
        }
        public void Find(Vertex[] v)
        {
            foreach(Vertex p in v)
            {
                if (p.x < xmin) xmin = p.x;
                if (p.x > xmax) xmax = p.x;
                if (p.y < ymin) ymin = p.y;
                if (p.y > ymax) ymax = p.y;
            }
        }
    }
}
