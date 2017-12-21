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
    using NUnit.Framework;

    public static class IMeasureTripletAssert
    {
        public static void AreEqual<Q1, Q2, Q3>(IMeasureTriplet<Q1, Q2, Q3> expected, IMeasureTriplet<Q1, Q2, Q3> actual)
            where Q1 : class, IQuantity<Q1>, new()
            where Q2 : class, IQuantity<Q2>, new()
            where Q3 : class, IQuantity<Q3>, new()
        {
            Assert.AreEqual(expected.X.Unit, actual.X.Unit);
            Assert.AreEqual(expected.Y.Unit, actual.Y.Unit);
            Assert.AreEqual(expected.Z.Unit, actual.Z.Unit);
            AmountAssert.AreEqual(expected.X.Amount, actual.X.Amount);
            AmountAssert.AreEqual(expected.Y.Amount, actual.Y.Amount);
            AmountAssert.AreEqual(expected.Z.Amount, actual.Z.Amount);
        }
    }
}