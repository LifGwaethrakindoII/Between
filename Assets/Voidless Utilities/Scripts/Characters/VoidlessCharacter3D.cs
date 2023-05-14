using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SensorSystem3D))]
public class VoidlessCharacter3D : MonoBehaviour
{
	[SerializeField] private VoidlessCharacter3DStats _stats; 	/// <summary>Character's Stats.</summary>
	[SerializeField] private Vector3 _gravityVelocity; 			/// <summary>Character's own gravity velocity applied.</summary>
	[SerializeField] private float _gravityMultiplier; 			/// <summary>Gravity velocity's multiplier.</summary>
	private Vector3 _forward; 									/// <summary>Character's forward normal.</summary>
	private Vector3 _movementVelocity; 							/// <summary>Character's current movement velocity.</summary>
	private RaycastHit _groundInfo; 							/// <summary>Ground information [if the character is hitting ground].</summary>
	private bool _grounded; 									/// <summary>Is the Character stepping ground?.</summary>
	private Rigidbody _rigidbody; 								/// <summary>Rigidbody's Component.</summary>
	private SensorSystem3D _sensorSystem; 						/// <summary>SensorSystem3D's Component.</summary>
	private Renderer _renderer; 								/// <summary>Renderer's Component.</summary>

#region Getters/Setters:
	/// <summary>Gets and Sets stats property.</summary>
	public VoidlessCharacter3DStats stats
	{
		get { return _stats; }
		protected set { _stats = value; }
	}

	/// <summary>Gets and Sets gravityVelocity property.</summary>
	public Vector3 gravityVelocity
	{
		get { return _gravityVelocity; }
		protected set { _gravityVelocity = value; }
	}

	/// <summary>Gets and Sets forward property.</summary>
	public Vector3 forward
	{
		get { return _forward; }
		set { _forward = value; }
	}

	/// <summary>Gets and Sets movementVelocity property.</summary>
	public Vector3 movementVelocity
	{
		get { return _movementVelocity; }
		set { _movementVelocity = value; }
	}

	/// <summary>Gets and Sets gravityMultiplier property.</summary>
	public float gravityMultiplier
	{
		get { return _gravityMultiplier; }
		protected set { _gravityMultiplier = value; }
	}

	/// <summary>Gets and Sets grounded property.</summary>
	public bool grounded
	{
		get { return _grounded; }
		set { _grounded = value; }
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

	/// <summary>Gets and Sets sensorSystem Component.</summary>
	public SensorSystem3D sensorSystem
	{ 
		get
		{
			if(_sensorSystem == null)
			{
				_sensorSystem = GetComponent<SensorSystem3D>();
			}
			return _sensorSystem;
		}
	}

	/// <summary>Gets and Sets renderer Component.</summary>
	public Renderer renderer
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
#endregion

#region UnityMethods:
	/// <summary>VoidlessCharacter3D's' instance initialization.</summary>
	void Awake()
	{
		//rigidbody.useGravity = false;
	}

	/// <summary>VoidlessCharacter3D's starting actions before 1st Update frame.</summary>
	void Start ()
	{
		
	}
	
	/// <summary>VoidlessCharacter3D's tick at each frame.</summary>
	void Update ()
	{
		grounded = sensorSystem.GetSubsystemDetection(0);
	}

	/// <summary>Physics' Threat Update.</summary>
	void FixedUpdate()
	{
		ApplyGravity();
	}
#endregion

	/// <summary>Applies gravity to Character.</summary>
	protected void ApplyGravity()
	{
		if(!grounded) rigidbody.velocity -= (gravityVelocity * gravityMultiplier * Time.fixedDeltaTime);
	}

	/// <summary>Moves Character.</summary>
	protected virtual void Move()
	{
		//rigidbody.velocity += (transform.forward.normalized * (stats.movement.Accelerate() * Time.deltaTime));
	}

	/// <summary>Makes Character jump.</summary>
	protected virtual void Jump()
	{
		//rigidbody.velocity += (gravityVelocity.normalized * (stats.jump.Accelerate() * Time.deltaTime));
	}
}
}