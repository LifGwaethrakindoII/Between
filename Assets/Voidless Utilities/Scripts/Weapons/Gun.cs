using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class Gun<T> : Weapon, IGun<T> where T : MonoBehaviour, IProjectile, IPoolObject
{
	[SerializeField] private Projectile _bullet; 	/// <summary>Bullet.</summary>
	[SerializeField] private T _projectile; 			/// <summary>Gun's Projectile.</summary>
	[SerializeField] private float _fireRate; 			/// <summary>Gun's Fire-Rate.</summary>
	[SerializeField] private float _rechargeDuration; 	/// <summary>Gun's Recharge Duration.</summary>
	[SerializeField] private float _capacity; 			/// <summary>Gun's Capacity.</summary>
	[SerializeField] private Vector3 _gunPointOffset; 	/// <summary>Gun Point's Offset. States the shooting origin.</summary>
#if UNITY_EDITOR
	[SerializeField] private Color _color; 				/// <summary>Gizmos' color.</summary>
	[SerializeField] private float _radius; 			/// <summary>Gizmos' radius.</summary>
#endif
	private ShootAbility _shootAbility; 						/// <summary>Shooting's Ability Component.</summary>

	public IShootAbility shoot {get; set;}

	/// <summary>Gets and Sets fireRate property.</summary>
	public float fireRate
	{
		get { return _fireRate; }
		set { _fireRate = value; }
	}

	/// <summary>Gets and Sets rechargeDuration property.</summary>
	public float rechargeDuration
	{
		get { return _rechargeDuration; }
		set { _rechargeDuration = value; }
	}

	/// <summary>Gets and Sets capacity property.</summary>
	public float capacity
	{
		get { return _capacity; }
		set { _capacity = value; }
	}

	/// <summary>Gets and Sets gunPointOffset property.</summary>
	public Vector3 gunPointOffset
	{
		get { return _gunPointOffset; }
		set { _gunPointOffset = value; }
	}

	/// <summary>Gets and Sets projectile property.</summary>
	public T projectile
	{
		get { return _projectile; }
		set { _projectile = value; }
	}

	/// <summary>Gets and Sets shootAbility Component.</summary>
	public ShootAbility shootAbility
	{ 
		get
		{
			if(_shootAbility == null)
			{
				_shootAbility = GetComponent<ShootAbility>();
			}
			return _shootAbility;
		}
		set { _shootAbility = value; }
	}

#if UNITY_EDITOR
	/// <summary>Gets and Sets color property.</summary>
	public Color color
	{
		get { return _color; }
		set { _color = value; }
	}

	/// <summary>Gets and Sets radius property.</summary>
	public float radius
	{
		get { return _radius; }
		set { _radius = value; }
	}
#endif

#region UnityMethods:
#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = color;
		Gizmos.DrawWireSphere(transform.TransformPoint(gunPointOffset), radius);
	}
#endif

	/// <summary>Gun's instance initialization.</summary>
	void Awake()
	{
		
	}

	/// <summary>Gun's starting actions before 1st Update frame.</summary>
	void Start ()
	{
		
	}
	
	/// <summary>Gun's tick at each frame.</summary>
	void Update ()
	{
		
	}
#endregion

	/// <summary>Default Weapon use activator.</summary>
	public override void UseWeapon()
	{
		shootAbility.CastAbilityAndReturn(_bullet, transform.TransformPoint(gunPointOffset), transform.rotation);
	}

	public virtual T UseWeaponAndReturn(T _projectile)
	{
		return shootAbility.CastAbilityAndReturn(_projectile, transform.TransformPoint(gunPointOffset), transform.rotation);
	}
}
}