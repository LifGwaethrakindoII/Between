using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessInteger
{
	/// <summary>Gets the number of active flags on a given int.</summary>
	/// <param name="_enumFlag">Enum Flag to count active [1] bits.</param>
	/// <returns>Number of active bits on enum flag.</returns>
	public static int GetActiveFlagsCount(this int _enumFlag)
	{
		int count = 0;

		while(_enumFlag > 0)
		{
			_enumFlag &= (_enumFlag - 1);
			count++;
		}

		return count;
	}

	/// <summary>Converts LayerMask's value to integer.</summary>
	/// <param name="_layer">LayerMask's reference.</param>
	/// <returns>Integer representing value's power.</returns>
	public static int GetLayerMaskInt(this LayerMask _layer)
	{
		return _layer.value.Log();
	}

	/// <summary>Calculates Logarithm in base b.</summary>
	/// <param name="x">Number to calculate logarithm.</param>
	/// <param name="b">Logarithm's base [2 as default].</param>
	/// <returns>X's logarithm in base b.</returns>
	public static int Log(this int x, int b = 2)
	{
		int count = 0;

		while(x > 1)
		{
			x /= b;
			count++;
		}

		return count;
	}
}
}