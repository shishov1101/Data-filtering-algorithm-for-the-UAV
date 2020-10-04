using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filter
{
    class Gyro : Sensor
    {
        public Gyro() : base()// Sensor()
        {
        }

        public Gyro(TVector rotate, Double angle)
            : base(rotate, angle)
        {
        }

        public Kvaternion getKvaternion()
        {
            return L;
        }
    }
    class Accel : Sensor
    {
        public Accel()
            : base()// Sensor()
        {
        }

        public Accel(TVector rotate, Double angle)
            : base(rotate, angle)
        {
        }

        public Kvaternion getKvaternion()
        {
            return L;
        }
    }

}
