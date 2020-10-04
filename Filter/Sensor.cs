using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Filter
{
    class Sensor
    {
        protected Kvaternion L;
        string myFile;
        StreamReader sr;

        public Sensor(TVector rotate, Double angle)
        {
            L = new Kvaternion(rotate, angle);
        }

        public Sensor(string Filename)
        {
            TVector A=new TVector(1,1,1);
            L = new Kvaternion(A, Math.PI / 2);
            myFile = Filename;
            sr = File.OpenText(myFile);
        }

        public Kvaternion getKvaternion()
        {
            string File1 = sr.ReadLine();
            string[] S = File1.Split(',');
            L.setx(Convert.ToDouble(S[0]));
            L.sety(Convert.ToDouble(S[1]));
            L.setz(Convert.ToDouble(S[2]));
            return L;
        }
    }
}
