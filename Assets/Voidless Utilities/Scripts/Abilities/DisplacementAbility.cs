using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class DisplacementAbility : Ability, IDisplacementAbility
{
	[SerializeField] private Space _relativeTo; 			/// <summary>The displacement is relative to itself or the World?.</summary>
	[SerializeField] private Axes3D _movementConstrains; 	/// <summary>Movement's constrains.</summary>
	[SerializeField] private float _displacementSpeed; 					/// <summary>Speed's Scalar.</summary>

	/// <summary>Gets and Sets relativeTo property.</summary>
	public Space relativeTo
	{
		get { return _relativeTo; }
		set { _relativeTo = value; }
	}

	/// <summary>Gets and Sets movementConstrains property.</summary>
	public Axes3D movementConstrains
	{
		get { return _movementConstrains; }
		set { _movementConstrains = value; }
	}

	/// <summary>Gets and Sets displacementSpeed property.</summary>
	public float displacementSpeed
	{
		get { return _displacementSpeed; }
		set { _displacementSpeed = value; }
	}

	/// <summary>Casts ability.</summary>
	public override abstract void CastAbility(); 

	/// <summary>Casts Displacement Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public abstract void CastAbility(Vector3 _direction, Space _space = Space.Self);
}
}