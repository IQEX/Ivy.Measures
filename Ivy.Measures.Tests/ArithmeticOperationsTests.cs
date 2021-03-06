/*
 *  Copyright (c) 2011-2015, Cureos AB.
 *  All rights reserved.
 *  http://www.cureos.com
 *
 *	This file is part of CSUnits.
 *
 *  CSUnits is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as
 *  published by the Free Software Foundation, either version 3 of the
 *  License, or (at your option) any later version.
 *
 *  CSUnits is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with CSUnits. If not, see http://www.gnu.org/licenses/.
 */

namespace Ivy.Measures
{
    using System;

    using Ivy.Measures.Quantities;

    using NUnit.Framework;
    public class ExpectedException : Attribute { public ExpectedException(Type t){} }
    [TestFixture]
    public class ArithmeticOperationsTests
    {
        #region Unit tests

        [Test]
        public void Times_MultiplyAreaAndLength_ReturnsVolume()
        {
            var expected = new Volume(amount: 6.0);
            var lhs = new Area(amount: 2.0);
            var rhs = new Length(amount: 3.0);
            Volume actual; ArithmeticOperations.Times(lhs, rhs, out actual);
            MeasureAssert.MeasuresAreEqual(expected, actual);
        }

        [Test]
        public void Times_MultiplyAreaAndAreaToVolume_Throws()
        {
            var lhs = new Area(amount: 2.0);
            var rhs = new Area(amount: 3.0);
            Volume throws;
            Assert.That(() => ArithmeticOperations.Times(lhs, rhs, out throws),
               Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Divide_DivideVolumeAndLength_ReturnsArea()
        {
            var expected = new Area(amount: 4.0);
            var numerator = new Volume(amount: 8.0);
            var denominator = new Length(amount: 200.0, unit: Length.CentiMeter);
            Area actual; ArithmeticOperations.Divide(numerator, denominator, out actual);
            MeasureAssert.MeasuresAreEqual(expected, actual);
        }

        [Test]
        public void Divide_DivideAreaAndAreaToLength_Throws()
        {
            var numerator = new Area(amount: 8.0);
            var denominator = new Area(amount: 200.0, unit: Area.SquareDeciMeter);
            Length throws;
            Assert.That(() => ArithmeticOperations.Divide(numerator, denominator, out throws),
             Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Power_LengthRaisedWith3_ReturnsVolume()
        {
            var expected = new Volume(amount: 1.0, unit: Volume.CubicDeciMeter);
            var len = new Measure<Length>(amount: 1.0, unit: Length.DeciMeter);
            Volume actual; ArithmeticOperations.Power(len, iExponent: 3, oResult: out actual);
            MeasureAssert.AmountsAreEqual(expected, actual);
        }

        #endregion
    }
}
