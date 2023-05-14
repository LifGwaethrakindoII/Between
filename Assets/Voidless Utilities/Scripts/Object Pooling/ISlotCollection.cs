using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface ISlotCollection
{
	bool ignoreIfPeekedElementIsActive { get; set; } 	/// <summary>Ignore this Slot's retrieval request if the peeked element is active?.</summary>
	int slotsSize { get; set; } 						/// <summary>Slots' Size.</summary>
	int occupiedSlotsCount { get; set; } 				/// <summary>Occupied Slots' count.</summary>
	int vacantSlotsCount { get; set; } 					/// <summary>Vacant Slots' count.</summary>	
}
}