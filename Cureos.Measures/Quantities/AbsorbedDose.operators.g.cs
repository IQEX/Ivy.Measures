namespace Cureos.Measures.Quantities
{
    /// <summary>
    /// Multiplicative operators for the absorbed dose quantity
    /// </summary>
    public partial struct AbsorbedDose
    {
        #region OPERATORS

        /// <summary>
        /// A division operator for AbsorbedDose and Time objects.
        /// </summary>
        /// <param name="lhs">Left-hand side AbsorbedDose object.</param>
        /// <param name="rhs">Right-hand side Time object.</param>
        /// <returns>Result of division between <paramref name="lhs"/> and <paramref name="rhs"/>, returned in quantity AbsorbedDoseRate.</returns>
        public static AbsorbedDoseRate operator /(AbsorbedDose lhs, Time rhs)
        {
            return new AbsorbedDoseRate(lhs.Amount / rhs.Amount);
        }

        /// <summary>
        /// A division operator for AbsorbedDose and Time objects.
        /// </summary>
        /// <param name="lhs">Left-hand side AbsorbedDose object.</param>
        /// <param name="rhs">Right-hand side Time object (any object implementing IMeasure&lt;Time&gt; interface).</param>
        /// <returns>Result of division between <paramref name="lhs"/> and <paramref name="rhs"/>, returned in quantity AbsorbedDoseRate.</returns>
        public static AbsorbedDoseRate operator /(AbsorbedDose lhs, IMeasure<Time> rhs)
        {
            return new AbsorbedDoseRate(lhs.Amount / rhs.StandardAmount);
        }

        /// <summary>
        /// A division operator for AbsorbedDose and Meterset objects.
        /// </summary>
        /// <param name="lhs">Left-hand side AbsorbedDose object.</param>
        /// <param name="rhs">Right-hand side Meterset object.</param>
        /// <returns>Result of division between <paramref name="lhs"/> and <paramref name="rhs"/>, returned in quantity AbsorbedDoseMetersetRate.</returns>
        public static AbsorbedDoseMetersetRate operator /(AbsorbedDose lhs, Meterset rhs)
        {
            return new AbsorbedDoseMetersetRate(lhs.Amount / rhs.Amount);
        }

        /// <summary>
        /// A division operator for AbsorbedDose and Meterset objects.
        /// </summary>
        /// <param name="lhs">Left-hand side AbsorbedDose object.</param>
        /// <param name="rhs">Right-hand side Meterset object (any object implementing IMeasure&lt;Meterset&gt; interface).</param>
        /// <returns>Result of division between <paramref name="lhs"/> and <paramref name="rhs"/>, returned in quantity AbsorbedDoseMetersetRate.</returns>
        public static AbsorbedDoseMetersetRate operator /(AbsorbedDose lhs, IMeasure<Meterset> rhs)
        {
            return new AbsorbedDoseMetersetRate(lhs.Amount / rhs.StandardAmount);
        }

        #endregion
    }
}
