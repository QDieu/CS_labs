using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    struct Point{
        public double x;
        public double y;
        public Point(double x, double y){
            this.x = x;
            this.y = y;
        }
    }
    abstract class Shape{
        abstract public double calculateArea();
        abstract public double calculatePerimeter();
        abstract public Point calculateCenterOfMass();

        protected double Square { get; set; }
        protected double Perimeter { get; set; }
        protected Point CenterOfMass { get; set; }

    }
}
