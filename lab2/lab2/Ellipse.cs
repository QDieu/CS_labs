using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    class Ellipse : Shape {
        private Point FirstFocus { get; set; }
        private Point SecondFocus { get; set; }
        private double BigHalfAxis { get; set; }
        private double SmallHalfAxis { get; set; }
        private double HalfFocusDistance { get; set; }
        private double e { get; set; }

        public Ellipse(Point FirstFocus, Point SecondFocus, double BigHalfAxis) {
            this.FirstFocus = FirstFocus;
            this.SecondFocus = SecondFocus;
            this.BigHalfAxis = BigHalfAxis;

            HalfFocusDistance = Math.Sqrt(Math.Pow(Math.Abs(FirstFocus.x - SecondFocus.x), 2) + Math.Pow(Math.Abs(FirstFocus.y - SecondFocus.y), 2)) / 2;
            e = HalfFocusDistance / BigHalfAxis;
            SmallHalfAxis = BigHalfAxis * Math.Sqrt(1 - Math.Pow(e, 2));

            Perimeter = calculatePerimeter();
            Square = calculateArea();
            CenterOfMass = calculateCenterOfMass();

        }

        public override double calculateArea() {
            return Math.PI * BigHalfAxis * SmallHalfAxis;
        }

        public override Point calculateCenterOfMass() {
            return new Point(FirstFocus.x + (SecondFocus.x - FirstFocus.x), FirstFocus.y + (SecondFocus.y - FirstFocus.y));
            
        }

        public override double calculatePerimeter() {
            double temp = Math.Pow(BigHalfAxis - SmallHalfAxis, 2);
            return 4 * (Math.PI * BigHalfAxis * SmallHalfAxis + temp) / (BigHalfAxis + SmallHalfAxis);
        }

        public override string ToString() {
            StringBuilder temp = new StringBuilder();
            return temp.AppendFormat("Ellipse: Area = {0}, Perimeter = {1}, Center of Mass in ({2},{3})", Square, Perimeter, CenterOfMass.x, CenterOfMass.y).ToString();
        }
    }
}
