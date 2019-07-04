using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4 {
    class Program {
        static void Main(string[] args) {
            BitContainer container = new BitContainer();
            container.PushBit(1);
            container.PushBit(1);
            container.PushBit(1);
            container.PushBit(0);
            container.PushBit(1);
            container.PushBit(0);
            container.PushBit(true);
            container.PushBit(true);
            Console.WriteLine(container);

            container[2] = 0;
            container[1] = 0;
            Console.WriteLine(container);

            container.Insert(2, 1);
            Console.WriteLine(container);

            container.Remove(2);
            Console.WriteLine(container);

            foreach (var i in container) {
                Console.Write(i);
                Console.Write(" ");
            }
        }
    }
}
