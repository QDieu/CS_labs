using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace lab6 {
    class Program {
        static void Main(string[] args) {
            MatrixFilter filter = new MatrixFilter(25);

            Bitmap BitmapImage = new Bitmap("image.jpg");
            Bitmap UnsafeImage = new Bitmap(BitmapImage);
            Bitmap UnsafeImageThread = new Bitmap(BitmapImage);

            DateTime start = DateTime.Now;
            filter.ApplyFilterToBitmap(ref BitmapImage);
            DateTime finish = DateTime.Now;

            Console.WriteLine("Working time for getPixel/SetPixel =  {0}", finish - start);

            start = DateTime.Now;
            filter.ApplyFilterToBitmapUnsafe(ref UnsafeImage,false);
            finish = DateTime.Now;

            Console.WriteLine("Workint time for unsafe = {0}", finish - start);

            start = DateTime.Now;
            filter.ApplyFilterToBitmapUnsafe(ref UnsafeImageThread, true);
            finish = DateTime.Now;

            Console.WriteLine("Workint time for thread = {0}", finish - start);

            BitmapImage.Save("Bitmap.jpeg", ImageFormat.Jpeg);
            UnsafeImage.Save("Unsafe.jpeg", ImageFormat.Jpeg);
            UnsafeImageThread.Save("UnsafeThread.jpeg", ImageFormat.Jpeg);
        }
    }
}
