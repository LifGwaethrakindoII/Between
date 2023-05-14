using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities.LevelFlowControl
{
[Serializable]
public class LevelFlowNodeData : BaseNodeData<ILevelFlowNode> 
{
	public override ILevelFlowNode GetNodeFlow()
	{
		return default(ILevelFlowNode);
	}
}
}