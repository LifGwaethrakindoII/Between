using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessVector3
{
	private static Vector3[] _normals3D; 		/// <summary>Normal direction vectors along the Third-Dimension.</summary>

	/// <summary>Gets and Sets normals3D property.</summary>
	public static Vector3[] normals3D
	{
		get
		{
			if(_normals3D == null)
			{
				_normals3D = new []
				{
					new Vector3(1f, 0f, 0f), 	/// <summary>Right Normal.</summary>
					new Vector3(-1f, 0f, 0f), 	/// <summary>Left Normal.</summary>
					new Vector3(0f, 1f, 0f), 	/// <summary>Up Normal.</summary>
					new Vector3(0f, -1f, 0f), 	/// <summary>Down Normal.</summary>
					new Vector3(0f, 0f, 1f), 	/// <summary>Forward Normal.</summary>
					new Vector3(0f, 0f, -1f), 	/// <summary>Back Normal.</summary>
					new Vector3(1f, 1f, 0f), 	/// <summary>Right & Up Normal.</summary>
					new Vector3(-1f, 1f, 0f), 	/// <summary>Left & Up Normal.</summary>
					new Vector3(1f, -1f, 0f), 	/// <summary>Right & Down Normal.</summary>
					new Vector3(-1f, -1f, 0f), 	/// <summary>Left & Down Normal.</summary>
					new Vector3(1f, 0f, 1f), 	/// <summary>Right & Forward Normal.</summary>
					new Vector3(-1f, 0f, 1f), 	/// <summary>Left & Forward Normal.</summary>
					new Vector3(1f, 0f, -1f), 	/// <summary>Right & Back Normal.</summary>
					new Vector3(-1f, 0f, -1f), 	/// <summary>Left & Back Normal.</summary>
					new Vector3(0f, 1f, 1f), 	/// <summary>Up & Forward Normal.</summary>
					new Vector3(0f, -1f, 1f), 	/// <summary>Down & Forwrd Normal.</summary>
					new Vector3(0f, 1f, -1f), 	/// <summary>Up & Back Normal.</summary>
					new Vector3(0f, -1f, -1f), 	/// <summary>Down & Back Normal.</summary>
					new Vector3(1f, 1f, 1f), 	/// <summary>Right & Up & Forward Normal.</summary>
					new Vector3(-1f, 1f, 1f), 	/// <summary>Left & Up & Forward Normal.</summary>
					new Vector3(1f, -1f, 1f), 	/// <summary>Right & Down & Forward Normal.</summary>
					new Vector3(-1f, -1f, 1f), 	/// <summary>Left & Down & Forward Normal.</summary>
					new Vector3(1f, 1f, -1f), 	/// <summary>Right & Up & Back Normal.</summary>
					new Vector3(-1f, 1f, -1f), 	/// <summary>Left & Up & Back Normal.</summary>
					new Vector3(1f, -1f, -1f), 	/// <summary>Right & Down & Back Normal.</summary>
					new Vector3(-1f, -1f, -1f) 	/// <summary>Left & Down & Back Normal.</summary>
				};
			}
			return _normals3D;
		}
	}

	/// <summary>Calculates a component-wise Vector division.</summary>
	/// <param name="a">Vector A.</param>
	/// <param name="b">Vector B.</param>
	/// <returns>Component-wise division between two vectors.</returns>
	public static Vector3 Division(Vector3 a, Vector3 b)
	{
		return new Vector3
		(
			(a.x / b.x),
			(a.y / b.y),
			(a.z / b.z)
		);
	}

	/// <summary>Generates a projected Vector relative to Transform's normals.</summary>
	/// <param name="_projectedVector">Vector to project relative to normals.</param>
	/// <returns>Projected Vector relative to Transform's normals.</returns>
	public static Vector3 RelativeProjectedVector(this Transform _transform, Vector3 _projectedVector)
	{
		return (_transform.position + _transform.TransformDirection(_projectedVector));
	}

	/// <summary>Gets the direction vector towards target position.</summary>
	/// <param name="_fromPosition">The position from where de direction points.</param>
	/// <param name="_targetPosition">The position where the _fromPosition heads to.</param>
	/// <returns>Direction towards target point (Vector3).</returns>
	public static Vector3 GetDirectionTowards(this Vector3 _fromPosition, Vector3 _targetPosition)
	{
		return (_targetPosition - _fromPosition);
	}

	/// <summary>Gets the position (Vector3) from a list of Transforms.</summary>
	/// <param name="_transforms">The list of Transforms from where the Vector3 list will be created.</param>
	/// <returns>List of the Transform positions (Vector3).</returns>
	public static List<Vector3> GetPositions(this List<Transform> _transforms)
	{
		List<Vector3> newList = new List<Vector3>(_transforms.Count);

		for(int i = 0; i < _transforms.Count; i++)
		{
			newList.Add(_transforms[i].position);
		}

		return newList;
	}

	/// <summary>Gets the position (Vector3) from a list of GameObjects.</summary>
	/// <param name="_gameObjects">The list of GameObjects from where the Vector3 list will be created.</param>
	/// <returns>List of the GameObject's positions (Vector3).</returns>
	public static List<Vector3> GetPositions(this List<GameObject> _gameObjects)
	{
		List<Vector3> newList = new List<Vector3>(_gameObjects.Count);

		for(int i = 0; i < _gameObjects.Count; i++)
		{
			if(_gameObjects[i] != null) newList.Add(_gameObjects[i].transform.position);
		}
		
		return newList;
	}

	/// <summary>Gets the Vector3 property with the highest value.</summary>
	/// <param name="_vector3">The Vector3 that will compare its components.</param>
	/// <returns>Highest value between Vector3 components.</returns>
	public static float GetMaxVectorProperty(this Vector3 _vector3)
	{
		return Mathf.Max(_vector3.x, _vector3.y, _vector3.z);
	}

	/// <summary>Gets the Vector3 property with the lowest value.</summary>
	/// <param name="_vector3">The Vector3 that will compare its components.</param>
	/// <returns>Lowest value between Vector3 components.</returns>
	public static float GetMinVectorProperty(this Vector3 _vector3)
	{
		return Mathf.Min(_vector3.x, _vector3.y, _vector3.z);
	}

	/// <summary>Gets the distances (float) between List of Vector3 and Vector3.</summary>
	/// <param name="_list">The list of Vector3 from where the distance will be measured.</param>
	/// <param name="_targetPoint">The Vector3 from where the List of Vector3 will measure the distance.</param>
	/// <returns>List of the distances (float).</returns>
	public static List<float> GetDistances(this List<Vector3> _list, Vector3 _targetPoint)
	{
		List<float> newList = new List<float>();

		foreach(Vector3 point in _list)
		{
			newList.Add(Vector3.Distance(point, _targetPoint));
		}

		return newList;
	}

	/// <summary>Gets the distances (float) between List of Vector3 and Vector3.</summary>
	/// <param name="_list">The list of Vector3 from where the distance will be measured.</param>
	/// <param name="_targetPoint">The Vector3 from where the List of Vector3 will measure the distance.</param>
	/// <returns>List of the distances (float).</returns>
	public static List<float> GetDistances(this List<Transform> _list, Vector3 _targetPoint)
	{
		List<float> newList = new List<float>();

		foreach(Transform _transform in _list)
		{
			newList.Add(Vector3.Distance(_transform.position, _targetPoint));
		}

		return newList;
	}

	/// <summary>Gets the distances (float) between List of Vector3 and Vector3.</summary>
	/// <param name="_list">The list of Vector3 from where the distance will be measured.</param>
	/// <param name="_targetPoint">The Vector3 from where the List of Vector3 will measure the distance.</param>
	/// <returns>List of the distances (float).</returns>
	public static List<float> GetDistances(this List<GameObject> _list, Vector3 _targetPoint)
	{
		List<float> newList = new List<float>();

		foreach(GameObject _gameObject in _list)
		{
			newList.Add(Vector3.Distance(_gameObject.transform.position, _targetPoint));
		}

		return newList;
	}

	/// <summary>Gets the Square Magnitudes between List of Vector3 and Target Vector3.</summary>
	/// <param name="List">the List of Vector3 from where the Square Magnitudes will be measured.</param>
	/// <param name="_targetPoint">The Vector3 from where the List of Vector3 will measure the Square Distances.</param>
	/// <returns>List of Square Magnitudes (float).</returns>
	public static List<float> GetSquareMagnitudes(this List <Vector3> _list, Vector3 _targetPoint)
	{
		List<float> newList = new List<float>();

		foreach(Vector3 point in _list)
		{
			newList.Add(point.GetDirectionTowards(_targetPoint).sqrMagnitude);
		}

		return newList;
	}

	/// <summary>Gets the Square Magnitudes between List of Vector3 and Target Vector3.</summary>
	/// <param name="List">the List of Vector3 from where the Square Magnitudes will be measured.</param>
	/// <param name="_targetPoint">The Vector3 from where the List of Vector3 will measure the Square Distances.</param>
	/// <returns>List of Square Magnitudes (float).</returns>
	public static List<float> GetSquareMagnitudes(this List <Transform> _list, Vector3 _targetPoint)
	{
		List<float> newList = new List<float>();

		foreach(Transform _transform in _list)
		{
			newList.Add(_transform.position.GetDirectionTowards(_targetPoint).sqrMagnitude);
		}

		return newList;
	}

	/// <summary>Gets the Square Magnitudes between List of Vector3 and Target Vector3.</summary>
	/// <param name="List">the List of Vector3 from where the Square Magnitudes will be measured.</param>
	/// <param name="_targetPoint">The Vector3 from where the List of Vector3 will measure the Square Distances.</param>
	/// <returns>List of Square Magnitudes (float).</returns>
	public static List<float> GetSquareMagnitudes(this List <GameObject> _list, Vector3 _targetPoint)
	{
		List<float> newList = new List<float>();

		foreach(GameObject _gameObject in _list)
		{
			newList.Add(_gameObject.transform.position.GetDirectionTowards(_targetPoint).sqrMagnitude);
		}

		return newList;
	}

	/// <summary>Generates a regular vector, with all components being given value.</summary>
	/// <param name="_value">Value to give to all Vector's Components.</param>
	/// <returns>Regular Vector3 with all components equal.</returns>
	public static Vector3 Regular(float _value)
	{
		return new Vector3(_value, _value, _value);
	}

	/// <summary>Gets Hick Opposite by given angles.</summary>
	/// <param name="_angle">Angle.</param>
	/// <returns>Hick Opposite.</returns>
	public static float GetHickOppositeGivenAngle(float _angle)
	{
		return (Mathf.Sin(_angle * Mathf.Deg2Rad) /* (_angle > 180.0f ? -1.0f : 1.0f)*/);
	}

	/// <summary>Gets Adyacent Leg by given angles.</summary>
	/// <param name="_angle">Angle.</param>
	/// <returns>Adyacent Leg.</returns>
	public static float GetAsyacentLegByGivenAngle(float _angle)
	{
		return (Mathf.Cos(_angle * Mathf.Deg2Rad) /* (_angle > 180.0f ? -1.0f : 1.0f)*/);
	}

	/// <summary>Rounds Vector3 components.</summary>
	/// <param name="_vector3">The Vector3 that will have its components rounded.</param>
	/// <returns>Vector3 with components rounded (0 or 1).</returns>
	public static Vector3 Round(this Vector3 _vector3)
	{
		return _vector3 = new Vector3(Mathf.Round(_vector3.x), Mathf.Round(_vector3.y), Mathf.Round(_vector3.z));
	}

#region RedundantOperations:
	/// <summary>Sets Vector3 X.</summary>
	/// <param name="_vector">The Vector3 that will have its X modified.</param>
	/// <param name="_x">Updated Vector3 X Component.</param>
	public static Vector3 WithX(this Vector3 _vector, float _x)
	{
		return _vector = new Vector3(_x, _vector.y, _vector.z);
	}

	/// <summary>Sets Vector3 Y.</summary>
	/// <param name="_vector">The Vector3 that will have its Y modified.</param>
	/// <param name="_x">Updated Vector3 Y Component.</param>
	public static Vector3 WithY(this Vector3 _vector, float _y)
	{
		return _vector = new Vector3(_vector.x, _y, _vector.z);
	}

	/// <summary>Sets Vector3 Z.</summary>
	/// <param name="_vector">The Vector3 that will have its Z modified.</param>
	/// <param name="_x">Updated Vector3 Z Component.</param>
	public static Vector3 WithZ(this Vector3 _vector, float _z)
	{
		return _vector = new Vector3(_vector.x, _vector.y, _z);
	}

	/// <summary>Sets Vector3 X and Y.</summary>
	/// <param name="_vector">The Vector3 that will have its X and Y modified.</param>
	/// <param name="_x">Updated Vector3 X Component.</param>
	/// <param name="_y">Updated Vector3 Y Component.</param>
	public static Vector3 WithXAndY(this Vector3 _vector, float _x, float _y)
	{
		return _vector = new Vector3(_x, _y, _vector.z);
	}

	/// <summary>Sets Vector3 X and Z.</summary>
	/// <param name="_vector">The Vector3 that will have its X and Z modified.</param>
	/// <param name="_x">Updated Vector3 X Component.</param>
	/// <param name="_z">Updated Vector3 Z Component.</param>
	public static Vector3 WithXAndZ(this Vector3 _vector, float _x, float _z)
	{
		return _vector = new Vector3(_x, _vector.y, _z);
	}

	/// <summary>Sets Vector3 Y and Z.</summary>
	/// <param name="_vector">The Vector3 that will have its Y and Z modified.</param>
	/// <param name="_y">Updated Vector3 Y Component.</param>
	/// <param name="_z">Updated Vector3 Z Component.</param>
	public static Vector3 WithYAndZ(this Vector3 _vector, float _y, float _z)
	{
		return _vector = new Vector3(_vector.x, _y, _z);
	}

	/// <summary>Adds value to Vector3 X component.</summary>
	/// <param name="_vector">The Vector3 that will have its X subtracted by value.</param>
	/// <param name="_addedX">Added value to Vector3 X Component.</param>
	/// <returns>Vector with subtracted X component by value.</summary>
	public static Vector3 WithAddedX(this Vector3 _vector, float _addedX)
	{
		return _vector = new Vector3((_vector.x + _addedX), _vector.y, _vector.z);
	}

	/// <summary>Adds value to Vector3 Y component.</summary>
	/// <param name="_vector">The Vector3 that will have its Y subtracted by value.</param>
	/// <param name="_addedY">Added value to Vector3 Y Component.</param>
	/// <returns>Vector with subtracted Y component by value.</summary>
	public static Vector3 WithAddedY(this Vector3 _vector, float _addedY)
	{
		return _vector = new Vector3(_vector.x, (_vector.y + _addedY), _vector.z);
	}

	/// <summary>Adds value to Vector3 Z component.</summary>
	/// <param name="_vector">The Vector3 that will have its Z subtracted by value.</param>
	/// <param name="_addedZ">Added value to Vector3 Z Component.</param>
	/// <returns>Vector with subtracted Z component by value.</summary>
	public static Vector3 WithAddedZ(this Vector3 _vector, float _addedZ)
	{
		return _vector = new Vector3(_vector.x, _vector.y, (_vector.z + _addedZ));
	}

	/// <summary>Adds value to Vector3 X and Y components.</summary>
	/// <param name="_vector">The Vector3 that will have its X and Y subtracted by values.</param>
	/// <param name="_addedX">Added value to Vector3 X Component.</param>
	/// <param name="_addedY">Added value to Vector3 Y Component.</param>
	/// <returns>Vector with subtracted X and Y components by values.</summary>
	public static Vector3 WithAddedXAndY(this Vector3 _vector, float _addedX, float _addedY)
	{
		return _vector = new Vector3((_vector.x + _addedX), (_vector.y + _addedY), _vector.z);
	}

	/// <summary>Adds value to Vector3 X and Z components.</summary>
	/// <param name="_vector">The Vector3 that will have its X and Z subtracted by values.</param>
	/// <param name="_addedX">Added value to Vector3 X Component.</param>
	/// <param name="_addedZ">Added value to Vector3 Z Component.</param>
	/// <returns>Vector with subtracted X and Z components by values.</summary>
	public static Vector3 WithAddedXAndZ(this Vector3 _vector, float _addedX, float _addedZ)
	{
		return _vector = new Vector3((_vector.x + _addedX), _vector.y, (_vector.z + _addedZ));
	}

	/// <summary>Adds value to Vector3 Y and Z components.</summary>
	/// <param name="_vector">The Vector3 that will have its Y and Z subtracted by values.</param>
	/// <param name="_addedY">Added value to Vector3 Y Component.</param>
	/// <param name="_addedZ">Added value to Vector3 Z Component.</param>
	/// <returns>Vector with subtracted Y and Z components by values.</summary>
	public static Vector3 WithAddedYAndZ(this Vector3 _vector, float _addedY, float _addedZ)
	{
		return _vector = new Vector3(_vector.x, (_vector.y + _addedY), (_vector.z + _addedZ));
	}

	/// <summary>Inverts Vector3's X component.</summary>
	/// <param name="_vector">The Vector3 that will have its X inverted.</param>
	/// <returns>Vector3 with X component inverted.</returns>
	public static Vector3 InvertX(this Vector3 _vector)
	{
		return _vector = new Vector3(-_vector.x, _vector.y, _vector.z);
	}

	/// <summary>Inverts Vector3's Y component.</summary>
	/// <param name="_vector">The Vector3 that will have its Y inverted.</param>
	/// <returns>Vector3 with Y component inverted.</returns>
	public static Vector3 InvertY(this Vector3 _vector)
	{
		return _vector = new Vector3(_vector.x, -_vector.y, _vector.z);
	}

	/// <summary>Inverts Vector3's Z component.</summary>
	/// <param name="_vector">The Vector3 that will have its Z inverted.</param>
	/// <returns>Vector3 with Z component inverted.</returns>
	public static Vector3 InvertZ(this Vector3 _vector)
	{
		return _vector = new Vector3(_vector.x, _vector.y, -_vector.z);
	}

	/// <summary>Inverts Vector3's X and Y component.</summary>
	/// <param name="_vector">The Vector3 that will have its X and Y inverted.</param>
	/// <returns>Vector3 with X and Y component inverted.</returns>
	public static Vector3 InvertXAndY(this Vector3 _vector)
	{
		return _vector = new Vector3(-_vector.x, -_vector.y, _vector.z);
	}

	/// <summary>Inverts Vector3's X and Z component.</summary>
	/// <param name="_vector">The Vector3 that will have its X and Z inverted.</param>
	/// <returns>Vector3 with X and Z component inverted.</returns>
	public static Vector3 InvertXAndZ(this Vector3 _vector)
	{
		return _vector = new Vector3(-_vector.x, _vector.y, -_vector.z);
	}

	/// <summary>Inverts Vector3's Y and Z component.</summary>
	/// <param name="_vector">The Vector3 that will have its Y and Z inverted.</param>
	/// <returns>Vector3 with Y and Z component inverted.</returns>
	public static Vector3 InvertYAndZ(this Vector3 _vector)
	{
		return _vector = new Vector3(_vector.x, -_vector.y, -_vector.z);
	}
#endregion

	/// <summary>Converts Vector2 to Vector3 format.</summary>
	/// <param name="_vector">The Vector2 that will be converted to Vector3.</param>
	/// <returns>Vector2 converted to Vector3 format.</returns>
	public static Vector3 ToVector2(this Vector3 _vector)
	{
		return new Vector2(_vector.x, _vector.y);
	}

	/// <summary>Clamps Vector to given range.</summary>
	/// <param name="_vector">Vector to clamp.</param>
	/// <param name="_min">Minimum's Value.</param>
	/// <param name="_max">Maximum's Value.</param>
	/// <returns>Clamped Vector by given range.</returns>
	public static Vector3 Clamped(this Vector3 _vector, float _min, float _max)
	{
		return new Vector3
		(
			Mathf.Clamp(_vector.x, Mathf.Min(_min), Mathf.Max(_max)),
			Mathf.Clamp(_vector.y, Mathf.Min(_min), Mathf.Max(_max)),
			Mathf.Clamp(_vector.z, Mathf.Min(_min), Mathf.Max(_max))
		); 
	}

	/// <summary>Turns Vector3's components into absolute values.</summary>
	/// <param name="_vector">Extended Vector3.</param>
	/// <returns>Vector3 with Absolute Values components.</returns>
	public static Vector3 Abs(this Vector3 _vector)
	{
		return new Vector3
		(
			Mathf.Abs(_vector.x),
			Mathf.Abs(_vector.y),
			Mathf.Abs(_vector.z)
		);
	}

	/// <summary>Turns Vector3's components into negative absolute values.</summary>
	/// <param name="_vector">Extended Vector3.</param>
	/// <returns>Vector3 with Negative Absolute Values components.</returns>
	public static Vector3 NegativeAbs(this Vector3 _vector)
	{
		return new Vector3
		(
			VoidlessMath.NegativeAbs(_vector.x),
			VoidlessMath.NegativeAbs(_vector.y),
			VoidlessMath.NegativeAbs(_vector.z)
		);
	}

	/// <summary>Calculates Scalar projection of Vector A into B.</summary>
	/// <param name="a">Vector A.</param>
	/// <param name="b">Vector B.</param>
	/// <returns>Scalar projection.</returns>
	public static float ScalarProjection(Vector3 a, Vector3 b)
	{
		float aDistance = a.magnitude;
		float bDistance = b.magnitude;
		float dot = ((a.x * b.x) + (a.y * b.y) + (a.z * b.z));
		float angle = (Mathf.Acos(dot / (aDistance * bDistance)));

		return aDistance * Mathf.Cos(angle * Mathf.Rad2Deg);
	}

	/// <summary>Calculates Vector projection of Vector A into B.</summary>
	/// <param name="a">Vector A.</param>
	/// <param name="b">Vector B.</param>
	/// <returns>Vector projection.</returns>
	public static Vector3 VectorProjection(Vector3 a, Vector3 b)
	{
		return (b.normalized * ScalarProjection(a, b));
	}

	/// <summary>Gets identity vector of given orientation semantic.</summary>
	/// <param name="_orientation">Orientation's Semantics.</param>
	/// <returns>Identity vector interpreted from orientation semantic.</returns>
	public static Vector3 GetOrientation(OrientationSemantics _orientation)
	{
		Vector3 orientation = Vector3.zero;

		if(_orientation.HasFlag(OrientationSemantics.Right)) orientation += Vector3.right;
		if(_orientation.HasFlag(OrientationSemantics.Left)) orientation += Vector3.left;
		if(_orientation.HasFlag(OrientationSemantics.Up)) orientation += Vector3.up;
		if(_orientation.HasFlag(OrientationSemantics.Down)) orientation += Vector3.down;
		if(_orientation.HasFlag(OrientationSemantics.Forward)) orientation += Vector3.forward;
		if(_orientation.HasFlag(OrientationSemantics.Back)) orientation += Vector3.back;

		return orientation.normalized;
	}

	/// <summary>Gets direction relative to a transform, given an orientation semantic.</summary>
	/// <param name="_transform">Target Transform.</param>
	/// <param name="_orientation">Orientation's Semantics.</param>
	/// <returns>Relative Direction interpreted from Orientation Semantic.</returns>
	public static Vector3 GetOrientationDirection(this Transform _transform, OrientationSemantics _orientation)
	{
		return _transform.TransformDirection(GetOrientation(_orientation));
	}

	public static float GetAverage(this Vector3 _vector)
	{
		return ((_vector.x + _vector.y + _vector.z) / 3.0f);
	}
}
}