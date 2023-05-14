using System.Collections.Generic;

namespace VoidlessUtilities
{
public interface IComposite<T> : IComponent<T>
{
	List<IComponent<T>> children { get; set; } 	/// <summary>Composite's Children.</summary>
}
}