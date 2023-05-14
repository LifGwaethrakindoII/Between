using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessVector2
{
	private static Vector2[] _normals2D; 	/// <summary>Normal direction vectors along the Second-Dimension.</summary>

	/// <summary>Gets and Sets normals2D property.</summary>
	public static Vector2[] normals2D
	{
		get
		{
			if(_normals2D == null)
			{
				_normals2D = new []
				{
					new Vector2(1f, 0f), 	/// <summary>Right Cemter Normal.</summary>
					new Vector2(-1f, 0f), 	/// <summary>Left Center Normal.</summary>
					new Vector2(0f, 1f), 	/// <summary>Up Center Normal.</summary>
					new Vector2(0f, -1f), 	/// <summary>Down Center Normal.</summary>
					new Vector2(1f, 1f), 	/// <summary>Up-Right Normal.</summary>
					new Vector2(-1f, -1f), 	/// <summary>Down-Left Normal.</summary>
					new Vector2(1f, -1f), 	/// <summary>Down-Right Normal.</summary>
					new Vector2(-1f, 1f), 	/// <summary>Up-Left Normal.</summary>
				};
			}
			return _normals2D;
		}
	}

	/// <summary>Gets the direction vector towards target position.</summary>
	/// <param name="_fromPosition">The position from where de direction points.</param>
	/// <param name="_targetPosition">The position where the _fromPosition heads to.</param>
	/// <returns>Direction towards target point (Vector2).</returns>
	public static Vector2 GetDirectionTowards(this Vector2 _fromPosition, Vector2 _targetPosition)
	{
		return (_targetPosition - _fromPosition);
	}

	/// <summary>Gets the Vector3 property with the highest value.</summary>
	/// <param name="_vector2">The Vector2 that will compare its components.</param>
	/// <returns>Highest value between Vector2 components.</returns>
	public static float GetMaxVectorProperty(this Vector2 _vector2)
	{
		return Mathf.Max(_vector2.x, _vector2.y);
	}

	/// <summary>Gets the Vector3 property with the lowest value.</summary>
	/// <param name="_vector3">The Vector3 that will compare its components.</param>
	/// <returns>Lowest value between Vector3 components.</returns>
	public static float GetMinVectorProperty(this Vector3 _vector3)
	{
		return Mathf.Min(_vector3.x, _vector3.y, _vector3.z);
	}

	/*/// <summary>Converts Vector3 to Vector2 [Ignores Z].</summary>
	/// <param name="_vector3">Vector3 that will be converted to Vector2.</param>
	/// <returns>Converted Vector2.</returns>
	public static Vector2 ToVector2(this Vector3 _vector3)
	{
		return new Vector2(_vector3.x, _vector3.y);
	}*/

	/// <summary>Sets Vector2 X.</summary>
	/// <param name="_vector">The Vector2 that will have its X modified.</param>
	/// <param name="_x">Updated Vector2 X Component.</param>
	public static Vector2 WithX(this Vector2 _vector, float _x)
	{
		return _vector = new Vector2(_x, _vector.y);
	}

	/// <summary>Sets Vector2 Y.</summary>
	/// <param name="_vector">The Vector2 that will have its Y modified.</param>
	/// <param name="_x">Updated Vector2 Y Component.</param>
	public static Vector2 WithY(this Vector2 _vector, float _y)
	{
		return _vector = new Vector2(_vector.x, _y);
	}

	/// <summary>Converts Vector2 to Vector3 format.</summary>
	/// <param name="_vector">The Vector2 that will be converted to Vector3.</param>
	/// <returns>Vector2 converted to Vector3 format.</returns>
	public static Vector3 ToVector3(this Vector2 _vector)
	{
		return new Vector3(_vector.x, _vector.y, 0f);
	}

	/// <summary>Clamps Vector to given range.</summary>
	/// <param name="_vector">Vector to clamp.</param>
	/// <param name="_min">Minimum's Value.</param>
	/// <param name="_max">Maximum's Value.</param>
	/// <returns>Clamped Vector by given range.</returns>
	public static Vector2 Clamped(this Vector2 _vector, float _min, float _max)
	{
		return new Vector2
		(
			Mathf.Clamp(_vector.x, Mathf.Min(_min), Mathf.Max(_max)),
			Mathf.Clamp(_vector.y, Mathf.Min(_min), Mathf.Max(_max))
		); 
	}

	/// <summary>Turns Vector2's components into absolute values.</summary>
	/// <param name="_vector">Extended Vector2.</param>
	/// <returns>Vector2 with Absolute Values components.</returns>
	public static Vector2 Abs(this Vector2 _vector)
	{
		return new Vector2
		(
			Mathf.Abs(_vector.x),
			Mathf.Abs(_vector.y)
		);
	}
}
}