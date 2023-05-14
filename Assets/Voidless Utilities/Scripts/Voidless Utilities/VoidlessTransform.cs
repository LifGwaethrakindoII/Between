using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessTransform
{
	/// <summary>Returns a Vector3 relative to the space given.</summary>
	/// <param name="_transform">Transform that requests the relative to space Vector.</param>
	/// <param name="_vector">Vector to evaluate.</param>
	/// <param name="_space">Space relativeness.</param>
	/// <returns>Vector relative to the given space, local space ig given 'Self', world space if 'World' given.</returns>
	public static Vector3 RelativeTo(this Transform _transform, Vector3 _vector, Space _space)
	{
		return _space == Space.World ? _vector : _transform.TransformDirection(_vector);
	}
}
}