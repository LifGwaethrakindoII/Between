﻿using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[Serializable]
public struct TransformData : ISerializationCallbackReceiver
{
	[SerializeField] private Vector3 _position; 	/// <summary>Transform's Data Position.</summary>
	[SerializeField] private Quaternion _rotation; 	/// <summary>Transform's Data Rotation.</summary>
	[SerializeField] private Vector3 _eulerAngles; 	/// <summary>Rotation representaiton in angles.</summary>
	[SerializeField] private Vector3 _scale; 		/// <summary>Transform's Data Scale.</summary>

	/// <summary>Gets and Sets position property.</summary>
	public Vector3 position
	{
		get { return _position; }
		set { _position = value; }
	}

	/// <summary>Gets and Sets rotation.eulerAngles.</summary>
	public Vector3 eulerAngles
	{
		get { return rotation.eulerAngles; }
		set
		{
			Quaternion rotationQuaternion = rotation;
	    	rotationQuaternion.eulerAngles = _eulerAngles = value;
	    	rotation = rotationQuaternion;
		}
	}

	/// <summary>Gets and Sets rotation property.</summary>
	public Quaternion rotation
	{
		get { return _rotation; }
		set
		{
			_rotation = value;
			_eulerAngles = rotation.eulerAngles;
		}
	}

	/// <summary>Gets and Sets scale property.</summary>
	public Vector3 scale
	{
		get { return _scale; }
		set { _scale = value; }
	}

	/// <summary>Implicit Transform to TransformData operator.</summary>
	public static implicit operator TransformData(Transform _transform) { return new TransformData(_transform); }

	/// <summary>TransformData's constructor.</summary>
	/// <param name="_transform">Transform to retrieve data from.</param>
	public TransformData(Transform _transform) : this()
	{
		position = _transform.position;
		rotation = _transform.rotation;
		scale = _transform.localScale;
	}

	/// <summary>TransformData's constructor.</summary>
	/// <param name="_position">Position.</param>
	/// <param name="_rotation">Rotation.</param>
	/// <param name="_scale">Scale.</param>
	public TransformData(Vector3 _position, Quaternion _rotation, Vector3 _scale) : this()
	{
		position = _position;
		rotation = _rotation;
		scale = _scale;
	}

	/// <summary>TransformData's constructor.</summary>
	/// <param name="_position">Position.</param>
	/// <param name="_rotation">Rotation.</param>
	public TransformData(Vector3 _position, Quaternion _rotation) : this()
	{
		position = _position;
		rotation = _rotation;
	}

	/// <summary>TransformData's constructor.</summary>
	/// <param name="_position">Position.</param>
	/// <param name="_eulerAngles">Rotation in Euler.</param>
	/// <param name="_scale">Scale.</param>
	public TransformData(Vector3 _position, Vector3 _eulerAngles, Vector3 _scale) : this()
	{
		position = _position;
		eulerAngles = _eulerAngles;
		scale = _scale;
	}

	/// <summary>TransformData's constructor.</summary>
	/// <param name="_position">Position.</param>
	/// <param name="_eulerAngles">Rotation in Euler.</param>
	public TransformData(Vector3 _position, Vector3 _eulerAngles) : this()
	{
		position = _position;
		eulerAngles = _eulerAngles;
	}

	/// <summary>Implement this method to receive a callback before Unity serializes your object.</summary>
	public void OnBeforeSerialize()
    {
    	Quaternion rotationQuaternion = rotation;
    	rotationQuaternion.eulerAngles = _eulerAngles;
    	rotation = rotationQuaternion;
    }

    /// <summary>Implement this method to receive a callback after Unity deserializes your object.</summary>
    public void OnAfterDeserialize()
    {
    	Quaternion rotationQuaternion = rotation;
    	rotationQuaternion.eulerAngles = _eulerAngles;
    	rotation = rotationQuaternion;
    }

	public override string ToString()
	{
		StringBuilder builder = new StringBuilder();

		builder.Append("Transform's Data: ");
		builder.Append("\n");
		builder.Append("{");
		builder.Append("\n\t");
		builder.Append("Position: ");
		builder.Append(position.ToString());
		builder.Append("\n\t");
		builder.Append("Quaternion Rotation: ");
		builder.Append(rotation.ToString());
		builder.Append("\n\t");
		builder.Append("Euler Rotation: ");
		builder.Append(eulerAngles.ToString());
		builder.Append("\n\t");
		builder.Append("Scale: ");
		builder.Append(scale.ToString());
		builder.Append("\n");
		builder.Append("}");

		return builder.ToString();
	}
}
}