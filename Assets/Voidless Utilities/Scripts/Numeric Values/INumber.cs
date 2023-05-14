using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface INumber<N>
{
	N value { get; set; } 	/// <summary>Number's value.</summary>
}
}