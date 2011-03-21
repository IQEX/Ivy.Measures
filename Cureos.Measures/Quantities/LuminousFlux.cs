﻿// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

namespace Cureos.Measures.Quantities
{
	/// <summary>
	/// Implementation of the luminous flux quantity
	/// </summary>
	public struct LuminousFlux : IQuantity<LuminousFlux>
	{
		#region FIELDS
		
		private static readonly QuantityDimension _dimension = new QuantityDimension(DimensionlessDifferentiators.Steradian, 0, 0, 0, 0, 0, 1, 0);

		public static readonly Unit<LuminousFlux> Lumen = new Unit<LuminousFlux>("lm");

		#endregion

		#region Implementation of IQuantity<LuminousFlux>

		/// <summary>
		/// Gets the physical dimension of the quantity in terms of SI units
		/// </summary>
		public QuantityDimension Dimension
		{
			get { return _dimension; }
		}

		/// <summary>
		/// Gets the standard unit associated with the quantity
		/// </summary>
		public IUnit<LuminousFlux> StandardUnit
		{
			get { return Lumen; }
		}

		#endregion
	}
}