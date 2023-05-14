using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IRigidbodyAbility : IAbility
{
	Rigidbody rigidbody { get; set; } 	/// <summary>Rigidbody's Component.</summary>

	ForceMode forceMode { get; set; } 	/// <summary>Force Mode applied.</summary>
}
}