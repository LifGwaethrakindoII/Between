using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessCollections
{
	/// <summary>Moves index to next position.</summary>
	/// <param name="_collection">Reference collection.</param>
	/// <param name="_index">Index to move.</param>
	/// <returns>Index after being moved.</returns>
	public static int AddIndex<T>(this ICollection<T> _collection, ref int _index)
	{
		return ((_index + 1 < (_collection.Count - 1)) ? _index + 1 : _index);
	}

	/// <summary>Moves index to previous position.</summary>
	/// <param name="_collection">Reference collection.</param>
	/// <param name="_index">Index to move.</param>
	/// <returns>Index after being moved.</returns>
	public static int SubtractIndex<T>(this ICollection<T> _collection, ref int _index)
	{
		return ((_index - 1 > -1) ? _index - 1 : _index);
	}

	/// <summary>Evaluates if provided index is between Collection's bounds.</summary>
	/// <param name="_collection">Collection that will define the bounds.</param>
	/// <param name="_index">Index to evaluate.</param>
	/// <returns>True if the index provided is bettween Collection's bounds, false otherwise.</returns>
	public static bool CheckIfIndexBetweenBounds<T>(this ICollection<T> _collection, int _index)
	{
		return (_index >= 0 && _index <= (_collection.Count - 1));
	}
}
}