using System;
using System.Collections.Generic;
using System.Text;

namespace Study.LabWork1.Features.Task1
{
    public class RationalNumber
    {
        public int Numerator { get; }

        public int Denominator { get; }

        public RationalNumber(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю.");

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            int gcd = GCD(Math.Abs(numerator), denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public override string ToString()
        {
            if (Denominator == 1)
                return Numerator.ToString();
            else
                return $"{Numerator}/{Denominator}";
        }
        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            int num = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int den = a.Denominator * b.Denominator;
            return new RationalNumber(num, den);
        }

        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            int num = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            int den = a.Denominator * b.Denominator;
            return new RationalNumber(num, den);
        }

        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            int num = a.Numerator * b.Numerator;
            int den = a.Denominator * b.Denominator;
            return new RationalNumber(num, den);
        }

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            if (b.Numerator == 0)
                throw new DivideByZeroException("Деление на ноль невозможно.");

            int num = a.Numerator * b.Denominator;
            int den = a.Denominator * b.Numerator;
            return new RationalNumber(num, den);
        }
        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }

        public static bool operator !=(RationalNumber a, RationalNumber b)
        {
            return !(a == b);
        }

        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
        }

        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
        }

        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            return a < b || a == b;
        }

        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            return a > b || a == b;
        }
        public override bool Equals(object obj)
        {
            if (obj is RationalNumber other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }
    }
}
