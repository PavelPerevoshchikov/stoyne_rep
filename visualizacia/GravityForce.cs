using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualizacia
{
    public class GravityForce
    {
        private bool[] used;
        public Graph g;
        public double l = 1.0;
        public double[] delta;
        public double cGrav = 2.0;
        private double eps = 0.1;
        public GravityForce(Graph my_g,double area)
        {
            Graph g = new Graph(my_g);
            l = Math.Sqrt(area / g.n);
            delta = new double[g.n];
            delta = delta.Select(ll=>0.2).ToArray();
        }
        private void springEmbedder()
        {
            Vertex[] shift;
            int length = g.n;
            shift = new Vertex[length];
            used = new bool[g.n];
            List<int> random_vertex= new List<int>();
            for (int r = 0; r < 100; r++)
            {
                toFalse();
                random_vertex = GenNumberVertex();
                for (int i = 0; i < length; i++)
                {
                    Vertex f = new Vertex(0, 0);
                    f.Add(FGravity(random_vertex[i]));
                    for (int j = 0; j < length; j++)
                    {
                        if (g.edges[random_vertex[i], j] == 1 && random_vertex[i] != j)
                        {
                            f.Add(FAttr(random_vertex[i], j));
                        }
                        else
                        {
                            if (i != j)
                                f.Add(FRep(g.vertexs[random_vertex[i]], g.vertexs[j]));
                        }
                    }
                    f.Mul(eps);
                    g.vertexs[random_vertex[i]].Add(f);
                }
            }
        }
        private void toFalse() 
        {
            for (int i = 0; i < g.n; i++) { used[i] = false; }
        }
        private List<int> GenNumberVertex()
        {
            List<int> l = new List<int>();
            Random rand = new Random();
            int rand_numb;
            while (l.Count < g.n) {
                rand_numb = rand.Next(g.n);
                if (!used[rand_numb]) l.Add(rand_numb);
            }
            return l;
        }
        public Vertex FRep(Vertex p1, Vertex p2) // сила отталкивания 
        {
            var kof_f = l * l / Graph.SqDistance(p1, p2);
            var p2p1 = p1.Sub(p2);
            p2p1.Mul(kof_f);
            return p2p1;
        }
        public Vertex FAttr(int i,int j) // сила притяжения
        {
            Vertex p1 = g.vertexs[i];
            Vertex p2 = g.vertexs[j];
            var kof_f = Graph.SqDistance(p1, p2)/(l*Fi(i));
            var p1p2 = p2.Sub(p1);
            p1p2.Mul(kof_f);
            return p1p2;
        }
        public Vertex FGravity(int i) 
        {
            Vertex p = g.vertexs[i];
            Vertex B = GetCetnreGravity();
            Vertex  pB = B.Sub(p);
            pB.Mul(Fi(i)*cGrav);
            return pB;
        }
        public Vertex GetCetnreGravity() {
            Vertex p = new Vertex();
            for (int i = 0; i < g.vertexs.Length; i++) {
                p.Add(g.vertexs[i]);
            }
            p.Div(g.vertexs.Length*1.0);
            return p;
        }
        public double Fi(int number_v) 
        {
            return 1 + g.degs[number_v] / 2.0;
        }
    }
}
