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

#if SINGLE
    using AmountType = System.Single;
#elif DECIMAL
    using AmountType = System.Decimal;
#elif DOUBLE
    using AmountType = System.Double;
#endif

    /// <summary>
    /// Representation of a physical unit of a specific quantity,
    /// where unit conversion is applied via defined functions.
    /// </summary>
    /// <typeparam name="Q">Quantity type with which the unit is associated</typeparam>
    public sealed class FunctionConverterUnit<Q> : Unit<Q> where Q : class, IQuantity<Q>, new()
    {
        #region FIELDS

        private readonly Func<float, float> convertAmountToStandardUnit;

        private readonly Func<float, float> convertStandardAmountToUnit;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a physical unit object
        /// </summary>
        /// <param name="symbol">Unit display symbol</param>
        /// <param name="convertAmountToStandardUnit">Amount converter function from this unit to quantity's standard unit</param>
        /// <param name="convertStandardAmountToUnit">Amount converter function from quantity's standard unit to this unit</param>
        public FunctionConverterUnit(
            string symbol,
            Func<float, float> convertAmountToStandardUnit,
            Func<float, float> convertStandardAmountToUnit)
            : base(false, symbol)
        {
            this.convertAmountToStandardUnit = convertAmountToStandardUnit;
            this.convertStandardAmountToUnit = convertStandardAmountToUnit;
        }

        #endregion

        #region Implementation of IUnit<Q>

        /// <summary>
        /// Convert the amount from the current unit to the standard unit of the specified quantity
        /// </summary>
        /// <param name="amount">Amount in this unit</param>
        /// <returns>Amount converted to standard unit</returns>
        public override float ConvertAmountToStandardUnit(float amount)
        {
            return this.convertAmountToStandardUnit(amount);
        }

        /// <summary>
        /// Convert a standard amount to this unit of the specified quantity
        /// to the current unit
        /// </summary>
        /// <param name="standardAmount">Standard amount of the current <see cref="IUnit.Quantity"/>.</param>
        /// <returns>Amount in this unit.</returns>
        public override float ConvertStandardAmountToUnit(float standardAmount)
        {
            return this.convertStandardAmountToUnit(standardAmount);
        }

        #endregion
    }
}