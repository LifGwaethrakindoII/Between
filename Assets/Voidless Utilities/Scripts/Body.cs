using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities.VR
{
public class Body : MonoBehaviour
{
	[SerializeField] private TransformProperties _detectionType; 	/// <summary>Properties that this bodyu will update.</summary>
	[SerializeField] private Distance _minimumMagnitudeToChange; 	/// <summary>Minimum magnitude between the velocity to register a change.</summary>
	private Vector3 _velocity; 										/// <summary>Body's Velocity.</summary>
	private Vector3 _angularVelocity; 								/// <summary>Body's Angular Velocity.</summary>
	private Vector3 _accumulatedVelocity; 							/// <summary>Accumulated's Velocity.</summary>
	private Vector3	_accumulatedAngularVelocity; 					/// <summary>Accumulated's Angular Velocity.</summary>
	private Vector3 _lastPosition; 									/// <summary>Body's Last Position.</summary>
	private Vector3 _lastEulerRotation; 							/// <summary>Body's Last Euler Rotation.</summary>

#if UNITY_EDITOR
	[Space(5f)]
	[Header("Debug Options:")]
	[SerializeField] private bool debug; 							/// <summary>Debug Velocities?.</summary>
	[SerializeField] private Color positionRayColor; 				/// <summary>Position Ray's Color.</summary>
	[SerializeField] private Color rotationRayColor; 				/// <summary>Rotation Ray's Color.</summary>
	[SerializeField] private float rayDuration; 					/// <summary>Ray's Duration when debugging.</summary>
#endif

#region Getters/Setters:
	/// <summary>Gets and Sets detectionType property.</summary>
	public TransformProperties detectionType
	{
		get { return _detectionType; }
		set { _detectionType = value; }
	}

	/// <summary>Gets and Sets minimumMagnitudeToChange property.</summary>
	public Distance minimumMagnitudeToChange
	{
		get { return _minimumMagnitudeToChange; }
		set { _minimumMagnitudeToChange = value * value; }
	}

	/// <summary>Gets and Sets velocity property.</summary>
	public Vector3 velocity
	{
		get { return _velocity; }
		private set { _velocity = value; }
	}

	/// <summary>Gets and Sets angularVelocity property.</summary>
	public Vector3 angularVelocity
	{
		get { return _angularVelocity; }
		set { _angularVelocity = value; }
	}

	/// <summary>Gets and Sets accumulatedVelocity property.</summary>
	public Vector3 accumulatedVelocity
	{
		get { return _accumulatedVelocity; }
		private set { _accumulatedVelocity = value; }
	}

	/// <summary>Gets and Sets accumulatedAngularVelocity property.</summary>
	public Vector3 accumulatedAngularVelocity
	{
		get { return _accumulatedAngularVelocity; }
		set { _accumulatedAngularVelocity = value; }
	}

	/// <summary>Gets and Sets lastPosition property.</summary>
	public Vector3 lastPosition
	{
		get { return _lastPosition; }
		private set { _lastPosition = value; }
	}

	/// <summary>Gets and Sets lastEulerRotation property.</summary>
	public Vector3 lastEulerRotation
	{
		get { return _lastEulerRotation; }
		private set { _lastEulerRotation = value; }
	}
#endregion

	private void OnDisable()
	{
		ResetBody();
	}

	private void Awake()
	{
		ResetBody();
	}

	private void Update()
	{
		if(detectionType.HasFlag(TransformProperties.Position)) UpdateVelocity();
		if(detectionType.HasFlag(TransformProperties.Rotation)) UpdateAngularVelocity();
#if UNITY_EDITOR
		if(debug) DebugBody();
#endif
	}

	/// <summary>Updates Velocity's Data.</summary>
	private void UpdateVelocity()
	{
		velocity = (transform.localPosition - lastPosition);
		if(velocity.sqrMagnitude >= minimumMagnitudeToChange * Time.deltaTime)
		accumulatedVelocity += velocity;
		else accumulatedVelocity = Vector3.zero;
		lastPosition = transform.localPosition;
	}

	/// <summary>Updates Angular velocity's Data.</summary>
	private void UpdateAngularVelocity()
	{
		angularVelocity = (transform.localRotation.eulerAngles - lastEulerRotation);
		if(velocity.sqrMagnitude >= minimumMagnitudeToChange * Time.deltaTime)
		accumulatedAngularVelocity = angularVelocity;
		else accumulatedAngularVelocity = Vector3.zero;
		lastEulerRotation = transform.localRotation.eulerAngles;
	}

	/// <summary>Resets Body's Data.</summary>
	private void ResetBody()
	{
		lastPosition = transform.localPosition;
		lastEulerRotation = transform.localRotation.eulerAngles;
		accumulatedVelocity = Vector3.zero;
	}

	/// <summary>Debug Body's Velocities [Only in Editor Mode].</summary>
	private void DebugBody()
	{
#if UNITY_EDITOR
		if(detectionType.HasFlag(TransformProperties.Position)) Debug.DrawRay(transform.position, velocity, positionRayColor, rayDuration);
		if(detectionType.HasFlag(TransformProperties.Rotation)) Debug.DrawRay(transform.rotation.eulerAngles, angularVelocity, rotationRayColor, rayDuration);
#endif
	}
}
}