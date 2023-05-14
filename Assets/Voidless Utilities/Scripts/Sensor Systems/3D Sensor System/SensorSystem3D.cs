using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class SensorSystem3D : MonoBehaviour
{
	private const float RADIUS_SPHERE = 0.025f; 				/// <summary>Gizmos's sphere Radius.</summary>

	[SerializeField] private SensorSubsystem3D[] _subsystems; 	/// <summary>Boundaries' Sensors.</summary>
	private Renderer _renderer; 								/// <summary>Renderer's Component.</summary>
	private Collider _collider; 								/// <summary>Collider's Component.</summary>

	/// <summary>Gets and Sets subsystems property.</summary>
	public SensorSubsystem3D[] subsystems
	{
		get { return _subsystems; }
		set { _subsystems = value; }
	}

	/// <summary>Gets and Sets renderer Component.</summary>
	public new Renderer renderer
	{ 
		get
		{
			if(_renderer == null)
			{
				_renderer = GetComponent<Renderer>();
			}
			return _renderer;
		}
	}
	/// <summary>Gets and Sets collider Component.</summary>
	public Collider collider
	{ 
		get
		{
			if(_collider == null)
			{
				_collider = GetComponent<Collider>();
			}
			return _collider;
		}
	}

	/// <summary>Draws Gizmos when this GameObject is selected.</summary>
	private void OnDrawGizmosSelected()
	{
		DrawSensors();
	}

	/// <summary>Draws Sensor's Gizmos.</summary>
	private void DrawSensors()
	{
#if UNITY_EDITOR
		if(subsystems != null && subsystems.Length > 0)
		foreach(SensorSubsystem3D subsystem in subsystems)
		{
			Gizmos.color = subsystem.color;
			foreach(SensorData3D sensorData in subsystem)
			{
				Ray sensorRay = ConvertToRay(subsystem, sensorData);
				Vector3 origin = GetRelativeOriginPoint(subsystem, sensorData);
				Vector3 sensorDirection = sensorRay.direction * sensorData.distance;
				Gizmos.DrawSphere(origin, RADIUS_SPHERE);
				Gizmos.DrawSphere(sensorRay.origin, RADIUS_SPHERE); 

				switch(sensorData.sensorType)
				{
					case SensorType.Ray:
					Gizmos.DrawRay(sensorRay.origin, sensorDirection);
					break;

					case SensorType.Box:
					Gizmos.DrawRay(sensorRay.origin, sensorDirection);
					VoidlessGizmos.DrawWireBox(sensorRay.origin + sensorDirection, sensorData.dimensions, transform.rotation);
					break;

					case SensorType.Sphere:
					Gizmos.DrawRay(sensorRay.origin, sensorDirection);
					Gizmos.DrawWireSphere(sensorRay.origin + sensorDirection, sensorData.radius);
					break;

					case SensorType.Capsule:
					VoidlessGizmos.DrawCapsule(sensorRay.origin, sensorDirection, sensorData.radius, sensorData.distance);
					break;
				}
			}
		}
#endif
	}

#region SensorDetectionMethods:
	/// <summary>Evaluates if the sensor subsystem detects a hit.</summary>
	/// <param name="_subsystemIndex">Subsystem's Index.</param>
	/// <returns>True if hit with something, false otherwise.</returns>
	public bool GetSubsystemDetection(int _subsystemIndex)
	{
		if(!subsystems.CheckIfIndexBetweenBounds(_subsystemIndex))
		{
			Debug.LogError("[SensorSystem3D] Index provided " + _subsystemIndex + " is out of bounds. Returning false.");
			return false;
		}

		SensorSubsystem3D subsystem = subsystems[_subsystemIndex];

		foreach(SensorData3D sensorData in subsystem)
		{
			Ray ray = ConvertToRay(subsystem, sensorData);

			switch(sensorData.sensorType)
			{
				case SensorType.Ray:
				if(Physics.Raycast(ray, sensorData.distance, subsystem.layerMask)) return true;
				break;

				case SensorType.Box:
				if(Physics.BoxCast(ray.origin, sensorData.dimensions, ray.direction, transform.rotation, sensorData.distance, subsystem.layerMask)) return true;
				break;

				case SensorType.Sphere:
				if(Physics.SphereCast(ray, sensorData.radius, sensorData.distance, subsystem.layerMask)) return true;
				break;

				case SensorType.Capsule:
				Vector3 destinyPoint = ray.origin + (ray.direction * sensorData.distance);
				if(Physics.CapsuleCast(ray.origin, destinyPoint, sensorData.radius, ray.direction, sensorData.distance, subsystem.layerMask)) return true;
				break;
			}
		}

		return false;
	}

	/// <summary>Evaluates if the sensor subsystem detects a hit.</summary>
	/// <param name="_subsystemIndex">Subsystem's Index.</param>
	/// <param name="_hit">RaycastHit information.</param>
	/// <returns>True if hit with something, false otherwise.</returns>
	public bool GetSubsystemDetection(int _subsystemIndex, out RaycastHit _hit)
	{
		_hit = default(RaycastHit);

		if(!subsystems.CheckIfIndexBetweenBounds(_subsystemIndex))
		{
			Debug.LogError("[SensorSystem3D] Index provided " + _subsystemIndex + " is out of bounds. Returning false.");
			return false;
		}

		SensorSubsystem3D subsystem = subsystems[_subsystemIndex];

		foreach(SensorData3D sensorData in subsystem)
		{
			Ray ray = ConvertToRay(subsystem, sensorData);

			switch(sensorData.sensorType)
			{
				case SensorType.Ray:
				if(Physics.Raycast(ray, out _hit, sensorData.distance, subsystem.layerMask)) return true;
				break;

				case SensorType.Box:
				if(Physics.BoxCast(ray.origin, sensorData.dimensions, ray.direction, out _hit, transform.rotation, sensorData.distance, subsystem.layerMask)) return true;
				break;

				case SensorType.Sphere:
				if(Physics.SphereCast(ray, sensorData.radius, out _hit, sensorData.distance, subsystem.layerMask)) return true;
				break;

				case SensorType.Capsule:
				Vector3 destinyPoint = ray.origin + (ray.direction * sensorData.distance);
				if(Physics.CapsuleCast(ray.origin, destinyPoint, sensorData.radius, ray.direction, out _hit, sensorData.distance, subsystem.layerMask)) return true;
				break;
			}
		}

		return false;
	}

	/// <summary>Evaluates if the sensor subsystem detects a hit.</summary>
	/// <param name="_subsystemIndex">Subsystem's Index.</param>
	/// <param name="_hits">Array of RaycastHit information.</param>
	/// <returns>True if hit with something, false otherwise.</returns>
	public bool GetSubsystemDetection(int _subsystemIndex, out RaycastHit[] _hits)
	{
		_hits = null;

		if(!subsystems.CheckIfIndexBetweenBounds(_subsystemIndex))
		{
			Debug.LogError("[SensorSystem3D] Index provided " + _subsystemIndex + " is out of bounds. Returning false.");
			return false;
		}

		SensorSubsystem3D subsystem = subsystems[_subsystemIndex];

		foreach(SensorData3D sensorData in subsystem)
		{
			Ray ray = ConvertToRay(subsystem, sensorData);

			switch(sensorData.sensorType)
			{
				case SensorType.Ray:
				_hits = Physics.RaycastAll(ray, sensorData.distance, subsystem.layerMask);
				break;

				case SensorType.Box:
				_hits = Physics.BoxCastAll(ray.origin, sensorData.dimensions, ray.direction, transform.rotation, sensorData.distance, subsystem.layerMask);
				break;

				case SensorType.Sphere:
				_hits = Physics.SphereCastAll(ray, sensorData.radius, sensorData.distance, subsystem.layerMask);
				break;

				case SensorType.Capsule:
				Vector3 destinyPoint = ray.origin + (ray.direction * sensorData.distance);
				_hits = Physics.CapsuleCastAll(ray.origin, destinyPoint, sensorData.radius, ray.direction, sensorData.distance, subsystem.layerMask);
				break;
			}

			if(_hits.Length > 0) return true;
		}

		return false;
	}

	/// <summary>Evaluates if the sensor subsystem detects a hit.</summary>
	/// <param name="_subsystemIndex">Subsystem's Index.</param>
	/// <param name="_colliders">Array of potential Collider.</param>
	/// <returns>True if hit with something, false otherwise.</returns>
	public bool GetSubsystemDetection(int _subsystemIndex, out Collider[] _colliders)
	{
		_colliders = null;

		if(!subsystems.CheckIfIndexBetweenBounds(_subsystemIndex))
		{
			Debug.LogError("[SensorSystem3D] Index provided " + _subsystemIndex + " is out of bounds. Returning false.");
			return false;
		}

		SensorSubsystem3D subsystem = subsystems[_subsystemIndex];

		foreach(SensorData3D sensorData in subsystem)
		{
			Ray ray = ConvertToRay(subsystem, sensorData);

			switch(sensorData.sensorType)
			{
				case SensorType.Ray:
				Debug.LogError("[SensorSystem3D] There is no method to provide colliders with a Ray sensor. Returning false");
				continue;

				case SensorType.Box:
				_colliders = Physics.OverlapBox(ray.origin, sensorData.dimensions, transform.rotation, subsystem.layerMask);
				break;

				case SensorType.Sphere:
				_colliders = Physics.OverlapSphere(ray.origin, sensorData.radius, subsystem.layerMask);
				break;

				case SensorType.Capsule:
				Vector3 destinyPoint = ray.origin + (ray.direction * sensorData.distance);
				_colliders = Physics.OverlapCapsule(ray.origin, destinyPoint, sensorData.radius, subsystem.layerMask);
				break;
			}

			if(_colliders.Length > 0) return true;
		}

		return false;
	}
#endregion

	/// <summary>Gets origin relative to Subsystem's data and Sensor's data.</summary>
	/// <param name="_subsystem">Subsystem's Data.</param>
	/// <param name="_sensorData">Sensor's Data.</param>
	/// <returns>Origin point relative to subsystem and sensor's data.</returns>
	private Vector3 GetRelativeOriginPoint(SensorSubsystem3D _subsystem, SensorData3D _sensorData)
	{
		switch(_subsystem.relativeTo)
		{
			case RelativeTo.Transform: 		return transform.TransformPoint(_subsystem.relativeOrigin * _subsystem.originDistance);
			case RelativeTo.RendererBounds: return transform.TransformPoint(Vector3.Scale(_subsystem.relativeOrigin, renderer.bounds.extents) * _subsystem.originDistance);
			case RelativeTo.ColliderBounds: return transform.TransformPoint(Vector3.Scale(_subsystem.relativeOrigin, collider.bounds.extents) * _subsystem.originDistance);
			default: 						return Vector3.zero;
		}
	}

	/// <summary>Converts Subsystem's and Sensor's Data into a Ray.</summary>
	/// <param name="_subsystem">Subsystem's Data.</param>
	/// <param name="_sensorData">Sensor's Data.</param>
	/// <returns>Ray interpreted from both Subsystem and Sensor Data.</returns>
	private Ray ConvertToRay(SensorSubsystem3D _subsystem, SensorData3D _sensorData)
	{
		Ray ray = default(Ray);

		ray.origin = transform.TransformDirection(_sensorData.origin);
		ray.direction = (transform.TransformDirection(_sensorData.direction) * _sensorData.distance);

		switch(_subsystem.relativeTo)
		{
			case RelativeTo.Transform:
			ray.origin += transform.TransformPoint(_subsystem.relativeOrigin * _subsystem.originDistance);
			break;

			case RelativeTo.RendererBounds:
			ray.origin += transform.TransformPoint(Vector3.Scale(_subsystem.relativeOrigin, renderer.bounds.extents) * _subsystem.originDistance);
			break;

			case RelativeTo.ColliderBounds:
			ray.origin += transform.TransformPoint(Vector3.Scale(_subsystem.relativeOrigin, collider.bounds.extents) * _subsystem.originDistance);
			break;
		}

		return ray;
	}
}
}