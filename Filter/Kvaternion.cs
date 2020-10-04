using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Filter
{
    class Kvaternion
    {
        Double x, y, z;
        Double w; // Компонента, описывающая поворот

        string filename="Kvaternion.txt";
        StreamWriter sw;

        public Kvaternion(TVector rotate, Double angle)
        {
            TVector temp = rotate.normal();
            w = Math.Cos(angle / 2);
            x = temp.x * Math.Sin(angle / 2);
            y = temp.y * Math.Sin(angle / 2);
            z = temp.z * Math.Sin(angle / 2);

            sw = null;
        }

        public double this[int index] // возвращает заданный компонент вектора по номеру
        {
            get
            {
                switch (index)
                {
                    case 1: return w;
                    case 2: return x;
                    case 3: return y;
                    case 4: return z;
                }
                return 0;
            }
        }

        public override String ToString()
        {
            String X = "(";
            X = X + String.Format("{0:0.###}", x) + ",";
            X = X + String.Format("{0:0.###}", y) + ",";
            X = X + String.Format("{0:0.###}", z) + ",";
            X = X + String.Format("{0:0.###}", w);
            X = X + "); ";
            return X;
        }

        // scale

        // Lenght
        public Double Lenght()
        {
            Double D = Math.Sqrt(x * x + y * y + z * z);
            return D;
        }

        // normalize
        public Kvaternion normalize()
        {
            Kvaternion E = new Kvaternion(new TVector(0, 0, 0),
                2 * Math.Acos(w));
            E.x = x / Lenght();
            E.y = y / Lenght();
            E.z = z / Lenght();
            E.w = w;
            return E;
        }
        // invert
        public Kvaternion invert()
        {
            Kvaternion F = new Kvaternion(new TVector(0, 0, 0), 2 * Math.Acos(w));
            F.x = -x;
            F.y = -y;
            F.z = -z;
            return F;
        }

        public Kvaternion scale(double val)
        {
            Kvaternion V = new Kvaternion(new TVector(0, 0, 0),
                2 * Math.Acos(w));
            V.w = w * val;
            V.x = x * val;
            V.y = y * val;
            V.z = z * val;
            return V;
        }

        public Kvaternion mult(Kvaternion b)
        {
            Kvaternion V = new Kvaternion(new TVector(0, 0, 0),
               2 * Math.Acos(w));
            V.w = w * b.w - x * b.x - y * b.y - z * b.z;
            V.x = w * b.x + x * b.w + y * b.z - z * b.y;
            V.y = w * b.y - x * b.z + y * b.w + z * b.x;
            V.z = w * b.z + x * b.y - y * b.x + z * b.w;
            return V;
        }

        public Kvaternion mult(TVector b)
        {
            Kvaternion V = new Kvaternion(new TVector(0, 0, 0),
               2 * Math.Acos(w));
            V.w = -x * b.x - y * b.y - z * b.z;
            V.x = w * b.x + y * b.z - z * b.y;
            V.y = w * b.y - x * b.z + z * b.x;
            V.z = w * b.z + x * b.y - y * b.x;
            return V;
        }

        public TVector Povorot(TVector V)
        {
            Kvaternion T = this.mult(V);
            T = T.mult(this.invert());
            return new TVector(T.x, T.y, T.z);

        }

        public void setx(Double val)
        {
            x = val;
        }
        public void setw(Double val)
        {
            w = val;
        }
        public void sety(Double val)
        {
            y = val;
        }
        public void setz(Double val)
        {
            z = val;
        }

        public RTK quat_to_RTK()
        {
            Double qx2, qy2, qz2;
            this.normalize();
            qx2 = x * x;
            qy2 = y * y;
            qz2 = z * z;
            RTK temp = new RTK(0, 0, 0);
            temp.setb(Math.Atan2(2*(x*w+y*z),1-2*(qx2+qy2)));
            temp.seta(Math.Asin(2*(y*w-z*x)));
            temp.seth(Math.Atan2(2*(z*w+x*y),1-2*(qy2+qz2)));
            return temp;
       
       }
        public Kvaternion Plus(Kvaternion B)
        {
            Kvaternion C = new Kvaternion(new TVector(0, 0, 0), 2 * Math.Acos(w));
            C.x = x + B.x;
            C.y = y + B.y;
            C.z = z + B.z;
            C.w = w + B.w;
            return C;  
        }
        public Kvaternion Minus(Kvaternion B)
        {
            Kvaternion C = new Kvaternion(new TVector(0, 0, 0), 2 * Math.Acos(w));
            C.x = x - B.x;
            C.y = y - B.y;
            C.z = z - B.z;
            C.w = w - B.w;
            return C;
        }

        internal void Write()
        {
            //if (sw == null)
            //    sw = File.CreateText(filename);
        }

        internal void Close()
        {
            if (sw != null)
                sw.Close();
        }

        internal void Write(StreamWriter sw)
        {
            sw.WriteLine(x + " " + y + " " + z + " " + w);
        }

        internal void WriteAngles(StreamWriter sw)
        {
                //atan2 (2*(q1*q2+q3*q4),1-2*(q2*q2+q3*q3))
                //asin (2*(q1*q3-q4*q2))
                //atan2 (2*(q1*q4+q2*q3),1-2*(q3*q3+q4*q4))

            Kvaternion T = this.normalize();

            double a1 = Math.Atan2(2 * (T.x * T.y + T.z * T.w), 
                                     1 - 2 * (T.y * T.y + T.z * T.z));

            double a2;
            a2 = Math.Asin(2 * (T.x * T.z - T.w * T.y));

            double a3 = Math.Atan2(2 * (T.x * T.w + T.y * T.z), 
                                   1 - 2 * (T.z * T.z + T.w * T.w));


            sw.WriteLine(a1 + " " + a2 + " " + a3);
        }
    }


}
