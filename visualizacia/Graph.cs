using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualizacia
{
    public class Graph
    {   
        public int[,] edges;
        public Vertex[] vertexs;
        public int n;
        public int[] degs;
        private int[] l = new int[2];
        public  Graph(int number, Vertex[] my_vertexs,int[,] my_edges,int[] my_degs) 
        {
            n = number;
            vertexs = new Vertex[n];
            edges = new int[n, n];
            degs = new int[n];
            Array.Copy(my_edges, edges, n * n);
            Array.Copy(my_degs, degs, n);
            vertexs = my_vertexs.Select(l => l.Clone()).ToArray();
        }
        public Graph(Graph g1)
        {
            n = g1.n;
            Array.Copy(g1.l, l, 2);
            vertexs = new Vertex[n];
            edges = new int[n, n];
            degs = new int[n];
            Array.Copy(g1.edges, edges, n * n);
            Array.Copy(g1.degs, degs, n);
            vertexs = g1.vertexs.Select(b => b.Clone()).ToArray();
        }
        public Graph(int number, int[,] my_edges, int[] my_degs)
        {
            n = number;
            edges = new int[n, n];
            degs = new int[n];
            Array.Copy(my_edges, edges, n * n);
            Array.Copy(my_degs, degs, n);
        }
        public void SetSizeEdge(int[] d)
        {
            l = new int[2];
            Array.Copy(d, l,2);
        }
        public void GenerateVertexsPosition(int rangeX,int rangeY)
        {
           vertexs = new Vertex[n];
            Random rand = new Random();
            bool[,] usedVertex = new bool[rangeX, rangeY];
            int i = 0;
            while (i < n)
            {
                int x = rand.Next(rangeX-1);
                int y = rand.Next(rangeY-1);
                if (!usedVertex[x, y])
                {
                    vertexs[i] = new Vertex(x , y);
                    for (int k = Math.Max(0, x - rangeX / 20); k < Math.Min(rangeX, x+ rangeX / 20); k++)
                        for (int j = Math.Max(0, y - rangeY / 20); j <= Math.Min(rangeY, y + rangeY / 20); j++) usedVertex[k, j] = true;
                    i++;
                }
            }
        }
        public double getSizeEdge()
        {
            return Graph.Distance(vertexs[l[0]], vertexs[l[1]]);
        }
       public static double Distance(Vertex p1, Vertex p2) 
       {
           return Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
       }
       public static double Distance(Vertex p1)
       {
           return Math.Sqrt((p1.x) * (p1.x) + (p1.y) * (p1.y));
       }
       public static double SqDistance(Vertex p1, Vertex p2)
       {
           return((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
       }
        
    }
}
