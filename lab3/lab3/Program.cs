using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatioLibrary;

namespace lab3 {
    class Program {
        static void Main(string[] args) {
            Ratio x = new Ratio(2, 5);
            Ratio y = new Ratio(3, 8);
            Console.WriteLine("x.toString and x.toDouble : {0} or {1}", x.ToString(), x.ToDouble());
            Console.WriteLine("y.toString and y.toDouble : {0} or {1}", y.ToString(), y.ToDouble());
            Console.WriteLine("x+y : {0}+{1} = {2}", x.ToString(), y.ToString(), (x + y).ToString());
            Console.WriteLine("x-y : {0}-{1} = {2}", x.ToString(), y.ToString(), (x - y).ToString());
            Console.WriteLine("x*y : {0}*{1} = {2}", x.ToString(), y.ToString(), (x * y).ToString());
            Console.WriteLine("x/y :{0} / {1} = {2}", x.ToString(), y.ToString(), (x / y).ToString());
        }
    }
}
