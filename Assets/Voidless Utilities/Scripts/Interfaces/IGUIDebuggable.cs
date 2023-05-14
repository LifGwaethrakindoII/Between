using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IGUIDebuggable
{
#if UNITY_EDITOR
	bool debug { get; set; } 	/// <summary>Debug?.</summary>
	
	Rect GUIRect { get; set; } 	/// <summary>GUI's Rect.</summary>
#endif
}
}