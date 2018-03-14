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

    [TestFixture]
    public class MeasureTests
    {
        #region Test Methods

        [Test]
        public void GetAmount_UsingIUnit_ValidConversion()
        {
            var expected = AmountConverter.ToAmountType(iValue: 500.0);
            var instance = new Measure<Length>(amount: 5.0, unit: Length.Meter);
            var actual = instance.GetAmount(Length.CentiMeter);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Indexer_SameQuantityNonGenericInterface_YieldsValidMeasureObject()
        {
            var expected = new Measure<Volume>(amount: 5000.0f, unit: Volume.Liter);
            IMeasure meas = new Volume(amount: 5.0f);
            var actual = meas[Volume.Liter];
            MeasureAssert.MeasuresAreEqual(expected, actual);
        }

        [Test]
        public void Indexer_DifferentQuantitiesNonGenericInterface_Throws()
        {
            IMeasure meas = new SpecificVolume(amount: 1.0);
           Assert.That(() =>
           {
               var throws = meas[Volume.CubicMeter];
           }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_WithNonReferenceUnit_InitializesMeasureInReferenceUnit()
        {
            var expected = new Measure<Time>(amount: 180.0, unit: Time.Second);
            var actual = new Time(amount: 3.0, unit: Time.Minute);
            MeasureAssert.MeasuresAreEqual(expected, actual);
        }

        [Test]
        public void DivisionOperator_DivideGenericSameQuantity_ReturnsScalar()
        {
            var expected = 1.0;
            var numerator = new Area(amount: 500.0, unit: Area.SquareCentiMeter);
            var denominator = new Area(amount: 5.0, unit: Area.SquareDeciMeter);
            var actual = (double)(numerator / denominator);
            Assert.AreEqual(expected, actual, delta: 1.0e-6);
        }

        [Test]
        public void GetAmountOfQuantity_UsingIUnit_ValidConversion()
        {
            var expected = AmountConverter.ToAmountType(iValue: 500.0);
            var instance = new Length(amount: 5.0);
            var actual = instance.GetAmount(Length.CentiMeter);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AreEqual_BothArgumentsHaveValuesValuesWithinTolerance_ReturnsTrue()
        {
            AmountConcentration lhs = new AmountConcentration(amount: 5.0);
            AmountConcentration rhs = new AmountConcentration(amount: 5.0001);
            AmountConcentration tol = new AmountConcentration(amount: 0.001);

            var expected = true;
            var actual = AreApproximatelyEqual(lhs, rhs, tol);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AreEqual_BothArgumentsHaveValuesValuesNotWithinTolerance_ReturnsFalse()
        {
            AmountConcentration lhs = new AmountConcentration(amount: 5.0);
            AmountConcentration rhs = new AmountConcentration(amount: 5.001);
            AmountConcentration tol = new AmountConcentration(amount: 0.0001);

            var expected = false;
            var actual = AreApproximatelyEqual(lhs, rhs, tol);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AreEqual_LhsArgumentHasValue_ReturnsFalse()
        {
            AmountConcentration lhs = new AmountConcentration(amount: 5.0);
            AmountConcentration rhs = null;
            AmountConcentration tol = AmountConcentration.Zero;

            var expected = false;
            var actual = AreApproximatelyEqual(lhs, rhs, tol);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AreEqual_RhsArgumentHasValue_ReturnsFalse()
        {
            AmountConcentration lhs = null;
            AmountConcentration rhs = new AmountConcentration(amount: 5.0);
            AmountConcentration tol = AmountConcentration.Zero;

            var expected = false;
            var actual = AreApproximatelyEqual(lhs, rhs, tol);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AreEqual_NoArgumentHasValue_ReturnsFalse()
        {
            AmountConcentration lhs = null;
            AmountConcentration rhs = null;
            AmountConcentration tol = AmountConcentration.Zero;

            var expected = false;
            var actual = AreApproximatelyEqual(lhs, rhs, tol);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Private support methods

        private static bool AreApproximatelyEqual<Q>(Q lhs, Q rhs, Q tol) where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            return lhs != null && rhs != null && Math.Abs(lhs.Amount - rhs.Amount) < tol.Amount;
        }

        #endregion
    }
}
