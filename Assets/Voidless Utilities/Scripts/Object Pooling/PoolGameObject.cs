using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class PoolGameObject : MonoBehaviour, IPoolObject
{
	public event OnPoolObjectDeactivation onPoolObjectDeactivation; 	/// <summary>Event invoked when this Pool Object is being deactivated.</summary>

	[SerializeField] private bool _dontDestroyOnLoad; 					/// <summary>Is this Pool Object going to be destroyed when changing scene? [By default it destroys it].</summary>
	private bool _active; 												/// <summary>Is this Pool Object active [preferibaly unavailable to recycle]?.</summary>
	private int _poolDictionaryID; 										/// <summary>Key's ID of this Pool Object on its respectrive pool dictionary.</summary>

	/// <summary>Gets and Sets dontDestroyOnLoad property.</summary>
	public bool dontDestroyOnLoad
	{
		get { return _dontDestroyOnLoad; }
		set { _dontDestroyOnLoad = value; }
	}

	/// <summary>Gets and Sets active property.</summary>
	public bool active
	{
		get { return _active; }
		set { _active = value; }
	}

	/// <summary>Gets and Sets poolDictionaryID property.</summary>
	public int poolDictionaryID
	{
		get { return _poolDictionaryID; }
		set { _poolDictionaryID = value; }
	}

	private void OnEnable()
	{
		active = true;
	}

	private void OnDisable()
	{
		active = false;
		if(onPoolObjectDeactivation != null) onPoolObjectDeactivation(this);
	}

#region IPoolObjectMethods:
	/// <summary>Independent Actions made when this Pool Object is being created.</summary>
	public void OnObjectCreation()
	{
		gameObject.SetActive(false);
	}

	/// <summary>Actions made when this Pool Object is being reseted.</summary>
	public void OnObjectReset()
	{
		gameObject.SetActive(false);
		gameObject.SetActive(true);
	}

	/// <summary>Actions made when this Pool Object is being destroyed.</summary>
	public void OnObjectDestruction()
	{
		Destroy(gameObject);
	}
#endregion
}
}