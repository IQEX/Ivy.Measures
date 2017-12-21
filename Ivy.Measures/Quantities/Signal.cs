namespace Ivy.Measures.Quantities
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

#if SINGLE
    using AmountType = System.Single;
#elif DECIMAL
    using AmountType = System.Decimal;
#elif DOUBLE
    using AmountType = System.Double;
#endif
    /// <summary>
    /// Implementation of the Signal-to-noise ratio (abbreviated SNR or S/N) 
    /// </summary>
    [DataContract]
    public class Signal : IQuantity<Signal>, IMeasure<Signal>, IEquatable<Signal>, IComparable<Signal>
    {
        public Signal() { }
        #region FIELDS

        // ReSharper disable once InconsistentNaming
        private static readonly IMeasureFactory<Signal> factory = new MeasureFactory();

        // ReSharper disable once InconsistentNaming
        private static readonly QuantityDimension dimension = new QuantityDimension(-1, 0, 0, 0, 0, 0, 0);

        public static readonly Unit<Signal> Bell = new ConstantConverterUnit<Signal>("b");
        public static readonly Unit<Signal> Neper = new ConstantConverterUnit<Signal>("Np");
        public static readonly Unit<Signal> DBell = new ConstantConverterUnit<Signal>("dB");

        [DataMember]
        private readonly float amount;

        #endregion

        #region Implementation of IQuantity<Signal>

        /// <summary>
        /// Gets the display name of the quantity
        /// </summary>
        public string DisplayName => "Signal Number";

        /// <summary>
        /// Gets the physical dimension of the quantity in terms of SI units
        /// </summary>
        QuantityDimension IQuantity.Dimension => dimension;

        /// <summary>
        /// Gets the standard unit associated with the quantity
        /// </summary>
        IUnit IQuantity.StandardUnit => this.StandardUnit;

        /// <summary>
        /// Gets the standard unit associated with the quantity
        /// </summary>
        public IUnit<Signal> StandardUnit => DBell;

        /// <summary>
        /// Gets the measure factory associated with the quantity.
        /// </summary>
        IMeasureFactory<Signal> IQuantity<Signal>.Factory => factory;

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        bool IEquatable<IQuantity>.Equals(IQuantity other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            return other is Signal;
        }

        #endregion

        #region Implementation of IMeasure<Signal>

        /// <summary>
        /// Gets the measured amount in the <see cref="StandardUnit">standard unit of measure</see>
        /// </summary>
        public float Amount => this.amount;

        /// <summary>
        /// Gets the measured amount in the standard unit of measure for the wave number quantity
        /// </summary>
        public float StandardAmount => this.amount;

        /// <summary>
        /// Gets the unit of measure
        /// </summary>
        /// <remarks>Always return the standard unit of measure</remarks>
        IUnit IMeasure.Unit => this.StandardUnit;

        /// <summary>
        /// Gets the amount of this measure in the requested unit
        /// </summary>
        /// <param name="unit">Unit to which the measured amount should be converted</param>
        /// <returns>Measured amount converted into <paramref name="unit">specified unit</paramref></returns>
        float IMeasure.GetAmount(IUnit unit)
        {
            return this.GetAmount(unit as IUnit<Signal>);
        }

        /// <summary>
        /// Gets a new unit specific measure based on this measure but in the <paramref name="unit">specified unit</paramref>
        /// </summary>
        /// <param name="unit">Unit in which the new measure should be specified</param>
        /// <exception cref="ArgumentNullException">if specified unit is null or if specified unit is not of the Signal quantity.</exception>
        IMeasure IMeasure.this[IUnit unit] => this[unit as IUnit<Signal>];

        /// <summary>
        /// Gets the quantity-typed unit of measure
        /// </summary>
        /// <remarks>Always return the standard unit of measure</remarks>
        public IUnit<Signal> Unit => this.StandardUnit;

        /// <summary>
        /// Gets the amount of this measure in the requested unit
        /// </summary>
        /// <param name="unit">Unit to which the measured amount should be converted</param>
        /// <returns>Measured amount converted into <paramref name="unit">specified unit</paramref></returns>
        public float GetAmount(IUnit<Signal> unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            return unit.ConvertStandardAmountToUnit(this.amount);
        }

        /// <summary>
        /// Gets a new unit specific measure based on this measure but in the <paramref name="unit">specified unit</paramref>
        /// </summary>
        /// <param name="unit">Unit in which the new measure should be specified</param>
        IMeasure<Signal> IMeasure<Signal>.this[IUnit<Signal> unit] => this[unit];

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        bool IEquatable<IMeasure<Signal>>.Equals(IMeasure<Signal> other)
        {
            if (ReferenceEquals(null, other)) return false;
            return this.amount.Equals(other.StandardAmount);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        bool IEquatable<IMeasure>.Equals(IMeasure other)
        {
            return this.Equals(other as IMeasure<Signal>);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:  
        ///    Value              Meaning 
        ///    Less than zero     This object is less than the <paramref name="other"/> parameter.
        ///    Zero               This object is equal to <paramref name="other"/>. 
        ///    Greater than zero  This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        int IComparable<IMeasure<Signal>>.CompareTo(IMeasure<Signal> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return this.amount.CompareTo(other.StandardAmount);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings:  
        ///    Value              Meaning 
        ///    Less than zero     This object is less than the <paramref name="other"/> parameter.
        ///    Zero               This object is equal to <paramref name="other"/>. 
        ///    Greater than zero  This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        int IComparable<IMeasure>.CompareTo(IMeasure other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other.Unit.Quantity is IMeasure<Signal>)) throw new ArgumentException("Measures are of different quantities");
            return this.amount.CompareTo(other.StandardAmount);
        }

        #endregion


        #region Implementation of IEquatable<Signal>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Signal other)
        {
            return this.amount.Equals(other.amount);
        }

        #endregion

        #region INDEXERS

        /// <summary>
        /// Gets a new unit preserving measure based on this measure but in the <paramref name="unit">specified unit</paramref>
        /// </summary>
        /// <param name="unit">Unit in which the new measure should be specified</param>
        public Measure<Signal> this[IUnit<Signal> unit]
        {
            get
            {
                if (unit == null) throw new ArgumentNullException(nameof(unit));
                return new Measure<Signal>(this.GetAmount(unit), unit);
            }
        }

        #endregion

        #region Implementation of IComparable<Signal>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:  
        ///    Value              Meaning 
        ///    Less than zero     This object is less than the <paramref name="other"/> parameter.
        ///    Zero               This object is equal to <paramref name="other"/>. 
        ///    Greater than zero  This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Signal other)
        {
            return this.amount.CompareTo(other.amount);
        }

        #endregion


        #region METHODS

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return obj is IMeasure<Signal> measure && this.Equals(measure);
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
            return this.amount.GetHashCode();
        }

        /// <summary>
        /// Returns the actual value with the quantity suffixed
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing the actual value and unit
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the actual value in formatted form with the quantity suffixed
        /// </summary>
        /// <param name="format">Format string to display the value with</param>
        /// <returns>A <see cref="T:System.String"/> containing the actual value in formatted form and unit</returns>
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the actual value in formatted form with the quantity suffixed
        /// </summary>
        /// <param name="provider">Formatting provider to format the value with</param>
        /// <returns>A <see cref="T:System.String"/> containing the actual value and unit</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString("G", provider);
        }

        /// <summary>
        /// Returns the actual value in formatted form with the quantity suffixed
        /// </summary>
        /// <param name="format">Format string to display the value with</param>
        /// <param name="provider">Formatting provider to format the value with</param>
        /// <returns>A <see cref="T:System.String"/> containing the actual value in formatted form and unit</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return $"{this.amount.ToString(format, provider)} {this.Unit.Symbol}".TrimEnd();
        }

        #endregion

        #region PROPERTIES

        public static Signal Zero { get; private set; }

        public static Signal Epsilon { get; private set; }

        #endregion

         #region OPERATORS

        /// <summary>
        /// Casts a double value to a Signal object
        /// </summary>
        /// <param name="standardAmount">Standard amount</param>
        /// <returns>Signal representation of <paramref name="standardAmount"/> in unit ReciprocalMeter</returns>
        public static explicit operator Signal(double standardAmount)
        {
            return new Signal(standardAmount);
        }

        /// <summary>
        /// Casts a float value to a Signal object
        /// </summary>
        /// <param name="standardAmount">Standard amount</param>
        /// <returns>Signal representation of <paramref name="standardAmount"/> in unit ReciprocalMeter</returns>
        public static explicit operator Signal(float standardAmount)
        {
            return new Signal(standardAmount);
        }

        /// <summary>
        /// Casts a decimal value to a Signal object
        /// </summary>
        /// <param name="standardAmount">Standard amount</param>
        /// <returns>Signal representation of <paramref name="standardAmount"/> in unit ReciprocalMeter</returns>
        public static explicit operator Signal(decimal standardAmount)
        {
            return new Signal(standardAmount);
        }
        
        /// <summary>
        /// Adds two measure objects provided the measured quantities are equal
        /// </summary>
        /// <param name="lhs">First measure term</param>
        /// <param name="rhs">Second measure term</param>
        /// <returns>Sum of the two measure objects in the unit of the <paramref name="lhs">left-hand side measure</paramref></returns>
        public static Signal operator +(Signal lhs,  Signal rhs)
        {
            return new Signal(lhs.amount + rhs.amount);
        }

        /// <summary>
        /// Adds two measure objects provided the measured quantities are equal
        /// </summary>
        /// <param name="lhs">First measure term</param>
        /// <param name="rhs">Second measure term (any object implementing the IMeasure interface)</param>
        /// <returns>Sum of the two measure objects in the unit of the <paramref name="lhs">left-hand side measure</paramref></returns>
        public static Signal operator +(Signal lhs, IMeasure<Signal> rhs)
        {
            return new Signal(lhs.amount + rhs.StandardAmount);
        }

        /// <summary>
        /// Subtract two measure objects of the same quantity
        /// </summary>
        /// <param name="lhs">First measure object</param>
        /// <param name="rhs">Second measure object</param>
        /// <returns>Difference of the measure objects</returns>
        public static Signal operator -(Signal lhs, Signal rhs)
        {
            return new Signal(lhs.amount - rhs.amount);
        }

        /// <summary>
        /// Subtract two measure objects of the same quantity
        /// </summary>
        /// <param name="lhs">First measure object</param>
        /// <param name="rhs">Second measure object (any object implementing the IMeasure interface)</param>
        /// <returns>Difference of the measure objects</returns>
        public static Signal operator -(Signal lhs, IMeasure<Signal> rhs)
        {
            return new Signal(lhs.amount - rhs.StandardAmount);
        }

        /// <summary>
        /// Multiply a scalar and a measure object
        /// </summary>
        /// <param name="scalar">Floating-point scalar</param>
        /// <param name="measure">Measure object</param>
        /// <returns>Product of the scalar and the measure object</returns>
        public static Signal operator *(double scalar, Signal measure)
        {
            return new Signal((float)scalar * measure.amount);
        }

        /// <summary>
        /// Multiply a scalar and a measure object
        /// </summary>
        /// <param name="scalar">Floating-point scalar</param>
        /// <param name="measure">Measure object</param>
        /// <returns>Product of the scalar and the measure object</returns>
        public static Signal operator *(float scalar, Signal measure)
        {
            return new Signal((float)scalar * measure.amount);
        }

        /// <summary>
        /// Multiply a scalar and a measure object
        /// </summary>
        /// <param name="scalar">Floating-point scalar</param>
        /// <param name="measure">Measure object</param>
        /// <returns>Product of the scalar and the measure object</returns>
        public static Signal operator *(decimal scalar, Signal measure)
        {
            return new Signal((float)scalar * measure.amount);
        }

        /// <summary>
        /// Multiply a measure object and a scalar
        /// </summary>
        /// <param name="measure">Measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Product of the measure object and the scalar</returns>
        public static Signal operator *(Signal measure, double scalar)
        {
            return new Signal(measure.amount * (float)scalar);
        }

        /// <summary>
        /// Multiply a measure object and a scalar
        /// </summary>
        /// <param name="measure">Measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Product of the measure object and the scalar</returns>
        public static Signal operator *(Signal measure, float scalar)
        {
            return new Signal(measure.amount * (float)scalar);
        }

        /// <summary>
        /// Multiply a measure object and a scalar
        /// </summary>
        /// <param name="measure">Measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Product of the measure object and the scalar</returns>
        public static Signal operator *(Signal measure, decimal scalar)
        {
            return new Signal(measure.amount * (float)scalar);
        }

        /// <summary>
        /// Multiply a measure object and a number
        /// </summary>
        /// <param name="measure">Measure object</param>
        /// <param name="scalar">Floating-point number</param>
        /// <returns>Product of the measure object and the number</returns>
        public static Signal operator *(Signal measure, IMeasure<Number> scalar)
        {
            return new Signal(measure.amount * scalar.StandardAmount);
        }

        /// <summary>
        /// Divide a measure object with a scalar
        /// </summary>
        /// <param name="measure">measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Quotient of the measure object and the scalar</returns>
        public static Signal operator /(Signal measure, double scalar)
        {
            return new Signal(measure.amount / (float)scalar);
        }

        /// <summary>
        /// Divide a measure object with a scalar
        /// </summary>
        /// <param name="measure">measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Quotient of the measure object and the scalar</returns>
        public static Signal operator /(Signal measure, float scalar)
        {
            return new Signal(measure.amount / (float)scalar);
        }

        /// <summary>
        /// Divide a measure object with a scalar
        /// </summary>
        /// <param name="measure">measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Quotient of the measure object and the scalar</returns>
        public static Signal operator /(Signal measure, decimal scalar)
        {
            return new Signal(measure.amount / (float)scalar);
        }

        /// <summary>
        /// Divide a measure object with a number
        /// </summary>
        /// <param name="measure">measure object</param>
        /// <param name="scalar">Floating-point number</param>
        /// <returns>Quotient of the measure object and the number</returns>
        public static Signal operator /(Signal measure, IMeasure<Number> scalar)
        {
            return new Signal(measure.amount / scalar.StandardAmount);
        }

        /// <summary>
        /// Divide a measure object with a measure object of the same quantity
        /// </summary>
        /// <param name="dividend">Dividend of specific quantity</param>
        /// <param name="divisor">Divisor of same quantity as dividend</param>
        /// <returns>Quotient of the two measure objects</returns>
        public static Number operator /(Signal dividend, Signal divisor)
        {
            return new Number(dividend.amount / divisor.amount);
        }

        /// <summary>
        /// Divide a measure object with a measure object of the same quantity
        /// </summary>
        /// <param name="dividend">Dividend of specific quantity</param>
        /// <param name="divisor">Divisor of same quantity as dividend</param>
        /// <returns>Quotient of the two measure objects</returns>
        public static Number operator /(Signal dividend, IMeasure<Signal> divisor)
        {
            return new Number(dividend.amount / divisor.StandardAmount);
        }

        /// <summary>
        /// Less than operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is less than second measure object; false otherwise</returns>
        public static bool operator <(Signal lhs, Signal rhs)
        {
            return lhs.amount < rhs.amount;
        }

        /// <summary>
        /// Less than operator for measure objects, where right-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <returns>true if first measure object is less than second measure object; false otherwise</returns>
        public static bool operator <(Signal lhs, IMeasure<Signal> rhs)
        {
            return lhs.amount < rhs.StandardAmount;
        }

        /// <summary>
        /// Less than operator for measure objects, where left-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is less than second measure object; false otherwise</returns>
        public static bool operator <(IMeasure<Signal> lhs, Signal rhs)
        {
            return lhs.StandardAmount < rhs.amount;
        }

        /// <summary>
        /// Greater than operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is greater than second measure object; false otherwise</returns>
        public static bool operator >(Signal lhs, Signal rhs)
        {
            return lhs.amount > rhs.amount;
        }

        /// <summary>
        /// Greater than operator for measure objects, where right-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <returns>true if first measure object is greater than second measure object; false otherwise</returns>
        public static bool operator >(Signal lhs, IMeasure<Signal> rhs)
        {
            return lhs.amount > rhs.StandardAmount;
        }

        /// <summary>
        /// Greater than operator for measure objects, where left-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is greater than second measure object; false otherwise</returns>
        public static bool operator >(IMeasure<Signal> lhs, Signal rhs)
        {
            return lhs.StandardAmount > rhs.amount;
        }

        /// <summary>
        /// Less than or equal to operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is less than or equal to second measure object; false otherwise</returns>
        public static bool operator <=(Signal lhs, Signal rhs)
        {
            return lhs.amount <= rhs.amount;
        }

        /// <summary>
        /// Less than or equal to operator for measure objects, where right-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <returns>true if first measure object is less than or equal to second measure object; false otherwise</returns>
        public static bool operator <=(Signal lhs, IMeasure<Signal> rhs)
        {
            return lhs.amount <= rhs.StandardAmount;
        }

        /// <summary>
        /// Less than or equal to operator for measure objects, where left-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is less than or equal to second measure object; false otherwise</returns>
        public static bool operator <=(IMeasure<Signal> lhs, Signal rhs)
        {
            return lhs.StandardAmount <= rhs.amount;
        }

        /// <summary>
        /// Greater than or equal to operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is greater than or equal to second measure object; false otherwise</returns>
        public static bool operator >=(Signal lhs, Signal rhs)
        {
            return lhs.amount >= rhs.amount;
        }

        /// <summary>
        /// Greater than or equal to operator for measure objects, where right-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <returns>true if first measure object is greater than or equal to second measure object; false otherwise</returns>
        public static bool operator >=(Signal lhs, IMeasure<Signal> rhs)
        {
            return lhs.amount >= rhs.StandardAmount;
        }

        /// <summary>
        /// Greater than or equal to operator for measure objects, where left-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is greater than or equal to second measure object; false otherwise</returns>
        public static bool operator >=(IMeasure<Signal> lhs, Signal rhs)
        {
            return lhs.StandardAmount >= rhs.amount;
        }

        /// <summary>
        /// Equality operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if the two measure objects are equal; false otherwise</returns>
        public static bool operator ==(Signal lhs, Signal rhs)
        {
            return lhs.amount == rhs.amount;
        }

        /// <summary>
        /// Equality operator for measure objects, where right-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <returns>true if the two measure objects are equal; false otherwise</returns>
        public static bool operator ==(Signal lhs, IMeasure<Signal> rhs)
        {
            return lhs.amount == rhs.StandardAmount;
        }

        /// <summary>
        /// Equality operator for measure objects, where left-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if the two measure objects are equal; false otherwise</returns>
        public static bool operator ==(IMeasure<Signal> lhs, Signal rhs)
        {
            return lhs.StandardAmount == rhs.amount;
        }

        /// <summary>
        /// Inequality operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if the two measure objects are not equal; false if they are equal</returns>
        public static bool operator !=(Signal lhs, Signal rhs)
        {
            return lhs.amount != rhs.amount;
        }

        /// <summary>
        /// Inequality operator for measure objects, where right-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <returns>true if the two measure objects are not equal; false if they are equal</returns>
        public static bool operator !=(Signal lhs, IMeasure<Signal> rhs)
        {
            return lhs.amount != rhs.StandardAmount;
        }

        /// <summary>
        /// Inequality operator for measure objects, where left-hand side may be any object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="lhs">First object (any object implementing IMeasure&lt;Signal&gt; interface)</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if the two measure objects are not equal; false if they are equal</returns>
        public static bool operator !=(IMeasure<Signal> lhs, Signal rhs)
        {
            return lhs.StandardAmount != rhs.amount;
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Static constructor for defining static class properties
        /// </summary>
        static Signal()
        {
            Zero = new Signal(Constants.Zero);
            Epsilon = new Signal(Constants.MachineEpsilon);
        }

        /// <summary>
        /// Initializes a wave number object from an object implementing the IMeasure&lt;Signal&gt; interface
        /// </summary>
        /// <param name="other">Object implemeting the IMeasure&lt;Signal&gt; interface</param>
        public Signal(IMeasure<Signal> other)
            : this(other.StandardAmount)
        {
        }

        /// <summary>
        /// Initializes a measure to the specified amount and standard unit of the measured quantity
        /// </summary>
        /// <param name="amount">Measured amount in standard unit of the specified quantity</param>
        public Signal(double amount)
        {
            this.amount = (float)amount;
        }

        /// <summary>
        /// Initializes a measure to the specified amount and standard unit of the measured quantity
        /// </summary>
        /// <param name="amount">Measured amount in standard unit of the specified quantity</param>
        public Signal(float amount)
        {
            this.amount = (float)amount;
        }

        /// <summary>
        /// Initializes a measure to the specified amount and standard unit of the measured quantity
        /// </summary>
        /// <param name="amount">Measured amount in standard unit of the specified quantity</param>
        public Signal(decimal amount)
        {
            this.amount = (float)amount;
        }

        /// <summary>
        /// Initializes a measure to the specified amount and unit
        /// </summary>
        /// <param name="amount">Measured amount</param>
        /// <param name="unit">Unit of measure</param>
        /// <exception cref="ArgumentNullException">if the specified unit is null</exception>
        public Signal(double amount, IUnit<Signal> unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            this.amount = unit.ConvertAmountToStandardUnit((float)amount);
        }

        /// <summary>
        /// Initializes a measure to the specified amount and unit
        /// </summary>
        /// <param name="amount">Measured amount</param>
        /// <param name="unit">Unit of measure</param>
        /// <exception cref="ArgumentNullException">if the specified unit is null</exception>
        public Signal(float amount, IUnit<Signal> unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            this.amount = unit.ConvertAmountToStandardUnit((float)amount);
        }

        /// <summary>
        /// Initializes a measure to the specified amount and unit
        /// </summary>
        /// <param name="amount">Measured amount</param>
        /// <param name="unit">Unit of measure</param>
        /// <exception cref="ArgumentNullException">if the specified unit is null</exception>
        public Signal(decimal amount, IUnit<Signal> unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            this.amount = unit.ConvertAmountToStandardUnit((float)amount);
        }

        #endregion

        #region Private class implementation of IMeasureFactory<Signal>

        private class MeasureFactory : IMeasureFactory<Signal>
        {
            /// <summary>
            /// Creates a new standard unit measure at the specified <paramref name="amount"/>.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <returns>Standard unit measure at the specified <paramref name="amount"/>.</returns>
            public Signal New(double amount)
            {
                return new Signal(amount);
            }

            /// <summary>
            /// Creates a new standard unit measure.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <param name="unit">Unit.</param>
            /// <returns>Standard unit measure.</returns>
            public Signal New(double amount, IUnit<Signal> unit)
            {
                return new Signal(amount, unit);
            }

            /// <summary>
            /// Creates a new standard unit measure at the specified <paramref name="amount"/>.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <returns>Standard unit measure at the specified <paramref name="amount"/>.</returns>
            public Signal New(float amount)
            {
                return new Signal(amount);
            }

            /// <summary>
            /// Creates a new standard unit measure.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <param name="unit">Unit.</param>
            /// <returns>Standard unit measure.</returns>
            public Signal New(float amount, IUnit<Signal> unit)
            {
                return new Signal(amount, unit);
            }

            /// <summary>
            /// Creates a new standard unit measure at the specified <paramref name="amount"/>.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <returns>Standard unit measure at the specified <paramref name="amount"/>.</returns>
            public Signal New(decimal amount)
            {
                return new Signal(amount);
            }

            /// <summary>
            /// Creates a new standard unit measure.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <param name="unit">Unit.</param>
            /// <returns>Standard unit measure.</returns>
            public Signal New(decimal amount, IUnit<Signal> unit)
            {
                return new Signal(amount, unit);
            }

            /// <summary>
            /// Creates a new measure from the specified <paramref name="amount"/> and <paramref name="unit"/>.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <param name="unit">Unit.</param>
            /// <returns>Measure from the specified <paramref name="amount"/> and <paramref name="unit"/>.</returns>
            public IMeasure<Signal> NewPreserveUnit(double amount, IUnit<Signal> unit)
            {
                return new Measure<Signal>(amount, unit);
            }

            /// <summary>
            /// Creates a new measure from the specified <paramref name="amount"/> and <paramref name="unit"/>.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <param name="unit">Unit.</param>
            /// <returns>Measure from the specified <paramref name="amount"/> and <paramref name="unit"/>.</returns>
            public IMeasure<Signal> NewPreserveUnit(float amount, IUnit<Signal> unit)
            {
                return new Measure<Signal>(amount, unit);
            }

            /// <summary>
            /// Creates a new measure from the specified <paramref name="amount"/> and <paramref name="unit"/>.
            /// </summary>
            /// <param name="amount">Amount.</param>
            /// <param name="unit">Unit.</param>
            /// <returns>Measure from the specified <paramref name="amount"/> and <paramref name="unit"/>.</returns>
            public IMeasure<Signal> NewPreserveUnit(decimal amount, IUnit<Signal> unit)
            {
                return new Measure<Signal>(amount, unit);
            }
        }

        #endregion
    }
}