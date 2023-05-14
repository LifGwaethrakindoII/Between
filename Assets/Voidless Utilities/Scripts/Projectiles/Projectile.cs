using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class Projectile : MonoBehaviour, IProjectile, IPoolObject
{
	public event OnPoolObjectDeactivation onPoolObjectDeactivation; 	/// <summary>Event invoked when this Pool Object is being deactivated.</summary>
	public event OnProjectileHit onProjectileHit; 						/// <summary>OnProjectileHit's delegate event.</summary>
	public event OnProjectileHits onProjectileHits; 					/// <summary>OnProjectileHits' delegate event.</summary>

	[SerializeField] private bool _dontDestroyOnLoad; 					/// <summary>Should this PoolObject be destroyed on load?.</summary>
	[SerializeField] private bool _shouldBeOnListSlots; 				/// <summary>Should this Pool Object be on the Pool Slots?.</summary>
	[SerializeField] private float _speed; 								/// <summary>Projectile's Speed.</summary>
	private bool _active; 												/// <summary>Is this Pool Object active?.</summary>
	private int _poolDictionaryID; 										/// <summary>Pool Object's ID on the Pool Dictionary.</summary>
	private int _slotID; 												/// <summary>Pool Object's ID on the Pool Slot.</summary>
	private DisplacementAbility _displacement; 							/// <summary>Displacement's Ability Component.</summary>

	/// <summary>Gets and Sets dontDestroyOnLoad property.</summary>
	public bool dontDestroyOnLoad
	{
		get { return _dontDestroyOnLoad; }
		set { _dontDestroyOnLoad = value; }
	}

	/// <summary>Gets and Sets shouldBeOnListSlots property.</summary>
	public bool shouldBeOnListSlots
	{
		get { return _shouldBeOnListSlots; }
		set { _shouldBeOnListSlots = value; }
	}

	/// <summary>Gets and Sets active property.</summary>
	public bool active
	{
		get { return _active; }
		set { _active = value; }
	}

	/// <summary>Gets and Sets speed property.</summary>
	public float speed
	{
		get { return _speed; }
		set { _speed = value; }
	}

	/// <summary>Gets and Sets poolDictionaryID property.</summary>
	public int poolDictionaryID
	{
		get { return _poolDictionaryID; }
		set { _poolDictionaryID = value; }
	}

	/// <summary>Gets and Sets slotID property.</summary>
	public int slotID
	{
		get { return _slotID; }
		set { _slotID = value; }
	}

	/// <summary>Gets and Sets displacement Component.</summary>
	public DisplacementAbility displacement
	{ 
		get
		{
			if(_displacement == null)
			{
				_displacement = GetComponent<DisplacementAbility>();
			}
			return _displacement;
		}
	}

#region UnityMethods:
	void OnEnable()
	{
		active = true;
	}

	void OnDisable()
	{
		if(onPoolObjectDeactivation != null) onPoolObjectDeactivation(this);
		active = false;
	}

	/// <summary>Projectile's instance initialization.</summary>
	void Awake()
	{
		displacement.displacementSpeed = speed;
	}

	/// <summary>Projectile's starting actions before 1st Update frame.</summary>
	void Start ()
	{
		
	}
	
	/// <summary>Projectile's tick at each frame.</summary>
	void Update ()
	{
		displacement.CastAbility(transform.forward, Space.World);
	}
#endregion

#region PoolObjectMethods:
	/// <summary>Independent Actions made when this Pool Object is being created.</summary>
	public virtual void OnObjectCreation()
	{
		gameObject.SetActive(false);
	}

	/// <summary>Actions made when this Pool Object is being reseted.</summary>
	public virtual void OnObjectReset()
	{
		gameObject.SetActive(true);
	}

	/// <summary>Actions made when this Pool Object is being destroyed.</summary>
	public virtual void OnObjectDestruction()
	{
		Destroy(gameObject);
	}
#endregion
}
}