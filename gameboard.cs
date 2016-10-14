using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraCapture
{
    class gameboard
    {

        int xpos = 0;
        int ypos = 0;
        int state = 0;
        int size = 0;
        int psize = 0;

        //board square x pos
        public int getxpos()
        {
            return xpos;
        }
        public void setxpos(int x)
        {
            xpos = x;
        }

        //board square y pos
        public int getypos()
        {
            return ypos;
        }
        public void setypos(int y)
        {
            ypos = y;
        }

        //board state
        public int getstate()
        {
            return state;
        }

        public void setstate(int t)
        {
            state = t;
        }

        //Board square sizes
        public int getsize()
        {
            return size;
        }

        public void setsize(int s)
        {
            size = s;
        }


        //real world individual pixel sizes (mm)
        public int getpsize()
        {
            return psize;
        }

        public void setpsize(int p)
        {
            psize = p;
        }
    }
}
