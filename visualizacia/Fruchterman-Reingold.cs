using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualizacia
{
    class Fruchterman_Reingold:IGetCoordinat
    {
        public int m  =100 ;
        public double l;
        public double delta;
        public double h;
        public double w;
        public Graph g;
        private int iterator = 1;
        private int step = 20;
        private bool optimumPosition;
        public  Fruchterman_Reingold(int my_m, Graph my_g,double my_w,double my_h) 
        {
            m = my_m;
            w = my_w;
            h = my_h;
            l = Math.Sqrt(h*w/my_g.n);
            delta = w; ;
            g = new Graph(my_g);
            optimumPosition = false;
        }
        public Fruchterman_Reingold(Graph my_g, double my_w, double my_h)
        {
            l = Math.Sqrt(my_h * my_w / my_g.n);
            delta = my_w;
            w = my_w;
            h = my_h;
            g = new Graph(my_g);
            optimumPosition = false;
        }
       public Graph GetNewCoordinats()
        {
           getNextPosition();
           //g.l = l;
           return g;

        }
       public void setStep(int k) 
       {
           step = k;
       }
       public double Delta(int k) 
       {
           return delta * 0.1 / k;
       }
         public Vertex FAttr(Vertex p1, Vertex p2)
        {
            var kof_f =Graph.Distance(p1,p2)/l;
            var p1p2 = p2.Sub(p1);
            p1p2.Mul(kof_f);
            return p1p2;
        }
        private Vertex FRep(Vertex p1, Vertex p2)
        {
            var kof_f = l*l/ (Graph.SqDistance(p1, p2));
            var p2p1 = p1.Sub(p2);
            p2p1.Mul(kof_f);
            return p2p1;
        }
        private bool isOptimumPosition(Vertex[] forces)
        {
            optimumPosition = true;
            foreach (Vertex p in forces)
            {
                if (Graph.Distance(p) > 0.5) optimumPosition = false;
            }
            return optimumPosition;
        }
        public bool hasNextPosition()
        {
            return !optimumPosition;
        }
        public void getNextPosition() 
        {
            Vertex[] shift;
            int length = g.n;
            shift = new Vertex[length];
            for (int r = iterator; r < iterator + step; r++)
            {
                if (r > m)
                {
                    optimumPosition = true;
                    return;
                }
                for (int i = 0; i < length; i++)
                {
                    Vertex f = new Vertex(0, 0); ;
                    for (int j = 0; j < length; j++)
                    {
                        if (i != j)
                        {
                            f.Add(FRep(g.vertexs[i], g.vertexs[j]));
                            if (g.edges[i, j] == 1)
                                f.Add(FAttr(g.vertexs[i], g.vertexs[j]));

                        }
                    }
                    shift[i] = f;
                    double t = Delta(r);
                    double d = Graph.Distance(shift[i]);
                    t = Math.Min(d, t);
                    shift[i].Mul(t / d);
                }
                if (isOptimumPosition(shift)) return;
                for (int i = 0; i < length; i++)
                {
                    g.vertexs[i].Add(shift[i]);
                    g.vertexs[i].x = Math.Min(w / 2, Math.Max(-w / 2, g.vertexs[i].x));
                    g.vertexs[i].y = Math.Min(h / 2, Math.Max(-h / 2, g.vertexs[i].y));
                }

            }
            iterator+=step;
   
        }
    }
}
