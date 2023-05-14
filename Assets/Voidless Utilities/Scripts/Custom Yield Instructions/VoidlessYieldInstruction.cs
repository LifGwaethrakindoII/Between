using System;
using System.Collections;

namespace VoidlessUtilities
{
public abstract class VoidlessYieldInstruction : IEnumerator
{
	private IEnumerator _enumerator; 	/// <summary>Yield Instruction's IEnumerator.</summary>

	/// <summary>Gets and Sets enumerator property.</summary>
	public IEnumerator enumerator
	{
		get { return _enumerator; }
		protected set { _enumerator = value; }
	}

	/// <summary>Gets Current property.</summary>
	public virtual Object Current { get { return  null; } }

	/// <summary>VoidlessYieldInstruction's constructor.</summary>
	public VoidlessYieldInstruction()
	{
		enumerator = Operation();
	}

#region IEnumeratorMethods:
	/// <summary>Advances the enumerator to the next element of the collection.</summary>
	public virtual bool MoveNext()
	{
		return enumerator.MoveNext();
	}

	/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
	public virtual void Reset()
	{
		enumerator = Operation();
	}
#endregion

	/// <summary>Yield Instruction's Operation.</summary>
	protected abstract IEnumerator Operation();
}
}