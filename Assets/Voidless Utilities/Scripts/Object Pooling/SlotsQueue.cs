using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class SlotsQueue<T> : Queue<T>/*, ISlotCollection*/
{
	private bool _setLimit; 			/// <summary>Set Limit to this Slot?.</summary>
	private int _slotsLimit; 			/// <summary>Slots' Limit.</summary>
	private int _occupiedSlotsCount; 	/// <summary>Occupied Slots' count.</summary>
	private int _vacantSlotsCount; 		/// <summary>Vacant Slots' count.</summary>
	
	/// <summary>Gets and Sets setLimit property.</summary>
	public bool setLimit
	{
		get { return _setLimit; }
		set { _setLimit = value; }
	}

	/// <summary>Gets and Sets slotsLimit property.</summary>
	public int slotsLimit
	{
		get { return _slotsLimit; }
		set { _slotsLimit = value; }
	}

	/// <summary>Gets and Sets occupiedSlotsCount property.</summary>
	public int occupiedSlotsCount
	{
		get { return _occupiedSlotsCount; }
		set { _occupiedSlotsCount = value; }
	}

	/// <summary>Gets and Sets vacantSlotsCount property.</summary>
	public int vacantSlotsCount
	{
		get { return _vacantSlotsCount; }
		set { _vacantSlotsCount = value; }
	}

	/// <summary>SlotsQueue default constructor.</summary>
	public SlotsQueue() : base()
	{
		setLimit = false;
		slotsLimit = -1;
		occupiedSlotsCount = 0;
		vacantSlotsCount = 0;
	}

	/// <summary>Overload SlotsQueue constructor.</summary>
	/// <param name="_quantity">Queue's initial quantity.</param>
	public SlotsQueue(int _quantity) : base(_quantity)
	{
		setLimit = false;
		slotsLimit = -1;
		occupiedSlotsCount = 0;
		vacantSlotsCount = 0;
	}

	/// <summary>Adds an object to the end of the Queue.</summary>
	/// <param name="_item">The object to add to the Queue. The value can be null for reference types.</param>
	public new void Enqueue(T _item)
	{
		base.Enqueue(_item);
		occupiedSlotsCount++;
	}

	/// <summary>Removes and returns the object at the beginning of the Queue.</summary>
	/// <returns>Object at the beginning of the Queue.</returns>
	public new T Dequeue()
	{
		occupiedSlotsCount--;
		return base.Dequeue();
	}

	public void OnItemActivation(T _item)
	{
		occupiedSlotsCount++;
	}

	public void OnItemDeactivation(T _item)
	{
		occupiedSlotsCount--;
	}
}
}