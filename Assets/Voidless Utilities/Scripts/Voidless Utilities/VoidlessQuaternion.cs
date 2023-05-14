using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessQuaternion
{
	/// <summary>Gets the rotations (Quaternion) from a list of Transforms.</summary>
	/// <param name="_list">The list of Transforms from where the Quaternion list will be created.</param>
	/// <returns>List of the Transform rotation (Quaternion).</returns>
	public static List<Quaternion> GetRotations(this List<Transform> _list)
	{
		List <Quaternion> newList = new List<Quaternion>();

		foreach(Transform _transform in _list)
		{
			newList.Add(_transform.rotation);
		}

		return newList;
	}

	/// <summary>Gets the rotations (Quaternion) from a list of GameObjects.</summary>
	/// <param name="_list">The list of GameObjects from where the Quaternion list will be created.</param>
	/// <returns>List of the Transform rotation (Quaternion).</returns>
	public static List<Quaternion> GetRotations(this List<GameObject> _list)
	{
		List <Quaternion> newList = new List<Quaternion>();

		foreach(GameObject _gameObject in _list)
		{
			if(_gameObject != null) newList.Add(_gameObject.transform.rotation);
		}

		return newList;
	}

	/// <summay>Sets Quaternion.Euler Y component.</summary>
	/// <param name="_quaternion">Queternion that will have its eulerAnglles.y modified.</param>
	/// <param name="_y">Ne Y component value.</param>
	/// <returns>Quaternion with eulerAngles.y modified.</returns>
	public static Quaternion SetY(this Quaternion _quaternion, float _y)
	{
		return Quaternion.Euler(_quaternion.eulerAngles.x, _y, _quaternion.eulerAngles.z);
	}	
}
}