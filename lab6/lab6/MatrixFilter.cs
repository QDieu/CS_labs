using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace lab6 {

    class MatrixFilter {
        private int radius_;
        private double[,] kernel;

        public MatrixFilter(int radius) {
            radius_ = radius;
            int K = 2 * radius_ + 1;
            kernel = new double[K, K];
            GaussBlurCore(K);
        }

        private double Gaussian(double x, double y, double sigma) {
            return Math.Exp(-(x * x + y * y) / 2 * sigma * sigma) / (2 * Math.PI * Math.Pow(sigma, 2.0));
        }

        private void GaussBlurCore(int k) {
            double sigma = 1;
            double sum = 0.0;
            for (int x = 0; x < k; ++x)
                for (int y = 0; y < k; ++y) {
                    kernel[x, y] = Gaussian(x - radius_, y - radius_, sigma);
                    sum += kernel[x, y];
                }
            for (int x = 0; x < k; ++x)
                for (int y = 0; y < k; ++y) kernel[x, y] /= sum;

        }

        public void WriteKernel() {
            int K = 2 * radius_ + 1;
            for (int i = 0; i < K; ++i) {
                for (int j = 0; j < K; ++j)
                    Console.Write("{0:F6}", kernel[i, j]);
                Console.WriteLine();
            }
        }

        private Color ApplyFilterToPixel(int x, int y, Bitmap image) {
            int N = image.Width, M = image.Height, n, m;
            byte R = 0, G = 0, B = 0;
            for (int i = y - radius_; i <= y + radius_; ++i) {
                if (i < 0) m = 0;
                else if (i >= M) m = M - 1;
                else m = i;
                for (int j = x - radius_; j <= x + radius_; ++j) {
                    if (j < 0) n = 0;
                    else if (j >= N) n = N - 1;
                    else n = j;

                    Color pixel = image.GetPixel(n, m);
                    R += (byte)(pixel.R * kernel[i - y + radius_, j - x + radius_]);
                    G += (byte)(pixel.G * kernel[i - y + radius_, j - x + radius_]);
                    B += (byte)(pixel.B * kernel[i - y + radius_, j - x + radius_]);
                }
            }

            return Color.FromArgb(R, G, B);
        }


        public void ApplyFilterToBitmap(ref Bitmap image) {
            for (int x = 0; x < image.Width; ++x)
                for (int y = 0; y < image.Height; ++y) {
                    Color newPixel = ApplyFilterToPixel(x, y, image);
                    image.SetPixel(x, y, newPixel);
                }
        }

        private unsafe void ApplyFilterUnsafeToPixel(int x, int y, int bytesPerPixel, int heightPixels, int widthInBytes, byte* firstPxlAdr, byte* currLine, int stride) {
            int N = widthInBytes, M = heightPixels, n, m;
            byte R = 0, G = 0, B = 0;
            for (int i = y - radius_; i <= y + radius_; ++i) {
                if (i < 0) m = 0;
                else if (i >= M) m = M - 1;
                else m = i;
                for (int j = x - radius_ * bytesPerPixel; j <= x + radius_ * bytesPerPixel; j += bytesPerPixel) {
                    if (j < 0) n = 0;
                    else if (j >= N) n = N - bytesPerPixel;
                    else n = j;

                    Color pixel = Color.FromArgb(firstPxlAdr[n + m * stride + 2], firstPxlAdr[n + m * stride + 1], firstPxlAdr[n + m * stride]);
                    R += (byte)(pixel.R * kernel[i - y + radius_, (j - x) / bytesPerPixel + radius_]);
                    G += (byte)(pixel.G * kernel[i - y + radius_, (j - x) / bytesPerPixel + radius_]);
                    B += (byte)(pixel.B * kernel[i - y + radius_, (j - x) / bytesPerPixel + radius_]);
                }
            }

            currLine[x + 2] = R;
            currLine[x + 1] = G;
            currLine[x] = B;
        }

        private unsafe void RunRow(int y, int bytesPerPixel, int heightPixels, int widthInBytes, byte* firstPxlAdr, byte* currLine, int stride) {
            for (var x = 0; x < widthInBytes; x += bytesPerPixel)
                ApplyFilterUnsafeToPixel(x, y, bytesPerPixel, heightPixels, widthInBytes, firstPxlAdr, currLine, stride);
        }


        public void ApplyFilterToBitmapUnsafe(ref Bitmap processedBitmap, bool isAsync) {
            unsafe {
                BitmapData bitmapData = processedBitmap.LockBits(
                    new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height),
                    ImageLockMode.ReadWrite, processedBitmap.PixelFormat
                );
                int bytesPerPixel = Image.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0; //адрес данных первого пикселя

                if (isAsync) {
                    var taskArray = new Task[heightInPixels];
                    for (var i = 0; i < taskArray.Length; i++) {
                        var y = i;
                        byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                        taskArray[y] = Task.Run(
                            () => RunRow(y, bytesPerPixel, heightInPixels, widthInBytes, ptrFirstPixel, currentLine, bitmapData.Stride)
                            );
                    }

                    Task.WaitAll(taskArray);
                }
                else {
                    for (int y = 0; y < heightInPixels; ++y) {
                        //stride -- это ширина шага, необходимого для перехода на новую строку изображения
                        byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                            // calculate new pixel value
                            ApplyFilterUnsafeToPixel(x, y, bytesPerPixel, heightInPixels, widthInBytes, ptrFirstPixel,
                                                      currentLine, bitmapData.Stride);
                    }
                }
                processedBitmap.UnlockBits(bitmapData);
            }
        }

    }
}
