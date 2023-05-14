using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessPhysics
{
	public const float PHYSICAS_BIAS = 50f;

	public static float CalculateForceForDesiredVelocity(this Rigidbody _rigidbody, float _velocity)
	{
		return (_velocity * (_rigidbody.mass * PHYSICAS_BIAS));
	}

	public static Vector3 CalculateForceForDesiredVelocity(this Rigidbody _rigidbody, Vector3 _velocity)
	{
		return (_velocity * (_rigidbody.mass * PHYSICAS_BIAS));
	}

	public static void AddDesiredForce(this Rigidbody _rigidbody, Vector3 _velocity, float _bias = PHYSICAS_BIAS)
	{
		_rigidbody.AddForce(_velocity * (_rigidbody.mass * _bias));
	}

	public static List<T> GetAllComponentsInOverLapSphere<T>(Vector3 _origin, float _radius, int _mask = Physics.AllLayers, QueryTriggerInteraction _queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) where T : Component
	{
		T component = null;
		Collider[] colliders = Physics.OverlapSphere(_origin, _radius, _mask, _queryTriggerInteraction);

		if(colliders.Length > 1)
		{
			List<T> list = new List<T>();

			foreach(Collider collider in colliders)
			{
				component = collider.gameObject.GetComponent<T>();
				if(component != null) list.Add(component);
			}

			return list;
		}
		else return null;
	}

	public static void ForEachComponentInOverlapSphere<T>(Vector3 _origin, float _radius, Action<T> action, int _mask = Physics.AllLayers, QueryTriggerInteraction _queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) where T : UnityEngine.Object
	{
		T component = null;
		Collider[] colliders = Physics.OverlapSphere(_origin, _radius, _mask, _queryTriggerInteraction);

		for(int i = 0; i < colliders.Length; i++)
		{
			component = colliders[i].gameObject.GetComponent<T>();
			if(component != null) action(component);	
		}
	}
}
}