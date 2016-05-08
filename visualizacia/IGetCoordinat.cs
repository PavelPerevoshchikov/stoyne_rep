using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualizacia
{
    public interface IGetCoordinat
    {
         Graph GetNewCoordinats();
         bool hasNextPosition();
         void setStep(int step);
    }
   
}
