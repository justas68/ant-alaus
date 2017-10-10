using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    public class Calculator
    {
        public double percentage(Point[] p1, Point[] p2)
        {
            Point temp;
            if (!(isParallel(p1[0], p2[0], p1[1], p2[1]) && isParallel(p1[0], p2[0], p1[2], p2[2]) && isParallel(p1[1], p2[1], p1[2], p2[2])))
                return 0;
            if (p1[0].Y > p1[1].Y)
            {
                temp = p1[0];
                p1[0] = p1[1];
                p1[0] = temp;
                temp = p2[0];
                p2[0] = p1[1];
                p2[0] = temp;
            }
            if (p1[0].Y > p1[2].Y)
            {
                temp = p1[0];
                p1[0] = p1[2];
                p1[0] = temp;
                temp = p2[0];
                p2[0] = p1[2];
                p2[0] = temp;
            }
            if (p1[1].Y > p1[2].Y)
            {
                temp = p1[1];
                p1[1] = p1[2];
                p1[1] = temp;
                temp = p2[1];
                p2[1] = p1[2];
                p2[1] = temp;
            }

            // po šito jau aišku, kuri linija yra kuri
            double s1 = (Math.Abs(p1[0].X - p2[0].X) + Math.Abs(p1[2].X - p2[2].X)) / 2 * Math.Abs(p1[0].Y - p2[2].Y);
            double s2 = (Math.Abs(p1[1].X - p2[1].X) + Math.Abs(p1[2].X - p2[2].X)) / 2 * Math.Abs(p1[1].Y - p2[2].Y);

            return s2*100/s1;
        }
        public bool isParallel(Point p1L1, Point p2L1, Point p1L2, Point p2L2)
        {
            double dx1 = Math.Abs(p1L1.X - p2L1.X);
            double dy1 = Math.Abs(p1L1.Y - p2L1.Y);
            double dx2 = Math.Abs(p1L2.X - p2L2.X);
            double dy2 = Math.Abs(p1L2.Y - p2L2.Y);
            double cosAngle = Math.Abs((dx1 * dx2 + dy1 * dy2) / Math.Sqrt((dx1 * dx1 + dy1 * dy1) * (dx2 * dx2 + dy2 * dy2)));
            if (cosAngle > 0.95)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
