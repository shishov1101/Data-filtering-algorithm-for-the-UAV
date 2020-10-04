using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filter
{
    class TVector
    {
        public double x, y, z;

        public TVector(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public void Fill(Kvaternion q, Kvaternion a)
        {
            x = 2 * (q[2] * q[4] - q[1] * q[3]) - a[2];
            y = 2 * (q[1] * q[2] + q[3] * q[4]) - a[3];
            z = 2 * (0.5 - q[2] * q[2] - q[3] * q[3]) - a[4];
        }

        public TVector normal()
        {
            Double length = Math.Sqrt(x * x + y * y + z * z);
            if (length == 0)
                return new TVector(0, 0, 0);
            else
             return new TVector(x / length, y / length, z / length);
        }

        public override String ToString()
        {
            String X = "(";
            X = X + String.Format("{0:0.###}", x) + ",";
            X = X + String.Format("{0:0.###}", y) + ",";
            X = X + String.Format("{0:0.###}", z);
            X = X + "); ";
            return X;
        }
    }

    class RTK
    {
        Double heading;
        Double altitude;
        Double bank;

        public RTK(Double h, Double a, Double b)
        {
            heading = h;
            altitude = a;
            bank = b;
        }

        public Kvaternion quat_from_angles_rad()
        {
            Kvaternion q_h = new Kvaternion(new TVector(0, 0, 0), 0);
            q_h.setz(Math.Sin(heading / 2));
            q_h.setw(Math.Cos(heading / 2));

            Kvaternion q_a = new Kvaternion(new TVector(0, 0, 0), 0);
            q_a.sety(Math.Sin(altitude / 2));
            q_a.setw(Math.Cos(altitude/ 2));

            Kvaternion q_b = new Kvaternion(new TVector(0, 0, 0), 0);
            q_b.setx(Math.Sin(bank / 2));
            q_b.setw(Math.Cos(bank / 2));

            Kvaternion q_temp = q_h.mult(q_a);
            return q_temp.mult(q_b);
        }

        internal void setb(double p)
        {
            bank = p;
        }

        internal void seta(double p)
        {
            altitude = p;
        }

        internal void seth(double p)
        {
            heading = p;
        }

        public override String ToString()
        {
            String X = "(";
            X = X + String.Format("{0:0.###}", heading) + ",";
            X = X + String.Format("{0:0.###}", altitude) + ",";
            X = X + String.Format("{0:0.###}", bank) + ",";
            X = X + "); ";
            return X;
        }
    }
}
