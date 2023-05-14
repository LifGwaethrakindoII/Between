using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public delegate void OnWaypointCameraTrigger(Transform _focusPoint, PathWaypointGenerator _pathWaypoints);
public delegate void OnGoToDefault();

public class CameraAgent : MonoBehaviour
{
	[SerializeField] private ThirdPersonCamera thirdPersonCamera; 	/// <summary>ThirPersonCamera's Component.</summary>
	[SerializeField] private bool _detect; 	/// <summary>Detect Camera's waypoint trigger?.</summary>
	private PathWaypointGenerator _lastPath;
	private PathWaypointGenerator _path;
	
	public static event OnWaypointCameraTrigger onWaypointCameraTrigger;
	public static event OnGoToDefault onGoToDefault;

	/// <summary>Gets and Sets lastPath property.</summary>
	public PathWaypointGenerator lastPath
	{
		get { return _lastPath; }
		set { _lastPath = value; }
	}

	/// <summary>Gets and Sets path property.</summary>
	public PathWaypointGenerator path
	{
		get { return _path; }
		set { _path = value; }
	}

	/// <summary>Gets and Sets detect property.</summary>
	public bool detect
	{
		get { return _detect; }
		set { _detect = value; }
	}

	/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerEnter(Collider col)
	{
		if(!detect) return;

		GameObject obj = col.gameObject;
	
		switch(obj.tag)
		{
			case "Waypoint_Camera":
			if(onWaypointCameraTrigger != null)
			{
				lastPath = obj.GetComponent<PathWaypointGenerator>();

				if(path == null || lastPath != path)
				{
					path = lastPath; 
					onWaypointCameraTrigger(transform, lastPath);
				}
				
			}
			break;

			case "Default_Waypoint_Camera":
			if(onGoToDefault != null)
			{
				path = null;
				onGoToDefault();
			}
			break;

			case "Camera_Offset_Setter":
			ThirdPersonCameraOffsetSetter setter = obj.GetComponent<ThirdPersonCameraOffsetSetter>();
			if(setter.changeDistance) thirdPersonCamera.maxDistance = setter.distance;
			thirdPersonCamera.normalizedOffset = setter.offset;
			break;
			
			default:
			break;
		}
	}

	/// <summary>Event triggered when this Collider exits another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerExit(Collider col)
	{
		if(!detect) return;

		GameObject obj = col.gameObject;
	
		switch(obj.tag)
		{
			case "Waypoint_Camera":
			if(onWaypointCameraTrigger != null)
			{
				path = null;
				onGoToDefault();
			}
			break;

			case "Default_Waypoint_Camera":
			if(onGoToDefault != null)
			{
				path = null;
				onGoToDefault();
			}
			break;

			case "Camera_Offset_Setter":
			if(onGoToDefault != null) onGoToDefault();
			break;
			
			default:
			break;
		}
	}
}
}