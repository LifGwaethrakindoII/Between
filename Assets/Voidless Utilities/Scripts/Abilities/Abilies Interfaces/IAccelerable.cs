using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IAccelerable<T>
{
	Accelerable accelerable { get; set; } 	/// <summary>Accelerable Ability's attributes.</summary>
	T velocity { get; set; } 				/// <summary>Ability's Velocity Vector.</summary>
}
}