using System.Collections.Generic;

namespace VoidlessUtilities
{
public interface IAgentLeaf<T, R> : IAgentComponent<T, R> where T : IComponentAgent<T, R>
{
	
}
}