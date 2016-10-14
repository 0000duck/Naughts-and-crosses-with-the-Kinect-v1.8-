using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CameraCapture
{
    class mark
    {
        public int xpos;
        public int ypos;
        public int boardx;
        public int boardy;

        public int state;
        public int size;
        public int psize;

        public int type;

        //mark overall x pos
        public int getxpos()
        {
            return xpos;
        }
        public void setxpos(int x)
        {
            xpos = x;
        }

        //mark ovarll y pos
        public int getypos()
        {
            return ypos;
        }
        public void setypos(int y)
        {
            ypos = y;
        }


        //mark square x pos
        public int getboardx()
        {
            return boardx;
        }
        public void setboardx(int b)
        {
            boardx = b;
        }

        //mark square y pos
        public int getboardy()
        {
            return boardy;
        }
        public void setboardy(int c)
        {
            boardy = c;
        }


        //peice types (1 for triangle, 2 for square)
        public int gettype()
        {
            return type;
        }
        public void settype(int ty)
        {
            type = ty;
        }


    }
}
