using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    class Circle : Shape {
        private Point Center { get; set; }
        private double Radius { get; set; }

        public Circle(Point center, double radius){
            this.Center = center;
            this.Radius = radius;

            Perimeter = calculatePerimeter();
            Square = calculateArea();
            CenterOfMass = calculateCenterOfMass();
        }

        public override double calculateArea() {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public override Point calculateCenterOfMass(){
            return Center;
        }

        public override double calculatePerimeter(){
            return Math.PI * Radius * 2;
        }
        public override string ToString() {
            StringBuilder temp = new StringBuilder();
            return temp.AppendFormat("Circle: Area = {0}, Perimeter = {1}, Center of Mass in ({2},{3})", Square, Perimeter, CenterOfMass.x, CenterOfMass.y).ToString();
        }
    }
}
