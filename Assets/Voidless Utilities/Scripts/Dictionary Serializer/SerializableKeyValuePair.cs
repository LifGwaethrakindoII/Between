﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VoidlessUtilities
{
[System.Serializable]
public abstract class SerializableKeyValuePair<T, U>
{
	[SerializeField] private T _key; 	/// <summary>SerializableKeyValuePair's ID.</summary>
	[SerializeField] private U _value; 	/// <summary>SerializableKeyValuePair's Value.</summary>

	/// <summary>Gets and Sets key property.</summary>
	public T key
	{
		get { return _key; }
		set { _key = value; }
	}

	/// <summary>Gets and Sets value property.</summary>
	public U value
	{
		get { return _value; }
		set { _value = value; }
	}

	/// <summary>Gives you a Dictionary Item of T key and U value.</summary>
	/// <returns>Parsed Class's T and U properties into a Dictionary Item [KeyValuePair].</returns>
	public KeyValuePair<T, U> ToDictionaryItem()
	{
		return new KeyValuePair<T, U>(key, value);
	}
}
}