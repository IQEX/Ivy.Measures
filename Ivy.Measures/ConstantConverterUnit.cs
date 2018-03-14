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
#if SINGLE
    using AmountType = System.Single;
#elif DECIMAL
    using AmountType = System.Decimal;
#elif DOUBLE
    using AmountType = System.Double;
#endif

    /// <summary>
    /// Representation of a physical unit of a specific quanity
    /// </summary>
    /// <typeparam name="Q">Quantity type with which the unit is associated</typeparam>
    public sealed class ConstantConverterUnit<Q> : Unit<Q> where Q : class, IQuantity<Q>, new()
    {
        #region FIELDS

        private readonly float amountToStandardUnitFactor;

        private readonly float standardAmountToUnitFactor;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initialize a physical unit object that is the standard unit of the specific quantity
        /// </summary>
        /// <param name="symbol">Unit display symbol</param>
        public ConstantConverterUnit(string symbol) : base(isStandardUnit: true, symbol: symbol)
        {
            this.amountToStandardUnitFactor = Constants.One;
            this.standardAmountToUnitFactor = Constants.One;
        }

        /// <summary>
        /// Convenience constructor for initializing prefixed non-standard unit
        /// </summary>
        /// <param name="prefix">Prefix to use in unit naming and scaling vis-a-vis standard unit</param>
        public ConstantConverterUnit(UnitPrefix prefix)
            : this($"{prefix.GetSymbol()}{new Q().StandardUnit.Symbol}", prefix.GetFactor())
        {
        }

        /// <summary>
        /// Initialize a physical unit object
        /// </summary>
        /// <param name="symbol">Unit display symbol</param>
        /// <param name="toStandardUnitFactor">Amount converter factor from this unit to quantity's standard unit</param>
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        public ConstantConverterUnit(string symbol, float toStandardUnitFactor)
            : base(toStandardUnitFactor == Constants.One, symbol)
        {
            this.amountToStandardUnitFactor = toStandardUnitFactor;
            this.standardAmountToUnitFactor = Constants.One / toStandardUnitFactor;
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
            return this.amountToStandardUnitFactor * amount;
        }

        /// <summary>
        /// Convert a standard amount to this unit of the specified quantity
        /// to the current unit
        /// </summary>
        /// <param name="standardAmount">Standard amount of the current <see cref="IUnit.Quantity"/>.</param>
        /// <returns>Amount in this unit.</returns>
        public override float ConvertStandardAmountToUnit(float standardAmount)
        {
            return this.standardAmountToUnitFactor * standardAmount;
        }

        #endregion
    }
}