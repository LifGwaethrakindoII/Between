using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyVelocityAccumulator : MonoBehaviour
{
	[SerializeField] private Vector3 _gravityVelocity; 	/// <summary>Gravity's velocity.</summary>
	[SerializeField] private float _gravityMultiplier; 	/// <summary>Gravity's Multiplier.</summary>
	[SerializeField] private bool _useGravity; 			/// <summary>Use Gravity?.</summary>
	private Rigidbody _rigidbody; 						/// <summary>Rigidboy'c Component.</summary>
	private Queue<Vector3> _accumulatedVelocities; 		/// <summary>Accumulated velocities on one frame.</summary>
	private Vector3 _velocity; 							/// <summary>Velocity applied to Rigidbody at end of frame.</summary>

	/// <summary>Gets and Sets gravityVelocity property.</summary>
	public Vector3 gravityVelocity
	{
		get { return _gravityVelocity; }
		set { _gravityVelocity = value; }
	}

	/// <summary>Gets and Sets gravityMultiplier property.</summary>
	public float gravityMultiplier
	{
		get { return _gravityMultiplier; }
		set { _gravityMultiplier = value; }
	}

	/// <summary>Gets and Sets useGravity property.</summary>
	public bool useGravity
	{
		get { return _useGravity; }
		set
		{
			rigidbody.useGravity = false;
			_useGravity = value;
		}
	}

	/// <summary>Gets and Sets accumulatedVelocities property.</summary>
	public Queue<Vector3> accumulatedVelocities
	{
		get { return _accumulatedVelocities; }
		private set { _accumulatedVelocities = value; }
	}

	/// <summary>Gets and Sets velocity property.</summary>
	public Vector3 velocity
	{
		get { return _velocity; }
		private set { _velocity = value; }
	}

	/// <summary>Gets and Sets rigidbody Component.</summary>
	public Rigidbody rigidbody
	{ 
		get
		{
			if(_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}
			return _rigidbody;
		}
	}

#region UnityMethods:
	void Reset()
	{
		gravityVelocity = new Vector3(0f, -1f, 0f);
		gravityMultiplier = 1.0f;
		useGravity = true;
	}

	/// <summary>RigidbodyVelocityAccumulator's' instance initialization.</summary>
	void Awake()
	{
		rigidbody.useGravity = false;
		accumulatedVelocities = new Queue<Vector3>();
	}
	
	/// <summary>RigidbodyVelocityAccumulator's tick at the end of each frame.</summary>
	void LateUpdate()
	{
		velocity = Vector3.zero;

		for(int i = 0; i < accumulatedVelocities.Count; i++)
		{
			velocity += accumulatedVelocities.Dequeue();
		}

		if(useGravity) velocity += (gravityVelocity * gravityMultiplier);

		rigidbody.velocity = velocity;
	}
#endregion

	/// <summary>Enqueues velocity to accumulated velocities queue.</summary>
	/// <param name="_velocity">Velocity to enqueue.</param>
	public void AccumulateVelocity(Vector3 _velocity)
	{
		accumulatedVelocities.Enqueue(_velocity);
	}
}
}