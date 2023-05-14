using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
#if UNITY_EDITOR_5_6
[RequireComponent(typeof(T))]
#endif
public abstract class BaseGizmosMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public static readonly string LABEL_DRAW_WHEN_SELECTED = "Draw When Selected? "; 	/// <summary>Label for drawWhenSelectedProperty [used for Inspector classes].</summary>

	[SerializeField] private bool _drawWhenSelected; 									/// <summary>Just draw this Gizmo when selected? Otherwise, it will draw in any case.</summary>
	private T _target; 																	/// <summary>Target Monobehaviour attached to this same GameObject.</summary>

	/// <summary>Gets and Sets drawWhenSelected property.</summary>
	public bool drawWhenSelected
	{
		get { return _drawWhenSelected; }
		set { _drawWhenSelected = value; }
	}

	/// <summary>Gets and Sets target Component.</summary>
	public T target
	{ 
		get
		{
			if(_target == null)
			{
				_target = GetComponent<T>();
			}
			return _target;
		}
	}

	/// <summary>Draws Gizmos, even if not selected.</summary>
	void OnDrawGizmos()
	{
		if(!drawWhenSelected) DrawGizmos();
	}

	/// <summary>Draws Gizmos only when selected.</summary>
	void OnDrawGizmosSelected()
	{
		if(drawWhenSelected) DrawGizmos();
	}

	protected abstract void DrawGizmos();
}
}