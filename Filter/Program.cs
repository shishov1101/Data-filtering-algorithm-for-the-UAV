using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Filter
{
    class Program
    {
        static void Main(string[] args)
        {
            Sensor Gyro = new Sensor("gyro.csv");   // Cоздаем датчик гироскопа
            Sensor Accel = new Sensor("accel.csv"); // Создаем акселерометр 

            // Летательный аппарат 
            Kvaternion L = new Kvaternion(new TVector(1, 1, 1), Math.PI / 2);
            StreamWriter sw = File.CreateText("Kvaternion.txt");

            Double t0=0,tk=0.8,dt=0.01;
            Double Beta = 1;
            Double T=t0;

            while (T<tk)
            {
                // Взять показания гироскопа 
                Kvaternion G = Gyro.getKvaternion().normalize();
                // Взять показания акселерометра
                Kvaternion A = Accel.getKvaternion().normalize();

                //Угловые скорости
                Kvaternion S = L.mult(G).scale(0.5);

                TVector K = new TVector(0, 0, 0);
                K.Fill(G, A);
                Matr J = new Matr(3, 4);
                J.Fill(G);

                // Кватернион градиент F 
                Kvaternion W = J.Trans().mult(K).normalize();

                // Шаг алгоритма
                S = S.Minus(W.scale(Beta));
                L = L.Plus(S.scale(dt));
                Console.WriteLine("G=" + G.ToString());
                Console.WriteLine("L=" + L.ToString());
                Console.WriteLine("S=" + S.ToString());
                // Сохранение результатов и переход на следующий шаг
               // L.Write(sw);
                L.WriteAngles(sw);
                T=T+dt;
            }
            sw.Close(); // Закрытие файла результата
        }
    }
}
