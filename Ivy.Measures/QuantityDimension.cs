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
    using System.Collections.Generic;

    /// <summary>
    /// Representation of the quantity dimension in terms of SI base units.
    /// </summary>
    /// <remarks>
    /// Each dimensionless quantity should be assigned a prime number, to ensure that one dimensionless
    /// quantity does not accidentally switch into an unrelated dimensionless quantity through multiplication.
    /// When adding a new field, unrelated to the other dimensionless quantities, assign its value by 
    /// calling the <see cref="GetNextPrime"/> method.
    /// The solid angle quantity steradian can be regarded as the square of the plane angle quantity
    /// radian, and therefore its identifier is set to the square of the radian.
    /// </remarks>
    public sealed class QuantityDimension
    {
        #region PRIVATE STATIC MEMBERS

        private const double EPSILON = double.Epsilon;
        private static readonly IEnumerator<int> _primesEnumerator = new PrimeNumbers().GetEnumerator();

        #endregion

        #region SI base quantity fields

        public static readonly QuantityDimension Length = new QuantityDimension(1, 0, 0, 0, 0, 0, 0);
        public static readonly QuantityDimension Mass = new QuantityDimension(0, 1, 0, 0, 0, 0, 0);
        public static readonly QuantityDimension Time = new QuantityDimension(0, 0, 1, 0, 0, 0, 0);
        public static readonly QuantityDimension ElectricCurrent = new QuantityDimension(0, 0, 0, 1, 0, 0, 0);
        public static readonly QuantityDimension Temperature = new QuantityDimension(0, 0, 0, 0, 1, 0, 0);
        public static readonly QuantityDimension LuminousIntensity = new QuantityDimension(0, 0, 0, 0, 0, 1, 0);
        public static readonly QuantityDimension AmountOfSubstance = new QuantityDimension(0, 0, 0, 0, 0, 0, 1);

        #endregion

        #region Dimensionless quantity fields

        public static readonly QuantityDimension Number = new QuantityDimension(1);

        public static readonly QuantityDimension Radian = new QuantityDimension(GetNextPrime());
        public static readonly QuantityDimension Steradian = Radian * Radian;
        public static readonly QuantityDimension Pi = new QuantityDimension(GetNextPrime());
        public static readonly QuantityDimension RelativeDensity = new QuantityDimension(GetNextPrime());
        public static readonly QuantityDimension RefractiveIndex = new QuantityDimension(GetNextPrime());
        public static readonly QuantityDimension RelativePermeability = new QuantityDimension(GetNextPrime());
        public static readonly QuantityDimension RelativeBiologicalEffectiveness = new QuantityDimension(GetNextPrime());
        public static readonly QuantityDimension Meterset = new QuantityDimension(GetNextPrime());

        #endregion
        
        #region CONSTRUCTORS

        /// <summary>
        /// Intitalizes a dimensionless quantity dimension
        /// </summary>
        /// <param name="iDimensionlessDifferentiator">Scalar used to differentiate between relevant dimensionless quantities</param>
        private QuantityDimension(int iDimensionlessDifferentiator) :
            this(iDimensionlessDifferentiator, 0, 0, 0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a (linear) instance of a quantity dimension
        /// </summary>
        /// <param name="iLengthExponent">Length exponent</param>
        /// <param name="iMassExponent">Mass exponent</param>
        /// <param name="iTimeExponent">Time exponent</param>
        /// <param name="iElectricCurrentExponent">Electric current exponent</param>
        /// <param name="iTemperatureExponent">Temperature exponent</param>
        /// <param name="iLuminousIntensityExponent">Luminous intensity exponent</param>
        /// <param name="iAmountOfSubstanceExponent">Amount of substance exponent</param>
        internal QuantityDimension(int iLengthExponent, int iMassExponent, int iTimeExponent, int iElectricCurrentExponent, int iTemperatureExponent,
            int iLuminousIntensityExponent, int iAmountOfSubstanceExponent) :
            this(1.0, iLengthExponent, iMassExponent, iTimeExponent, iElectricCurrentExponent,
            iTemperatureExponent, iLuminousIntensityExponent, iAmountOfSubstanceExponent)
        {
        }

        /// <summary>
        /// Initializes an instance of a quantity dimension
        /// </summary>
        /// <param name="iDimensionlessDifferentiator">Scalar used to differentiate between relevant dimensionless quantities</param>
        /// <param name="iLengthExponent">Length exponent</param>
        /// <param name="iMassExponent">Mass exponent</param>
        /// <param name="iTimeExponent">Time exponent</param>
        /// <param name="iElectricCurrentExponent">Electric current exponent</param>
        /// <param name="iTemperatureExponent">Temperature exponent</param>
        /// <param name="iLuminousIntensityExponent">Luminous intensity exponent</param>
        /// <param name="iAmountOfSubstanceExponent">Amount of substance exponent</param>
        private QuantityDimension(double iDimensionlessDifferentiator, int iLengthExponent, int iMassExponent, int iTimeExponent, int iElectricCurrentExponent, int iTemperatureExponent,
            int iLuminousIntensityExponent, int iAmountOfSubstanceExponent)
        {
            DimensionlessDifferentiator = iDimensionlessDifferentiator;
            LengthExponent = iLengthExponent;
            MassExponent = iMassExponent;
            TimeExponent = iTimeExponent;
            ElectricCurrentExponent = iElectricCurrentExponent;
            TemperatureExponent = iTemperatureExponent;
            LuminousIntensityExponent = iLuminousIntensityExponent;
            AmountOfSubstanceExponent = iAmountOfSubstanceExponent;
        }

        #endregion

        #region AUTO-IMPLEMENTED PROPERTIES

        /// <summary>
        /// Gets the scalar used to differentiate between relevant dimensionless quantities
        /// </summary>
        internal double DimensionlessDifferentiator { get; private set; }

        /// <summary>
        /// Gets the length (m) exponent
        /// </summary>
        internal int LengthExponent { get; private set; }

        /// <summary>
        /// Gets the mass (kg) exponent
        /// </summary>
        internal int MassExponent { get; private set; }

        /// <summary>
        /// Gets the time (s) exponent
        /// </summary>
        internal int TimeExponent { get; private set; }

        /// <summary>
        /// Gets the electric current (A) exponent
        /// </summary>
        internal int ElectricCurrentExponent { get; private set; }

        /// <summary>
        /// Gets the temperature (K) exponent
        /// </summary>
        internal int TemperatureExponent { get; private set; }

        /// <summary>
        /// Gets the luminous intensity (Cd) exponent
        /// </summary>
        internal int LuminousIntensityExponent { get; private set; }

        /// <summary>
        /// Gets the substance amount (mol) exponent
        /// </summary>
        internal int AmountOfSubstanceExponent { get; private set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Compare the exponents of this object with another quantity dimension object for dimensional equality
        /// </summary>
        /// <param name="other">Quantity dimension object with which to compare dimensional equality</param>
        /// <returns>true if all exponent elements of this and the other object are equal, false otherwise</returns>
        internal bool ExponentsEqual(QuantityDimension other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AreExponentsEqualTo(other);
        }

        /// <summary>
        /// Compare this object with another quantity dimension object for equality
        /// </summary>
        /// <param name="other">Quantity dimension object with which to compare equality</param>
        /// <returns>true if all elements of this and the other object are equal, false otherwise</returns>
        internal bool Equals(QuantityDimension other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(other.DimensionlessDifferentiator - DimensionlessDifferentiator) < EPSILON &&
                   AreExponentsEqualTo(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as QuantityDimension);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = DimensionlessDifferentiator.GetHashCode();
                result = (result * 397) ^ LengthExponent;
                result = (result * 397) ^ MassExponent;
                result = (result * 397) ^ TimeExponent;
                result = (result * 397) ^ ElectricCurrentExponent;
                result = (result * 397) ^ TemperatureExponent;
                result = (result * 397) ^ LuminousIntensityExponent;
                result = (result * 397) ^ AmountOfSubstanceExponent;
                return result;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                ConditionalOutput("m", LengthExponent), ConditionalOutput("kg", MassExponent),
                ConditionalOutput("s", TimeExponent), ConditionalOutput("A", ElectricCurrentExponent),
                ConditionalOutput("K", TemperatureExponent), ConditionalOutput("Cd", LuminousIntensityExponent),
                ConditionalOutput("mol", AmountOfSubstanceExponent)).Trim();
        }

        private bool AreExponentsEqualTo(QuantityDimension other)
        {
            return other.LengthExponent == LengthExponent && other.MassExponent == MassExponent &&
                   other.TimeExponent == TimeExponent &&
                   other.ElectricCurrentExponent == ElectricCurrentExponent &&
                   other.TemperatureExponent == TemperatureExponent &&
                   other.LuminousIntensityExponent == LuminousIntensityExponent &&
                   other.AmountOfSubstanceExponent == AmountOfSubstanceExponent;
        }

        private static string ConditionalOutput(string iSiUnit, int iExponent)
        {
            return iExponent == 0
                       ? string.Empty
                       : iExponent == 1 ? string.Format(" {0}", iSiUnit) : string.Format(" {0}^{1}", iSiUnit, iExponent);
        }

        private static int GetNextPrime()
        {
            if (_primesEnumerator.MoveNext()) return _primesEnumerator.Current;
            throw new InvalidOperationException("Reached the end of the Int32 prime number collection");
        }

        #endregion

        #region OPERATORS

        /// <summary>
        /// Multiply two quantity dimension objects
        /// </summary>
        /// <param name="iLhs">First quantity dimension object</param>
        /// <param name="iRhs">Second quantity dimension object</param>
        /// <returns>New quantity dimension object representing the product of the two specified quantity dimensions</returns>
        /// <remarks>Multiplication of base units correspond to addition of their respective exponents</remarks>
        public static QuantityDimension operator *(QuantityDimension iLhs, QuantityDimension iRhs)
        {
            return new QuantityDimension(iLhs.DimensionlessDifferentiator * iRhs.DimensionlessDifferentiator,
                iLhs.LengthExponent + iRhs.LengthExponent, iLhs.MassExponent + iRhs.MassExponent,
                iLhs.TimeExponent + iRhs.TimeExponent, iLhs.ElectricCurrentExponent + iRhs.ElectricCurrentExponent,
                iLhs.TemperatureExponent + iRhs.TemperatureExponent, iLhs.LuminousIntensityExponent + iRhs.LuminousIntensityExponent,
                iLhs.AmountOfSubstanceExponent + iRhs.AmountOfSubstanceExponent);
        }

        /// <summary>
        /// Divide two quantity dimension objects
        /// </summary>
        /// <param name="iLhs">Numerator quantity dimension</param>
        /// <param name="iRhs">Denominator quantity dimension</param>
        /// <returns>New quantity dimension representing the quotient between the two specified quantity dimensions</returns>
        /// <remarks>Division of base units correspond to subtraction of their respective exponents</remarks>
        public static QuantityDimension operator /(QuantityDimension iLhs, QuantityDimension iRhs)
        {
            return new QuantityDimension(iLhs.DimensionlessDifferentiator / iRhs.DimensionlessDifferentiator,
                iLhs.LengthExponent - iRhs.LengthExponent, iLhs.MassExponent - iRhs.MassExponent,
                iLhs.TimeExponent - iRhs.TimeExponent, iLhs.ElectricCurrentExponent - iRhs.ElectricCurrentExponent,
                iLhs.TemperatureExponent - iRhs.TemperatureExponent, iLhs.LuminousIntensityExponent - iRhs.LuminousIntensityExponent,
                iLhs.AmountOfSubstanceExponent - iRhs.AmountOfSubstanceExponent);
        }

        /// <summary>
        /// Compute the power of the quantity dimension object
        /// </summary>
        /// <param name="iDimension">Quantity dimension base in the power computation</param>
        /// <param name="iExponent">Scalar exponent</param>
        /// <returns>Computed power of the <paramref name="iDimension">specified quantity dimension</paramref> and the 
        /// <paramref name="iExponent">specified exponent</paramref></returns>
        public static QuantityDimension operator ^(QuantityDimension iDimension, int iExponent)
        {
            return new QuantityDimension(Math.Pow(iDimension.DimensionlessDifferentiator, iExponent),
                iExponent * iDimension.LengthExponent, iExponent * iDimension.MassExponent,
                iExponent * iDimension.TimeExponent, iExponent * iDimension.ElectricCurrentExponent,
                iExponent * iDimension.TemperatureExponent, iExponent * iDimension.LuminousIntensityExponent,
                iExponent * iDimension.AmountOfSubstanceExponent);
        }

        #endregion
    }
}

