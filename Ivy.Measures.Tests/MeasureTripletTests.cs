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
    using Ivy.Measures.Quantities;

    using NUnit.Framework;

    [TestFixture]
    public class MeasureTripletTests
    {
        #region Fields

        private MeasureTriplet<Time, Power, ElectricPotential> _instance;

        #endregion

        #region SetUp and TearDown

        [SetUp]
        public void Setup()
        {
            _instance = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 5.0, iAmount2: 0.12, iAmount3: 0.6);
        }

        [TearDown]
        public void Teardown()
        {
        }

        #endregion

        #region Unit tests

        [Test]
        public void PlusOperator_AddInstanceWithItself_ReturnsDoubleInstance()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 10.0, iAmount2: 0.24, iAmount3: 1.2);
            var actual = _instance + _instance;
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MinusOperator_SubtractInstanceWithItself_ReturnsZero()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 0.0, iAmount2: 0.0, iAmount3: 0.0);
            var actual = _instance - _instance;
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TimesOperator_MultiplyBy2And3And4_ReturnsDoubleTimeThreeTimesPowerFourTimesPotential()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 10.0, iAmount2: 0.36, iAmount3: 2.4);
            var actual = _instance * new MeasureTriplet<Number, Number, Number>(iAmount1: 2.0, iAmount2: 3.0, iAmount3: 4.0);
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideOperator_DivideBy2And3And4_ReturnsHalfTimeOneThirdPowerOneFourthPotential()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 2.5, iAmount2: 0.04, iAmount3: 0.15);
            var actual = _instance / new MeasureTriplet<Number, Number, Number>(iAmount1: 2.0, iAmount2: 3.0, iAmount3: 4.0);
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideOperator_DivideByItself_ReturnsUnityDoublet()
        {
            var expected = new MeasureTriplet<Number, Number, Number>(iAmount1: 1.0, iAmount2: 1.0, iAmount3: 1.0);
            var actual = _instance / _instance;
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TimesOperator_MultiplyScalarRhs_AllMeasuresEquallyScaled()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 15.0, iAmount2: 0.36, iAmount3: 1.8);;
            var actual = _instance * 3.0f;
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TimesOperator_MultiplyScalarLhs_AllMeasuresEquallyScaled()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 15.0, iAmount2: 0.36, iAmount3: 1.8); ;
            var actual = 3.0f * _instance;
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TimesOperator_MultiplyNumberLhs_AllMeasuresEquallyScaled()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 15.0, iAmount2: 0.36, iAmount3: 1.8); ;
            var actual = _instance * new Number(amount: 3.0);
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TimesOperator_MultiplyNumberRhs_AllMeasuresEquallyScaled()
        {
            var expected = new MeasureTriplet<Time, Power, ElectricPotential>(iAmount1: 15.0, iAmount2: 0.36, iAmount3: 1.8); ;
            var actual = new Number(amount: 3.0) * _instance;
            IMeasureTripletAssert.AreEqual(expected, actual);
        }

        [Test]
        public void IMeasureXAccessor_CheckReturnType_ShouldBeIMeasure()
        {
            var expected = typeof(IMeasure<Time>);
            IMeasureTriplet<Time, Power, ElectricPotential> triplet = _instance;
            var actual = triplet.X;
            Assert.IsInstanceOf(expected, actual);
        }

        #endregion
    }
}
