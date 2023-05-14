using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class IPoolObjectPoolData : BaseObjectPoolData<IPoolObject>
{
	/// <summary>IPoolObjectPoolData constructor.</summary>
	/// <param name="_poolObjectsQueue">IPoolObject's Queue.</param>
	/// <param name="_group">Group where the queue Objects will belong to.</param>
	/// <param name="_ignoreIfPeekedElementIsActive">Ignore this Slot's retrieval request if the peeked element is active?.</param>
	/// <param name="_slotLimit">Slot's Limit, set '-1' by default [approving the limit policy by default].</param>
	public IPoolObjectPoolData(Queue<IPoolObject> _poolObjects, Transform _group, bool _ignoreIfPeekedElementIsActive = false, int _slotLimit = 0) : base(_poolObjects, _group, _ignoreIfPeekedElementIsActive, _slotLimit)
	{ }
}
}