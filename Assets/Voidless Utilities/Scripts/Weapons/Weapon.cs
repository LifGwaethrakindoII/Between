using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class Weapon : MonoBehaviour, IWeapon
{
	private IWeaponBearer _bearer; 	/// <summary>Weapon's Bearer.</summary>
	
	/// <summary>Gets and Sets bearer property.</summary>
	public IWeaponBearer bearer
	{
		get { return _bearer; }
		set { _bearer = value; }
	}

#region UnityMethods:
	/// <summary>Weapon's instance initialization.</summary>
	void Awake()
	{
		
	}

	/// <summary>Weapon's starting actions before 1st Update frame.</summary>
	void Start ()
	{
		
	}
	
	/// <summary>Weapon's tick at each frame.</summary>
	void Update ()
	{
		
	}
#endregion

	/// <summary>Default Weapon use activator.</summary>
	public abstract void UseWeapon();
}
}