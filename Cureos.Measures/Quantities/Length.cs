// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

namespace Cureos.Measures.Quantities
{
	/// <summary>
	/// Implementation of the length quantity
	/// </summary>
	public struct Length : IQuantity<Length>
	{
		#region FIELDS

		public static readonly Unit<Length> Meter = new Unit<Length>("m");
		public static readonly Unit<Length> KiloMeter = new Unit<Length>("km", Factors.Kilo);
		public static readonly Unit<Length> DeciMeter = new Unit<Length>("dm", Factors.Deci);
		public static readonly Unit<Length> CentiMeter = new Unit<Length>("cm", Factors.Centi);
		public static readonly Unit<Length> MilliMeter = new Unit<Length>("mm", Factors.Milli);
		public static readonly Unit<Length> MicroMeter = new Unit<Length>("�m", Factors.Micro);
		public static readonly Unit<Length> NanoMeter = new Unit<Length>("nm", Factors.Nano);
		public static readonly Unit<Length> PicoMeter = new Unit<Length>("pm", Factors.Pico);
		public static readonly Unit<Length> Angstrom = new Unit<Length>("�", Factors.MetersPerAngstrom);
		public static readonly Unit<Length> Inch = new Unit<Length>("in", Factors.MetersPerInch);
		public static readonly Unit<Length> Foot = new Unit<Length>("ft", Factors.MetersPerFoot);
		public static readonly Unit<Length> Yard = new Unit<Length>("yd", Factors.MetersPerYard);
		public static readonly Unit<Length> Mile = new Unit<Length>("mi", Factors.MetersPerMile);
		public static readonly Unit<Length> NauticalMile = new Unit<Length>("M", Factors.MetersPerNauticalMile);

		#endregion

		#region Implementation of IQuantity<Q>

		/// <summary>
		/// Gets the physical dimension of the quantity in terms of SI units
		/// </summary>
		public QuantityDimension Dimension
		{
			get { return QuantityDimension.Length; }
		}

		/// <summary>
		/// Gets the standard unit associated with the quantity
		/// </summary>
		public IUnit<Length> StandardUnit
		{
			get { return Meter; }
		}

		#endregion
	}
}

