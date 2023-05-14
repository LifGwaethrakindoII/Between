using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[Serializable]
public struct CameraGrid
{
	public static readonly float RANGE_MIN = 0.0f; 							/// <summary>Minimum range.</summary>
	public static readonly float RANGE_MID = 0.5f; 							/// <summary>Medium range.</summary>
	public static readonly float RANGE_MAX = 1.0f; 							/// <summary>Maximum range.</summary>

	[SerializeField] [Range(0.0f, 1.0f)] private float _horizontalUp; 		/// <summary>Horizontal-up line normalized position.</summary>
	[SerializeField] [Range(0.0f, 1.0f)] private float _horizontalDown;		/// <summary>Horizontal-down line normalized position.</summary>
	[SerializeField] [Range(0.0f, 1.0f)] private float _verticalLeft;		/// <summary>Vertical-left line normalized position.</summary>
	[SerializeField] [Range(0.0f, 1.0f)] private float _verticalRight; 		/// <summary>Vertical-right line normalized position.</summary>

	/// <summary>Gets and Sets horizontalUp property.</summary>
	public float horizontalUp
	{
		get { return _horizontalUp; }
		set { _horizontalUp = Mathf.Clamp(value, 0.0f, 1.0f); }
	}

	/// <summary>Gets and Sets horizontalDown property.</summary>
	public float horizontalDown
	{
		get { return _horizontalDown; }
		set { _horizontalDown = Mathf.Clamp(value, 0.0f, 1.0f); }
	}

	/// <summary>Gets and Sets verticalLeft property.</summary>
	public float verticalLeft
	{
		get { return _verticalLeft; }
		set { _verticalLeft = Mathf.Clamp(value, 0.0f, 1.0f); }
	}

	/// <summary>Gets and Sets verticalRight property.</summary>
	public float verticalRight
	{
		get { return _verticalRight; }
		set { _verticalRight = Mathf.Clamp(value, 0.0f, 1.0f); }
	}

	/// <summary>CameraGrid's constructor.</summary>
	/// <param name="_horizontalUp">Horizontal-up normalized position.</param>
	/// <param name="_horizontalDown">Horizontal-down normalized position.</param>
	/// <param name="_verticalLeft">Vertical-left normalized position.</param>
	/// <param name="_verticalRight">Vertical-right normalized position.</param>
	public CameraGrid(float _horizontalUp, float _horizontalDown, float _verticalLeft, float _verticalRight) : this()
	{
		horizontalUp = _horizontalUp;
		horizontalDown = _horizontalDown;
		verticalLeft = _verticalLeft;
		verticalRight = _verticalRight;
	}

	/// <summary>Maps Horizontal-Up value relative to the given ranges.</summary>
	/// <returns>Mapped Horizontal-Up value.</returns>
	public float GetMappedHorizontalUp()
	{
		return RANGE_MID + (RANGE_MID * horizontalUp);
	}

	/// <summary>Maps Horizontal-Down value relative to the given ranges.</summary>
	/// <returns>Mapped Horizontal-Down value.</returns>
	public float GetMappedHorizontalDown()
	{
		return RANGE_MIN + (RANGE_MID * horizontalDown);
	}

	/// <summary>Maps Vertical-Left value relative to the given ranges.</summary>
	/// <returns>Mapped Vertical-Left value.</returns>
	public float GetMappedVerticalLeft()
	{
		return RANGE_MIN + (RANGE_MID * verticalLeft);
	}

	/// <summary>Maps Vertical-Right value relative to the given ranges.</summary>
	/// <returns>Mapped Vertical-Right value.</returns>
	public float GetMappedVerticalRight()
	{
		return RANGE_MID + (RANGE_MID * verticalRight);
	}
}
}