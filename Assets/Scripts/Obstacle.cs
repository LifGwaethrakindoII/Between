using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoidlessUtilities;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PoolGameObject))]
public class Obstacle : MonoBehaviour
{
	[SerializeField] private LayerMask _boundaryMask; 					/// <summary>Boundary's Mask.</summary>
	[SerializeField] private NormalizedVector3 _displacementDirection; 	/// <summary>Displacement's Direction.</summary>
	[SerializeField] private float _speed; 								/// <summary>Displacement's Speed.</summary>
	[SerializeField] private TrailRenderer _trailRenderer; 				/// <summary>Rednerer's Component.</summary>
	private Rigidbody2D	_rigidbody; 									/// <summary>Rigidbody's Component.</summary>

	/// <summary>Gets boundaryMask property.</summary>
	public LayerMask boundaryMask { get { return _boundaryMask; } }

	/// <summary>Gets displacementDirection property.</summary>
	public NormalizedVector3 displacementDirection { get { return _displacementDirection; } }

	/// <summary>Gets speed property.</summary>
	public float speed { get { return _speed; } }

	/// <summary>Gets and Sets trailRenderer Component.</summary>
	public TrailRenderer trailRenderer
	{ 
		get
		{
			if(_trailRenderer == null)
			{
				_trailRenderer = GetComponent<TrailRenderer>();
			}
			return _trailRenderer;
		}
	}

	/// <summary>Gets and Sets rigidbody Component.</summary>
	public Rigidbody2D rigidbody
	{ 
		get
		{
			if(_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody2D>();
			}
			return _rigidbody;
		}
	}

	private void OnEnable()
	{
		rigidbody.velocity = Vector2.zero;
		trailRenderer.enabled = true;
	}

	private void OnDisable()
	{
		trailRenderer.enabled = false;
	}

	private void FixedUpdate()
	{
		rigidbody.AddForce(transform.TransformDirection(displacementDirection) * speed, ForceMode2D.Force);
	}

	/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject obj = col.gameObject;
	
		if(obj.IsOnLayerMask(boundaryMask)) gameObject.SetActive(false);	
	}
}
