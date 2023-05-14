using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
/// <summary>Event invoked when this Hit Collider hits a gameobject.</summary>
/// <param name="_gameObjecy">GameObject that was involved on the Hit Event.</param>
/// <param name="_eventType">Type of the event.</param>
public delegate void OnHitColliderEvent(GameObject _gameObject, HitColliderEventTypes _eventType);

public class HitCollider : MonoBehaviour, ILayerAffecter, ITagsAffecter
{
	public event OnHitColliderEvent onHitColliderEvent; 					/// <summary>OnHitColliderEvent event delegate.</summary>

	[SerializeField] private HitColliderEventTypes _detectableHitEvents; 	/// <summary>Detectablie Hit's Events.</summary>
	[SerializeField] private LayerMask _affectedLayer;  					/// <summary>Affected LayerMasks.</summary>
	[SerializeField] private string[] _affectedTags;  						/// <summary>Affected Tags.</summary>
	private Collider _collider; 											/// <summary>Collider's Component.</summary>

	/// <summary>Gets and Sets detectableHitEvents property.</summary>
	public HitColliderEventTypes detectableHitEvents
	{
		get { return _detectableHitEvents; }
		set { _detectableHitEvents = value; }
	}

	/// <summary>Gets and Sets affectedLayer property.</summary>
	public LayerMask affectedLayer
	{
		get { return _affectedLayer; }
		set { _affectedLayer = value; }
	}

	/// <summary>Gets and Sets affectedTags property.</summary>
	public string[] affectedTags
	{
		get { return _affectedTags; }
		set { _affectedTags = value; }
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

	/// <summary>Sets the Collider either as trigger or as a collision collider.</summary>
	/// <param name="_trigger">Set collider as trigger?.</param>
	public void SetTrigger(bool _trigger)
	{
		collider.isTrigger = _trigger;
	}

#region TriggerCallbakcs:
	/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerEnter(Collider col)
	{
		if(!detectableHitEvents.HasFlag(HitColliderEventTypes.Enter)) return;

		GameObject obj = col.gameObject;
	
		if(obj.layer == affectedLayer) if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Enter);
		else
		{
			if(affectedTags != null)
			for(int i = 0; i < affectedTags.Length; i++)
			{
				if(obj.CompareTag(affectedTags[i]))
				if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Enter);
			}
		}
	}

	/// <summary>Event triggered when this Collider stays with another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerStays(Collider col)
	{
		if(!detectableHitEvents.HasFlag(HitColliderEventTypes.Stays)) return;
		
		GameObject obj = col.gameObject;
	
		if(obj.layer == affectedLayer) if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Stays);
		else
		{
			if(affectedTags != null)
			for(int i = 0; i < affectedTags.Length; i++)
			{
				if(obj.CompareTag(affectedTags[i]))
				if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Stays);
			}
		}
	}

	/// <summary>Event triggered when this Collider exits another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerExit(Collider col)
	{
		if(!detectableHitEvents.HasFlag(HitColliderEventTypes.Exit)) return;

		GameObject obj = col.gameObject;
	
		if(obj.layer == affectedLayer) if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Exit);
		else
		{
			if(affectedTags != null)
			for(int i = 0; i < affectedTags.Length; i++)
			{
				if(obj.CompareTag(affectedTags[i]))
				if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Exit);
			}
		}
	}
#endregion

#region CollisionCallbacks:
	/// <summary>Event triggered when this Collider/Rigidbody begun having contact with another Collider/Rigidbody.</summary>
	/// <param name="col">The Collision data associated with this collision Event.</param>
	void OnCollisionEnter(Collision col)
	{
		if(!detectableHitEvents.HasFlag(HitColliderEventTypes.Enter)) return;

		GameObject obj = col.gameObject;
	
		if(obj.layer == affectedLayer) if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Enter);
		else
		{
			if(affectedTags != null)
			for(int i = 0; i < affectedTags.Length; i++)
			{
				if(obj.CompareTag(affectedTags[i]))
				if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Enter);
			}
		}
	}

	/// <summary>Event triggered when this Collider/Rigidbody begun having contact with another Collider/Rigidbody.</summary>
	/// <param name="col">The Collision data associated with this collision Event.</param>
	void OnCollisionStays(Collision col)
	{
		if(!detectableHitEvents.HasFlag(HitColliderEventTypes.Stays)) return;
		
		GameObject obj = col.gameObject;
	
		if(obj.layer == affectedLayer) if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Stays);
		else
		{
			if(affectedTags != null)
			for(int i = 0; i < affectedTags.Length; i++)
			{
				if(obj.CompareTag(affectedTags[i]))
				if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Stays);
			}
		}
	}

	/// <summary>Event triggered when this Collider/Rigidbody began having contact with another Collider/Rigidbody.</summary>
	/// <param name="col">The Collision data associated with this collision Event.</param>
	void OnCollisionExit(Collision col)
	{
		if(!detectableHitEvents.HasFlag(HitColliderEventTypes.Exit)) return;

		GameObject obj = col.gameObject;
	
		if(obj.layer == affectedLayer) if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Exit);
		else
		{
			if(affectedTags != null)
			for(int i = 0; i < affectedTags.Length; i++)
			{
				if(obj.CompareTag(affectedTags[i]))
				if(onHitColliderEvent != null) onHitColliderEvent(obj, HitColliderEventTypes.Exit);
			}
		}
	}
#endregion
}
}