using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Init() {
            List<Shape> shapes = new List<Shape>();
            PrintHelp();
            while (true) {
                char key = Console.ReadKey().KeyChar;

                switch (key) {
                    case 'c': {
                            CreateShape(shapes);
                            break;
                        }
                    case 'l': {
                            PrintShape(shapes);
                            break;
                        }
                    case 'h': {
                            PrintHelp();
                            break;
                        }
                    case 'q': {
                            return;
                        }
                    default: {
                            Console.WriteLine("Wrong Input");
                            break;
                        }
                }
            }
        }

        private static void PrintHelp() {
            Console.WriteLine();
            Console.WriteLine("C - Create a shape");
            Console.WriteLine("L - print a list of shapes with their attributes");
            Console.WriteLine("H - help");
            Console.WriteLine("Q - exit of program");
        }

        private static void CreateShape(List<Shape> shapes) {
            Console.WriteLine();
            
            while (true) {
                Console.WriteLine("Choose a shape to create: E - Ellipse, C - Circle, P - Polygon, Q - to cancel");
                char key = Console.ReadKey().KeyChar;
                switch (key) {
                    case 'e': {
                            CreateEllipse(shapes);
                            return;
                        }
                    case 'c': {
                            CreateCircle(shapes);
                            return;
                        }
                    case 'p': {
                            CreatePolygon(shapes);
                            return;
                        }
                    case 'q': {
                            return;
                        }
                    default: {
                            Console.WriteLine("Wrong Input");
                            break;
                        }
                }
            }
        }

        private static void PrintShape(List<Shape> shapes) {
            Console.WriteLine();
            foreach (var item in shapes) Console.WriteLine(item.ToString());
        }

        private static void CreateEllipse(List<Shape> shapes) {
            Point FirstPoint, SecondPoint;
            Console.WriteLine();
            Console.WriteLine("Input coordinates of focuses and length big half-axis in a format x1 y1 x2 y2 length");
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"-?[0-9]\.?[0-9]*");
            MatchCollection matches = regForPoint.Matches(line);
            if (matches.Count == 5) {
                FirstPoint = new Point(Convert.ToDouble(matches[0].Value), Convert.ToDouble(matches[1].Value));
                SecondPoint = new Point(Convert.ToDouble(matches[2].Value), Convert.ToDouble(matches[3].Value));
                double axes = Convert.ToDouble(matches[4].Value);
                shapes.Add(new Ellipse(FirstPoint, SecondPoint, axes));
            }
            else {
                Console.WriteLine("Wrong input");
            }
        }

        private static void CreateCircle(List<Shape> shapes) {
            Point center;
            Console.WriteLine();
            Console.WriteLine("Input coordinates of center and radius in a format x y radius");
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"-?[0-9]\.?[0-9]*");
            MatchCollection matches = regForPoint.Matches(line);
            if (matches.Count == 3) {
                center = new Point(Convert.ToDouble(matches[0].Value), Convert.ToDouble(matches[1].Value));
                double radius = Convert.ToDouble(matches[2].Value);
                shapes.Add(new Circle(center, radius));
            }
            else {
                Console.WriteLine("Wrong input");
            }
        }

        private static void CreatePolygon(List<Shape> shapes) {
            int n;
            List<Point> points = new List<Point>();
            Console.WriteLine();
            Console.WriteLine("Input a number of points in a polygon");
            do {
                n = Convert.ToInt32(Console.ReadLine());
            } while (n <= 2);
            Console.WriteLine("Input {0} coordinates in a format x1 y1 ... xn yn", n);
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"-?[0-9]\.?[0-9]*");
            MatchCollection matches = regForPoint.Matches(line);
            if (matches.Count == n*2) {
                for(int i = 0; i < matches.Count - 1; i += 2) {
                    points.Add(new Point(Convert.ToDouble(matches[i].Value), Convert.ToDouble(matches[i + 1].Value)));
                }
                shapes.Add(new Polygon(n,points));
            }
            else {
                Console.WriteLine("Wrong input");
            }
        }

        static void Main(string[] args)
        {
            Init();
        }
    }
}
