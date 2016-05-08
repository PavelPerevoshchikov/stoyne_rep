using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace visualizacia
{
   public interface IDraw
    {
       Bitmap DrawGraph(Graph g);
       Bitmap DrawGraphWitnNewCoordinat();
       Point toPoint(Vertex p);
       Point toPoint(double x,double y);
       Bitmap Background();
       Bitmap getPicture();
       void SetIDraw(IGetCoordinat graf);
       void ZoomUp();
       void ZoomDown();
      // int GetStep();
       bool hasNext();
       void DrawLine(Point p1, Point p2);

    }
}
