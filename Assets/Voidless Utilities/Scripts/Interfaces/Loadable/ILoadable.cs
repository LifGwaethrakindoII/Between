using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface ILoadable
{
	bool Loaded { get; set; } 	/// <summary>Has the Object been Loaded?.</summary>
}
}