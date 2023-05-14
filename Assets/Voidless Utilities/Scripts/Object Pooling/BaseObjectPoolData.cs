using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class BaseObjectPoolData<T> : ISlotCollection
{
	private Queue<T> _poolObjectsQueue;
	private Transform _group;
	private bool _ignoreIfPeekedElementIsActive;
	private int _slotsSize;
	private int _occupiedSlotsCount;
	private int _vacantSlotsCount;

	/// <summary>Gets and Sets poolObjectsQueue property.</summary>
	public Queue<T> poolObjectsQueue
	{
		get { return _poolObjectsQueue; }
		set { _poolObjectsQueue = value; }
	}

	/// <summary>Gets and Sets group property.</summary>
	public Transform group
	{
		get { return _group; }
		set { _group = value; }
	}

	/// <summary>Gets and Sets ignoreIfPeekedElementIsActive property.</summary>
	public bool ignoreIfPeekedElementIsActive
	{
		get { return _ignoreIfPeekedElementIsActive; }
		set { _ignoreIfPeekedElementIsActive = value; }
	}

	/// <summary>Gets and Sets slotsSize property.</summary>
	public int slotsSize
	{
		get { return _slotsSize; }
		set
		{
			_slotsSize = value;
			ignoreIfPeekedElementIsActive = (_slotsSize > 0);
		}
	}

	/// <summary>Gets and Sets occupiedSlotsCount property.</summary>
	public int occupiedSlotsCount
	{
		get { return _occupiedSlotsCount; }
		set
		{
			_occupiedSlotsCount = value;
			if(_occupiedSlotsCount > slotsSize) slotsSize = _occupiedSlotsCount;
			vacantSlotsCount = (slotsSize - occupiedSlotsCount);
		}
	}

	/// <summary>Gets and Sets vacantSlotsCount property.</summary>
	public int vacantSlotsCount
	{
		get { return _vacantSlotsCount; }
		set { _vacantSlotsCount = value; }
	}

	/// <summary>BaseObjectPoolData constructor.</summary>
	/// <param name="_poolObjectsQueue">T's Queue.</param>
	/// <param name="_group">Group where the queue Objects will belong to.</param>
	/// <param name="_ignoreIfPeekedElementIsActive">Ignore this Slot's retrieval request if the peeked element is active?.</param>
	/// <param name="_slotsSize">Slot's Limit, set '-1' by default [approving the limit policy by default].</param>
	public BaseObjectPoolData(Queue<T> _poolObjectsQueue, Transform _group = null, bool _ignoreIfPeekedElementIsActive = false, int _slotsSize = 0)
	{
		poolObjectsQueue = _poolObjectsQueue;
		group = _group;
		slotsSize = _slotsSize;
		ignoreIfPeekedElementIsActive = _ignoreIfPeekedElementIsActive;
	}
}
}