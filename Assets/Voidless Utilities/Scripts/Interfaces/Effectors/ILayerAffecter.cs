using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface ILayerAffecter
{
	LayerMask affectedLayer { get; set; } 	/// <summary>Affected LayerMasks.</summary>
}
}