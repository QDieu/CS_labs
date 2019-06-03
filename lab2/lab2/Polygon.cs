using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    class Polygon : Shape {
        private int Count { get; set; }
        private List<Point> list;

        public Polygon(int count, List<Point> points) {
            this.Count = count;
            list = new List<Point>(points);
            list.Add(list.First());

            Perimeter = calculatePerimeter();
            Square = calculateArea();
            CenterOfMass = calculateCenterOfMass();
        }



        public override double calculateArea() {
            double result = 0;
            Point[] tempArray = list.ToArray();
            for (int i = 0; i < list.Count - 1; ++i)
                result += ((tempArray[i + 1].x - tempArray[i].x) * (tempArray[i + 1].y + tempArray[i].y));
            return Math.Abs(result / 2);
        }

        public override Point calculateCenterOfMass() {
            Point result = new Point(0, 0);
            for (int i = 0; i < list.Count - 1; ++i) {
                result.x += Math.Sqrt(Math.Pow(list[i].x - list[i+1].x, 2) + Math.Pow(list[i].y - list[i+1].y, 2)) * (list[i].x + list[i + 1].x) / 2;
                result.y += Math.Sqrt(Math.Pow(list[i].x - list[i + 1].x, 2) + Math.Pow(list[i].y - list[i + 1].y, 2)) * (list[i].y + list[i + 1].y) / 2;
            }
            return result;
        }

        public override double calculatePerimeter() {
            double result = 0;
            Point[] tempArray = list.ToArray();
            for (int i = 0; i < list.Count - 1; ++i)
                result += Math.Sqrt(Math.Pow(Math.Abs(tempArray[i+1].x - tempArray[i].x), 2) + Math.Pow(Math.Abs(tempArray[i + 1].y - tempArray[i].y), 2));
            return Math.Abs(result / 2);
        }

        public override string ToString() {
            StringBuilder temp = new StringBuilder();
            return temp.AppendFormat("Polygon: Area = {0}, Perimeter = {1}, Center of Mass in ({2},{3})", Square, Perimeter, CenterOfMass.x, CenterOfMass.y).ToString();
        }
    }
}
