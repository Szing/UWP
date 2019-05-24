using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace NavDemo.Models
{
    class FireflyParticle
    {
        public FireflyParticle(Rect bound)
        {
            Point = new Point(ran.Next((int)bound.Width), ran.Next((int)bound.Height));
            _x = new Ran(Point.X, bound.Width, 0)
            {
                EasingFunction = true,
            };
            _y = new Ran(Point.Y, bound.Height, 0)
            {
                EasingFunction = true,
            };
            _radius = new Ran(ran.Next(2, 5), 5, 2)
            {
                Po = 0.71
            };
            Bound = bound;
        }

        public FireflyParticle()
        {
        }

        public void Time(TimeSpan time)
        {
            _radius.Time(time);
            _opColor.Time(time);
            _x.Time(time);
            _y.Time(time);

            Radius = _radius.Value;
            OpColor = _opColor.Value;
            Point = new Point(_x.Value, _y.Value);
        }

        public Point Point { get; set; }

        public Rect Bound
        {
            get { return _bound; }
            set
            {
                _bound = value;
                _x.Ma = value.Width;
                _y.Ma = value.Height;
            }
        }

        public double Radius { get; set; } = 10;
        public Color CenterColor { get; set; } = Color.FromArgb(255, 252, 203, 89);
        public double OpColor { set; get; } = 1;
        private static Random ran = new Random();

        private Ran _radius;
        private Ran _opColor = new Ran(1, 1, 0.001);

        private Ran _x;
        private Ran _y;
        private Rect _bound;
    }

}
