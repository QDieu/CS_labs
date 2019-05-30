using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace lab_1
{
    enum StatusOfFile{
        NoneEof,
        FirstEof,
        SecondEof,
        BothEof
    }
    class Program
    {
        private const int EOF = -1;
        
        //Check for difference
        public static long getPosition(ref FileStream stream1, ref FileStream stream2, out int firstByte, out int secondByte, ref StatusOfFile status){
            do{
                firstByte = stream1.ReadByte();
                secondByte = stream2.ReadByte();
            } while (firstByte == secondByte && firstByte != EOF && secondByte != EOF);

            if (firstByte == EOF && secondByte == EOF){
                status = StatusOfFile.BothEof;
            }
            else if ( firstByte == EOF){
                status = StatusOfFile.FirstEof;
                return stream2.Position;
            }
            else if (secondByte == EOF) {
                status = StatusOfFile.SecondEof;
                return stream1.Position;
            }
            return stream1.Position;

        }

        public static void PrintOffSet(int firstByte, int secondByte, bool side, bool text) {
            if (text) {
                char firstOut = (char)firstByte, secondOut = (char)secondByte;
                if (Char.IsControl(firstOut)) firstOut = '.';
                if (Char.IsControl(secondOut)) secondOut = '.';

                if (side) Console.Write("{0}|{1}", firstOut, secondOut);
                else Console.Write("{0}({1})", firstOut, secondOut);
            }
            else {
                if (side) Console.Write("0x{0:x}|0x{1:x}", firstByte, secondByte);
                else Console.Write("0x{0:x}(0x{1:x})", firstByte, secondByte);
            }
        }

        public static bool CheckNextDifferent(ref FileStream firstStream, ref FileStream secondStream, out int firstByte, out int secondByte, ref StatusOfFile status) {
            firstByte = firstStream.ReadByte();
            secondByte = secondStream.ReadByte();
            if (firstByte == EOF) {
                status = StatusOfFile.FirstEof;
            }
            if (secondByte == EOF) {
                status = StatusOfFile.SecondEof;
            }

            return !(firstByte == secondByte);
        }

        public static void PrintEndOfStream (ref FileStream stream, ref int count, ref StatusOfFile status, long max, int buf) {
            count++;
            while(count < max || buf != EOF) {
                buf = stream.ReadByte();
                if(buf != EOF) Console.Write("(0x{0:x})", buf);
                count++;
            }
            status = StatusOfFile.BothEof;
        }

        static void Main(string[] args){
            StatusOfFile status = StatusOfFile.NoneEof;

            if (args.Length < 2){
                Console.WriteLine("The programm requires two or more agruments");
                return;
            }
            FileStream firstStream = new FileStream(args[0], FileMode.Open);
            FileStream secondStream = new FileStream(args[1], FileMode.Open);
            long maxLengthFiles;
            if (args.Length >= 3)
               maxLengthFiles = Convert.ToInt64(args[3]);
            else if (firstStream.Length > secondStream.Length)
                maxLengthFiles = firstStream.Length;
            else maxLengthFiles = secondStream.Length;

            bool isDifferent = false;
            bool brief = false, text = false, side = false;

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(option => {
                if (option.Length > 0) {
                    Console.WriteLine("--length was set and ={0}", option.Length);
                    maxLengthFiles = option.Length;
                }
                if (option.Text) {
                    Console.WriteLine("--text was set");
                    text = true;
                }
                if (option.Side_by_side) {
                    Console.WriteLine("--side-by-side was set");
                    side = true;
                }
                if (option.Brief) {
                    Console.WriteLine("--brief was set");
                    brief = true;
                }
            });
            int firstByte = 0, secondByte = 0, count = 0;
            long position = 0;
            while(status != StatusOfFile.BothEof && count < maxLengthFiles){
                position = getPosition(ref firstStream, ref secondStream, out firstByte, out secondByte, ref status);
                bool offset = false;
                //Found a difference
                if (status == StatusOfFile.NoneEof) {
                    if (!isDifferent) {
                        isDifferent = true;
                    }

                    if (!brief) {
                        Console.Write("0x{0:x8}: ", position - 1);
                        offset = true;
                        PrintOffSet(firstByte, secondByte, side, text);
                    }

                    count++;

                    while (CheckNextDifferent(ref firstStream, ref secondStream, out firstByte, out secondByte, ref status) && status == StatusOfFile.NoneEof) {
                        position++;
                        if (!brief) PrintOffSet(firstByte, secondByte, side, text);
                        count++;
                    }
                }
                if(status == StatusOfFile.FirstEof || status == StatusOfFile.SecondEof) {
                    bool isFirst = false;
                    if (status == StatusOfFile.FirstEof) isFirst = true;

                    if (!offset && !brief) Console.Write("0x{0:x8}: ", position - 1);
                    if (!brief) {
                        if (!isFirst) {
                            Console.Write("0x{0:x}(<EOF>) ", firstByte);
                            PrintEndOfStream(ref firstStream, ref count, ref status, maxLengthFiles, firstByte);
                        }
                        else {
                            Console.Write("<EOF>(0x{0:x})", secondByte);
                            PrintEndOfStream(ref secondStream, ref count, ref status, maxLengthFiles, secondByte);
                        }
                    }
                }
                if (!brief) Console.WriteLine();
            }
            if (!isDifferent) Console.WriteLine("Files are identical");
            else if (brief && isDifferent) Console.WriteLine("Files are not identical");
        }
    }
}
