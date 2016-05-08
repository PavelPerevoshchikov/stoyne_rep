using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace visualizacia
{
    public class Vertex
    {
        public double x;
        public double y;
        public Vertex()
        {
            x = 0;
            y = 0;
        }
        public Vertex(double xx, double yy)
        {
            x = xx;
            y = yy;
        }
        public void Set(double x_, double y_) 
        {
            x = x_;
            y = y_;
        }
        public void Add(Vertex p1)
        {
            x = x + p1.x;
            y = y + p1.y;
        }
        public Vertex Sub(Vertex p2)
        {
            return new Vertex(x - p2.x, y - p2.y);
        }
        public void Div(double k)
        {
            x = x / k;
            y = y / k;
        }
        public void Mul(double k)
        {
            x = x * k;
            y = y * k;
        }
        public Vertex Clone() 
        {
            Vertex v = new Vertex(x, y);
            return v;
        }
        public double CosAngle(Vertex p1, Vertex p2) 
        {
            double coord = p1.x * p2.x + p1.y * p2.y;
            double norma_p1 = Math.Sqrt(p1.x*p1.x +p1.y*p1.y);
            double norma_p2 = Math.Sqrt(p2.x * p2.x + p2.y * p2.y);
            return coord / (norma_p1 * norma_p2);
        }
    }
}
