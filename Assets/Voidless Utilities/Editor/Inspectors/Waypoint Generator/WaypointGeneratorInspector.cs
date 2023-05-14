using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoidlessUtilities
{
[CustomEditor(typeof(WaypointGenerator))]
public class WaypointGeneratorInspector : BaseWaypointGeneratorInspector<WaypointGenerator, Waypoint>
{
	
}
}