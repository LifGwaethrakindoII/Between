using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public enum RelativeTo 												/// <summary>Point Relativeness.</summary>
{
	Transform, 														/// <summary>Relative to Transform.</summary>
	RendererBounds, 												/// <summary>Relative to Renderer's Bounds.</summary>
	ColliderBounds 													/// <summary>Relative to Collider's Bounds.</summary>
}

[Serializable]
public class SensorSubsystem3D : IEnumerable<SensorData3D>
{
	[SerializeField] private RelativeTo _relativeTo; 				/// <summary>Relative To Which?.</summary>
	[SerializeField] private LayerMask _layerMask; 					/// <summary>Layers of interest for sensors.</summary>
	[SerializeField] private NormalizedVector3 _relativeOrigin; 	/// <summary>Sensor's Origin Relative to either Transform or Bounds.</summary>
	[SerializeField] private float _originDistance; 				/// <summary>Sensor's Origin distance.</summary>
	[SerializeField] private SensorData3D[] _sensorsData; 			/// <summary>Sensors' Data.</summary>
#if UNITY_EDITOR
	[Space(5f)]
	[Header("Gizmos' Attributes:")]
	[SerializeField] private Color _color; 							/// <summary>Debug color for the sensors.</summary>
#endif

	/// <summary>Gets and Sets relativeTo property.</summary>
	public RelativeTo relativeTo
	{
		get { return _relativeTo; }
		set { _relativeTo = value; }
	}

	/// <summary>Gets and Sets layerMask property.</summary>
	public LayerMask layerMask
	{
		get { return _layerMask; }
		set { _layerMask = value; }
	}

	/// <summary>Gets and Sets relativeOrigin property.</summary>
	public NormalizedVector3 relativeOrigin
	{
		get { return _relativeOrigin; }
		set { _relativeOrigin = value; }
	}

	/// <summary>Gets and Sets originDistance property.</summary>
	public float originDistance
	{
		get { return _originDistance; }
		set { _originDistance = value; }
	}

	/// <summary>Gets and Sets sensorsData property.</summary>
	public SensorData3D[] sensorsData
	{
		get { return _sensorsData; }
		set { _sensorsData = value; }
	}

#if UNITY_EDITOR
	/// <summary>Gets and Sets color property.</summary>
	public Color color
	{
		get { return _color; }
		set { _color = value; }
	}
#endif

	/// <returns>Returns an enumerator that iterates through the sensors' data.</returns>
	public IEnumerator<SensorData3D> GetEnumerator()
	{
		foreach(SensorData3D sensorData in sensorsData)
		{
			yield return sensorData;
		}
	}

	/// <returns>Returns an enumerator that iterates through the sensors' data.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		yield return GetEnumerator();
	}
}
}