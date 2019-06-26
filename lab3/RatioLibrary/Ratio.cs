using System;

namespace RatioLibrary {
    public class DenominatorZeroException : ArgumentException {
        public DenominatorZeroException(string message) : base(message) { }
    }

    public class Ratio {
        private int Numerator { get; set; }
        private int Denominator { get; set; }

        private static int GCD(int a, int b) {
            return (b == 0) ? a : GCD(b, a % b);
        }

        public Ratio(int numerator, int denominator) {
            if (denominator == 0) throw new DenominatorZeroException("Error : Denominator equals zero");
            int gcd = GCD(numerator, denominator);
            
            if (gcd > 1) {
                numerator /= gcd;
                denominator /= gcd;
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        public static Ratio operator+ (Ratio r) {
            return new Ratio(r.Numerator, r.Denominator);
        }

        public static Ratio operator+ (Ratio r1, Ratio r2) {
            int gcd = GCD(r1.Denominator, r2.Denominator);
            int newNumerator = r1.Numerator * (r2.Denominator / gcd) + r2.Numerator * (r1.Denominator / gcd);
            int newDenominator = r1.Denominator * r2.Denominator / gcd;
            return new Ratio(newNumerator, newDenominator);
        }

        public static Ratio operator -(Ratio r) {
            return new Ratio(r.Numerator, r.Denominator);
        }

        public static Ratio operator -(Ratio r1, Ratio r2) {
            int gcd = GCD(r1.Denominator, r2.Denominator);
            int newNumerator = r1.Numerator * (r2.Denominator / gcd) - r2.Numerator * (r1.Denominator / gcd);
            int newDenominator = r1.Denominator * r2.Denominator / gcd;
            return new Ratio(newNumerator, newDenominator);
        }

        public static Ratio operator *(Ratio r1, Ratio r2) {
            int gcd = GCD(r1.Numerator, r2.Denominator) * GCD(r2.Numerator, r1.Denominator);

            int newNumerator = (r1.Numerator * r2.Numerator) / gcd;
            int newDenominator = (r1.Denominator * r2.Denominator) / gcd;

            return new Ratio(newNumerator, newDenominator);
        }

        public static Ratio operator /(Ratio r1, Ratio r2) {
            if (r2.Numerator == 0) throw new DenominatorZeroException("Error : Denominator equals zero");
            return r1 * new Ratio(r2.Denominator, r2.Numerator);
        }

        public double ToDouble() {
            if (Denominator == 0) throw new DenominatorZeroException("Error : Denominator equals zero");
            return (double)Numerator / Denominator;
        }

        public override string ToString() {
            return string.Format("{0}/{1}", Numerator, Denominator);
        }
    }
}
