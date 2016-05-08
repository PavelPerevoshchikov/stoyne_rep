using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;


namespace visualizacia
{
    public partial class Form1 : Form
    {
        private Graph graphForDraw;
        private IDraw areaDraw;
        private bool f;
        public Form1()
        {
            InitializeComponent();
        }
        public Graph ReadInput() 
        {
            String s;
            Graph new_graph;
            var sep = new Char[] { ' ' };
            int[,] matrix;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\pavel\documents\visual studio 2013\Projects\visualizacia\input.txt");
            s = s = file.ReadLine();
            var split = s.Split(sep);
            int n = Int16.Parse(split[0]);
            matrix = new int[n, n];
            int[] degs = new int[n];
            Vertex[] V = new Vertex[n];
            V[0] = new Vertex(5.0, 5.0);
            V[1] = new Vertex(5.0, 30.0);
            V[2] = new Vertex(100.0, 5.0);
            int deg = 0;
            double distance = 1;
            double l = 1;
            int[] length = new int[2];
          for(int i = 0; i<n; i++){
                s = file.ReadLine();
                split = s.Split(sep);
                int m = split.Length;
                deg = 0;
                for (int j = 0; j < m;j++ )
                {
                    matrix[i, j] = Int16.Parse(split[j]);
                    if (matrix[i, j] == 1) 
                    {
                        //distance = Math.Sqrt((V[i].x - V[j].x) * (V[i].x - V[j].x) + (V[i].y - V[j].y) * (V[i].y - V[j].y)); 
                        if (distance > l)
                        {
                            //l = distance;
                            //length[0] = i;
                           // length[1] = j;
                        }
                        deg++;
                        
                    }
                }
                degs[i] = deg;
            }
            new_graph = new Graph(n,matrix,degs);
            new_graph.SetSizeEdge(length);
            file.Close();
            return new_graph;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            f = true;
            button1.Visible = false;
            SpringEmbered se = new SpringEmbered(graphForDraw);
            areaDraw.SetIDraw(se);
            while (areaDraw.hasNext()) 
            {
                areaDraw.Background();
                areaDraw.DrawGraphWitnNewCoordinat();
                pictureBox1.Image = areaDraw.getPicture();
                pictureBox1.Update();
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            graphForDraw = ReadInput();
            button2.Visible = false;
            int Xmax, Ymax;
            pictureBox1.Dock = DockStyle.Left;
            //Xmax = ClientSize.Width;
            //Ymax = ClientSize.Height;
            Size size = pictureBox1.Size;
            Xmax = size.Width;
            Ymax = size.Height;
            //pictureBox1.Size = new Size(Xmax,Ymax);
            //graphForDraw.GenerateVertexsPosition(Xmax - 10, Ymax - 10);
            var viz = new Vizualization(Xmax, Ymax);
            areaDraw = new DrawWithSize(viz,30);
            areaDraw.Background();
            //areaDraw.DrawGraph(graphForDraw);
            Point p1 = new Point(0,0);
            Point p2 = new Point(Xmax,Ymax);
            areaDraw.DrawLine(p1, p2);
            pictureBox1.Image = areaDraw.getPicture();
            pictureBox1.Update();
            button1.Visible = true;
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            switch (symbol)
            {
                case '=': changeZoom('+');
                    break;
                case '-': changeZoom('-');
                    break;
            }

        }
        private void changeZoom(char ch) {
            if (ch == '-')
            {
                areaDraw.ZoomDown();
            }
            else { areaDraw.ZoomDown(); }
            if (!f)
            {
                areaDraw.DrawGraph(graphForDraw);
            }
            else { areaDraw.DrawGraphWitnNewCoordinat(); }
        }
    }
}
