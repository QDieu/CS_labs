using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace lab5 {
    class Program {
        static void Main(string[] args) {
            MatrixFilter filter = new MatrixFilter(1);

            Bitmap BitmapImage = new Bitmap("image.jpg");
            Bitmap UnsafeImage = new Bitmap(BitmapImage);

            DateTime start = DateTime.Now;
            filter.ApplyFilterToBitmap(ref BitmapImage);
            DateTime finish = DateTime.Now;

            Console.WriteLine("Working time for getPixel/SetPixel =  {0}", finish - start);

            start = DateTime.Now;
            filter.applyFilterToBitmapUnsafe(ref UnsafeImage);
            finish = DateTime.Now;

            Console.WriteLine("Workint time for unsafe = {0}", finish - start);

            BitmapImage.Save("Bitmap.jpeg", ImageFormat.Jpeg);
            UnsafeImage.Save("Unsafe.jpeg", ImageFormat.Jpeg);

        }
    }
}
