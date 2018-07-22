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

namespace Ivy.Measures.Quantities
{
    using NUnit.Framework;

    [TestFixture]
    public class AbsorbedDoseTests
    {
        #region Unit tests

        [Test]
        public void AbsorbedDose_StandardUnit_IsGray()
        {
            var expected = AbsorbedDose.Gray;
            var actual = new AbsorbedDose().StandardUnit;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LessThan_CompareTwoInstances_YieldsValidComparisonResult()
        {
            var lhs = new AbsorbedDose(amount: 1.0, unit: AbsorbedDose.CentiGray);
            var rhs = new AbsorbedDose(amount: 0.02);
            Assert.IsTrue(lhs < rhs);
        }

        #endregion
    }
}