using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[Serializable]
public struct NormalizedProperty : INumber<float>, IComparable<NormalizedProperty>, IComparable<float>
{
	public const float MIN = 0.0f; 		/// <summary>Minimum Value.</summary>
	public const float MAX = 1.0f; 		/// <summary>Maximum Value.</summary>

	private float _value; 				/// <summary>Nomalized Property's value, between [-1, 1].</summary>

	/// <summary>Implicit NormalizedProperty to float operator.</summary>
	public static implicit operator float(NormalizedProperty _number) { return _number; }

	/// <summary>Implicit float to NormalizedProperty value operator.</summary>
	public static implicit operator NormalizedProperty(float _number) { return new NormalizedProperty(_number); }

	/// <summary>Implicit NormalizedProperty plus float value sum operator.</summary>
	public static NormalizedProperty operator + (NormalizedProperty _n, float _number) { return new NormalizedProperty(_n + _number); }

	/// <summary>Implicit NormalizedProperty plus float value sum operator.</summary>
	public static NormalizedProperty operator - (NormalizedProperty _n, float _number) { return new NormalizedProperty(_n - _number); }

	/// <summary>Define the is greater than operator.</summary>
    public static bool operator >  (NormalizedProperty _n1, NormalizedProperty _n2)
    {
       return _n1.value > (_n2.value - Mathf.Epsilon);
    }

    /// <summary>Define the is less than operator.</summary>
    public static bool operator <  (NormalizedProperty _n1, NormalizedProperty _n2)
    {
       return _n1.value < (_n2.value + Mathf.Epsilon);
    }

    /// <summary>Define the is greater than or equal to operator.</summary>
    public static bool operator >=  (NormalizedProperty _n1, NormalizedProperty _n2)
    {
       return _n1.value >= (_n2.value - Mathf.Epsilon);
    }

    /// <summary>Define the is less than or equal to operator.</summary>
    public static bool operator <=  (NormalizedProperty _n1, NormalizedProperty _n2)
    {
       return _n1.value <= (_n2.value + Mathf.Epsilon);
    }

    /// <summary>Define the is equal to operator.</summary>
    public static bool operator ==  (NormalizedProperty _n1, NormalizedProperty _n2)
    {
       return (_n1.value > (_n2.value - Mathf.Epsilon)) && (_n1.value < (_n2.value + Mathf.Epsilon));
    }

    /// <summary>Define the is different than operator.</summary>
    public static bool operator !=  (NormalizedProperty _n1, NormalizedProperty _n2)
    {
       return (_n1.value < (_n2.value - Mathf.Epsilon)) || (_n1.value > (_n2.value + Mathf.Epsilon));
    }
	
	/// <summary>Gets and Sets value property.</summary>
	public float value
	{
		get { return _value; }
		set { _value = Mathf.Clamp(value, MIN, MAX); }
	}

	/// <summary>NormalizedProperty default constructor.</summary>
	/// <param name="_value">Normalized value, internally clamped.</param>
	public NormalizedProperty(float _value) : this()
	{
		value = Mathf.Clamp(_value, MIN, MAX);
	}

	/// <summary>Compares itself against another NormalizedProperty value.</summary>
	/// <returns>-1 if less, 0 if equal, 1 if greater than given NormalizedProperty value.</returns>
	public int CompareTo(NormalizedProperty _normalizedProperty)
	{
		if(value < _normalizedProperty.value)
		{
			return -1;
		}else if(value == _normalizedProperty.value)
		{
			return 0;
		}else if(value > _normalizedProperty.value)
		{
			return 1;
		}
		else return 0;
	}

	/// <summary>Compares itself against another float value.</summary>
	/// <returns>-1 if less, 0 if equal, 1 if greater than given float value.</returns>
	public int CompareTo(float _value)
	{
		if(value < _value)
		{
			return -1;
		}else if(value == _value)
		{
			return 0;
		}else if(value > _value)
		{
			return 1;
		}
		else return 0;
	}
}
}