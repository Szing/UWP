using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Models
{
    class Ran
    {
        public Ran(double value, double ma, double mi)
        {
            Value = value;
            Ma = ma;
            Mi = mi;
            To = ran.NextDouble() * (Ma - Mi) + Mi;
        }

        public double Value { get; set; }
        public double To { get; set; }
        public double Dalue { get; set; }

        public double Ma { get; set; }

        public double Mi { get; set; }

        public bool EasingFunction { get; set; }

        /// <summary>
        /// 加速度
        /// </summary>
        public double Po { get; set; }

        public void Time(TimeSpan time)
        {

            if (Math.Abs(Dalue) < 0.000001)
            {
                if (Math.Abs(Po) < 0.0001)
                {
                    Dalue = Math.Abs(Value - To) / ran.Next(10, 300);
                }
                else
                {
                    Dalue = Po;
                }
            }
            //减数
            if (EasingFunction && Math.Abs(Value - To) < Dalue * 10/*如果接近*/)
            {
                Dalue /= 2;
                if (Math.Abs(Dalue) < 1)
                {
                    Dalue = 1;
                }
            }
            int n = 1;
            if (Value > To)
            {
                n = n * -1;
            }
            Value += n * Dalue * time.TotalSeconds * 2;
            if (n > 0 && Value >= To)
            {
                Value = To;
                To = ran.NextDouble() * (Ma - Mi) + Mi;
                Dalue = 0;
            }
            if (n < 0 && Value <= To)
            {
                Value = To;
                To = ran.NextDouble() * (Ma - Mi) + Mi;
                Dalue = 0;
            }
        }


        private static Random ran = new Random();
    }

}
