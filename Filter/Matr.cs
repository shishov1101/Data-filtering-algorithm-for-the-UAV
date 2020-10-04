using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filter
{
    class Matr
    {
        public double[,] mas;
        public int m;// количество строк 
        public int n;// количество столбцов

        public Matr(int m1, int n1)
        {
            m = m1;
            n = n1;
            mas = new double[m, n];
        }

        public void Fill(Kvaternion q) 
        {
            mas[0, 0] = -2 * q[3];
            mas[0, 1] = 2 * q[4];
            mas[0, 2] = -2 * q[1];
            mas[0, 3] = 2 * q[2];
            mas[1, 0] = 2 * q[2];
            mas[1, 1] = 2 * q[1];
            mas[1, 2] = 2 * q[4];
            mas[1, 3] = 2 * q[3];
            mas[2, 1] = -4 * q[2];
            mas[2, 2] = -4 * q[3];
        }

        public Kvaternion mult(TVector b)
        {
            Kvaternion C = new Kvaternion(new TVector(1, 1, 1), 
                Math.PI/2);

            //C.x = mas[0, 3] * b.x + mas[1, 3] * b.y + mas[2, 3] * b.z;
            C.setx(mas[0, 0] * b.x + mas[0,1] * b.y + mas[0, 2] * b.z);
            //C.y = mas[0, 2] * b.x + mas[1, 2] * b.y + mas[2, 2] * b.z;
            C.sety( mas[1, 0] * b.x + mas[1, 1] * b.y + mas[1, 2] * b.z);
            
            C.setz (mas[2, 0] * b.x + mas[2,1] * b.y + mas[2, 2] * b.z);

            C.setw(mas[3, 0] * b.x + mas[3, 1] * b.y + mas[3, 2] * b.z);
            return C;
        }

        public Matr Trans()
        {
            Matr L = new Matr(n, m);
            for (int i = 0; i < m; i++)
            {
                for (int g = 0; g < n; g++)
                {
                    L.mas[g,i] = mas[i,g];
                }
            }
            return L;
        }

    }
}
