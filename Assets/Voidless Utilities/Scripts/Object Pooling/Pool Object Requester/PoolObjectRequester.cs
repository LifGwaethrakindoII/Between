using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public enum RequestPresistence 									/// <summary>Request Persistences Modes.</summary>
{
	RequestOnce, 												/// <summary>Request Once Persistence Mode.</summary>
	RequestUntilAttended 										/// <summary>Request Until Attended Persistence Mode.</summary>
}

public abstract class PoolObjectRequester<T> : MonoBehaviour, ICameraVisibleHandler where T : MonoBehaviour, IPoolObject
{
	[SerializeField] private T _requestedPoolObject; 			/// <summary>Pool Object to Request.</summary>
	[SerializeField] private RequestPresistence _persistence; 	/// <summary>Request Persistence's Mode.</summary>
	[SerializeField] private bool _unableToRequest; 			/// <summary>Is this Requester unable to request a Pool Object? by default false.</summary>
	private T _poolObjectReference; 							/// <summary>Pool Object retrieved's reference.</summary>
	private bool _seenByCamera; 								/// <summary>Has this GameObject been seen by the camera?.</summary>

	/// <summary>Gets and Sets requestedPoolObject property.</summary>
	public T requestedPoolObject
	{
		get { return _requestedPoolObject; }
		protected set { _requestedPoolObject = value; }
	}

	/// <summary>Gets and Sets persistence property.</summary>
	public RequestPresistence persistence
	{
		get { return _persistence; }
		set { _persistence = value; }
	}

	/// <summary>Gets and Sets poolObjectReference property.</summary>
	public T poolObjectReference
	{
		get { return _poolObjectReference; }
		protected set { _poolObjectReference = value; }
	}

	/// <summary>Gets and Sets unableToRequest property.</summary>
	public bool unableToRequest
	{
		get { return _unableToRequest; }
		set { _unableToRequest = value; }
	}

	/// <summary>Gets and Sets seenByCamera property.</summary>
	public bool seenByCamera
	{
		get { return _seenByCamera; }
		set { _seenByCamera = value; }
	}

#region UnityMethods:
	private void OnEnable()
	{
		if(poolObjectReference != null) poolObjectReference.onPoolObjectDeactivation += OnPoolObjectDeactivated;
	}

	private void OnDisable()
	{
		if(poolObjectReference != null) poolObjectReference.onPoolObjectDeactivation -= OnPoolObjectDeactivated;
	}

	/// <summary>Callback invoked when this GameObject is rendered by the camara.</summary>
	private void OnBecameVisible()
	{
		if(Application.isPlaying)
		{
			switch(persistence)
			{
				case RequestPresistence.RequestOnce:
				if(!seenByCamera && !unableToRequest)
				{
					if(poolObjectReference == null || poolObjectReference != null && !poolObjectReference.active)
					ObjectPool.Instance.OnPoolObjectAddToSlotRequest(requestedPoolObject, transform.position, transform.rotation, OnPoolObjectRetrieved);
				}
				break;

				case RequestPresistence.RequestUntilAttended:
				if(!seenByCamera && !unableToRequest)
				{
					if(poolObjectReference == null || poolObjectReference != null && !poolObjectReference.active)
					ObjectPool.Instance.OnPoolObjectAddToSlotRequest(requestedPoolObject, transform.position, transform.rotation, OnPoolObjectRetrieved);
				}
				break;
			}
			
		}
	}

	/// <summary>Callback invoked when this GameObject is not rendered by the camara.</summary>
	private void OnBecameInvisible()
	{
		if(Application.isPlaying)
		{
			seenByCamera = false;
			if(poolObjectReference != null && !poolObjectReference.active)
			ObjectPool.Instance.OnPoolObjectRemoveFromSlotRequest(requestedPoolObject);
		}
	}
#endregion

	/// <summary>Callback invoked when this requester receives a Pool Object.</summary>
	/// <param name="_poolObject">Pool Object retrieved.</param>
	protected virtual void OnPoolObjectRetrieved(IPoolObject _poolObject)
	{
		seenByCamera = true;
		unableToRequest = true;
		poolObjectReference = _poolObject as T;
		poolObjectReference.onPoolObjectDeactivation += OnPoolObjectDeactivated;
	}

	/// <summary>Callback invoked when this reference's Pool Object is deactivated.</summary>
	/// <param name="_poolObject">Pool Object deactivated.</param>
	protected virtual void OnPoolObjectDeactivated(IPoolObject _poolObject)
	{
		unableToRequest = false;
		poolObjectReference.onPoolObjectDeactivation -= OnPoolObjectDeactivated;
		poolObjectReference = null;
	}
}
}