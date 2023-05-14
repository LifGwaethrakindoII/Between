using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class GameObjectPoolData : BaseObjectPoolData<GameObject>
{
	/// <summary>GameObjectPoolData constructor.</summary>
	/// <param name="_poolObjectsQueue">GameObject's Queue.</param>
	/// <param name="_group">Group where the queue Objects will belong to.</param>
	/// <param name="_shouldBeOnListSlots">Should this Queue have objects belinging to the List's Slots?.</param>
	/// <param name="_slotLimit">Slot's Limit, set '-1' by default [approving the limit policy by default].</param>
	public GameObjectPoolData(Queue<GameObject> _poolObjects, Transform _group, bool _shouldBeOnListSlots = false, int _slotLimit = -1) : base(_poolObjects, _group, _shouldBeOnListSlots, _slotLimit)
	{ }
}
}