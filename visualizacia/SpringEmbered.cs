using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualizacia
{
    public class SpringEmbered:IGetCoordinat
    {
        public int m ;
        public double c1 ;
        public double c2 ;
        public double l ;
        public double k ;
        public Graph g;
        private int iterator = 1;
        private int step = 20;
        private bool optimumPosition;
        public  SpringEmbered(int my_m, double my_c1, double my_c2, double my_l, double my_k, Graph my_g) 
        {
            m = my_m;
            c1 = my_c1;
            c2 = my_c2;
            l = my_l;
            k = my_k;
            g = new Graph(my_g);
            optimumPosition = false;
            changePosition();
        }
        public bool isOptimumPosition(){
            return optimumPosition;
        }
        public SpringEmbered(Graph g1) 
        {
            m = 200;
            c1 = 1.0;
            c2 = 1.0;
            l = 1.0;
            k = 0.1;
            g = new Graph(g1);
            optimumPosition = false;
            changePosition();
        }
        public void setStep(int k) 
        {
            step = k;
        }
       public Graph GetNewCoordinats()
        {
           if(!optimumPosition)
            getNextPosition();
            return g;

        }
        private Vertex FSprings(Vertex p1, Vertex p2)
        {
            var kof_f = c1 * Math.Log(Graph.Distance(p1, p2) / l) / Graph.Distance(p1, p2);
            var p1p2 = p2.Sub(p1);
            p1p2.Mul(kof_f);
            return p1p2;
        }
        private Vertex FRep(Vertex p1, Vertex p2)
        {
            var kof_f = c2 / (Graph.Distance(p1, p2) * Graph.Distance(p1, p2));
            var p2p1 = p1.Sub(p2);
            p2p1.Mul(kof_f);
            return p2p1;
        }
        public void changePosition() {
            for (int i = 0; i < g.n; i++) {
                g.vertexs[i].Div(10);
            }
        }
        private bool isOptimumPosition(Vertex[] forces) 
        {
            optimumPosition = true;
            foreach (Vertex p in forces) 
            {
                if (Graph.Distance(p) > 0.001) optimumPosition = false; 
            }
            return optimumPosition;
        }
        public bool hasNextPosition() {
            return !optimumPosition;
        }
        public void getNextPosition() 
        {
            Vertex[] shift;
            int length = g.vertexs.Length;
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
                        if (g.edges[i, j] == 1 && i != j)
                        {
                            f.Add(FSprings(g.vertexs[i], g.vertexs[j]));
                        }
                        else
                        {
                            if (i != j)
                                f.Add(FRep(g.vertexs[i], g.vertexs[j]));
                        }
                    }
                    shift[i] = f;
                }
                if (isOptimumPosition(shift)) return;
                for (int i = 0; i < length; i++)
                {
                    shift[i].Mul(k);
                    g.vertexs[i].Add(shift[i]);
                }

            }
            iterator += step;
        }
    }
}
