using System;
using System.Collections.Generic;
using System.Text;
using Study.LabWork1.Features.Task1;

namespace Study.LabWork1.UnitTests.Features.Task1
{
    [TestFixture]
    public class RationalNumberTests
    {
        [Test]
        [TestCase(5, 10, 1, 2)]
        [TestCase(-4, 8, -1, 2)]
        [TestCase(3, -9, -1, 3)]
        [TestCase(0, 5, 0, 1)]
        public void Constructor_ReducesFraction(int num, int den, int expectedNum, int expectedDen)
        {
            var r = new RationalNumber(num, den);

            Assert.That(r.Numerator, Is.EqualTo(expectedNum));
            Assert.That(r.Denominator, Is.EqualTo(expectedDen));
        }

        [Test]
        public void Constructor_DenominatorZero_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new RationalNumber(1, 0));
        }

        [Test]
        [TestCase(1, 2, 1, 3, 5, 6)]
        [TestCase(-1, 4, 3, 4, 1, 2)]
        public void Addition_Works(int n1, int d1, int n2, int d2, int en, int ed)
        {
            var a = new RationalNumber(n1, d1);
            var b = new RationalNumber(n2, d2);
            var result = a + b;

            Assert.That(result.Numerator, Is.EqualTo(en));
            Assert.That(result.Denominator, Is.EqualTo(ed));
        }

        [Test]
        [TestCase(3, 4, 1, 4, 1, 2)]
        [TestCase(1, 4, 3, 4, -1, 2)]
        public void Subtraction_Works(int n1, int d1, int n2, int d2, int en, int ed)
        {
            var a = new RationalNumber(n1, d1);
            var b = new RationalNumber(n2, d2);
            var result = a - b;

            Assert.That(result.Numerator, Is.EqualTo(en));
            Assert.That(result.Denominator, Is.EqualTo(ed));
        }

        [Test]
        [TestCase(2, 3, 3, 4, 1, 2)]
        [TestCase(-1, 2, 1, 3, -1, 6)]
        public void Multiplication_Works(int n1, int d1, int n2, int d2, int en, int ed)
        {
            var a = new RationalNumber(n1, d1);
            var b = new RationalNumber(n2, d2);
            var result = a * b;

            Assert.That(result.Numerator, Is.EqualTo(en));
            Assert.That(result.Denominator, Is.EqualTo(ed));
        }

        [Test]
        [TestCase(2, 3, 1, 2, 4, 3)]
        [TestCase(3, 4, 2, 1, 3, 8)]
        public void Division_Works(int n1, int d1, int n2, int d2, int en, int ed)
        {
            var a = new RationalNumber(n1, d1);
            var b = new RationalNumber(n2, d2);
            var result = a / b;

            Assert.That(result.Numerator, Is.EqualTo(en));
            Assert.That(result.Denominator, Is.EqualTo(ed));
        }

        [Test]
        public void Division_ByZero_ThrowsException()
        {
            var a = new RationalNumber(1, 2);
            var b = new RationalNumber(0, 1);

            Assert.Throws<DivideByZeroException>(() => { var r = a / b; });
        }

        [Test]
        [TestCase(1, 2, 2, 4, true)]
        [TestCase(1, 3, 2, 3, false)]
        public void Equality_Works(int n1, int d1, int n2, int d2, bool expected)
        {
            var a = new RationalNumber(n1, d1);
            var b = new RationalNumber(n2, d2);

            Assert.That(a == b, Is.EqualTo(expected));
            Assert.That(a != b, Is.EqualTo(!expected));
        }

        [Test]
        [TestCase(1, 2, 3, 4, true)]
        [TestCase(3, 4, 1, 2, false)]
        public void Comparison_Works(int n1, int d1, int n2, int d2, bool less)
        {
            var a = new RationalNumber(n1, d1);
            var b = new RationalNumber(n2, d2);

            Assert.That(a < b, Is.EqualTo(less));
            Assert.That(a > b, Is.EqualTo(!less));
        }

        [Test]
        [TestCase(1, 2, "1/2")]
        [TestCase(5, 1, "5")]
        [TestCase(0, 5, "0")]
        public void ToString_Works(int num, int den, string expected)
        {
            var r = new RationalNumber(num, den);

            Assert.That(r.ToString(), Is.EqualTo(expected));
        }
    }
}
