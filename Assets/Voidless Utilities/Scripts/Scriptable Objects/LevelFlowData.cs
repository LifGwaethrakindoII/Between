using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities.LevelFlowControl
{
public class LevelFlowData : BaseNodeData<ILevelFlowNode>
{
	public override ILevelFlowNode GetNodeFlow()
	{
		return default(ILevelFlowNode);
	} 
}
}