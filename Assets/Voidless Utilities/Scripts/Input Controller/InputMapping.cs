using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[Serializable]
public class InputMapping
{
	[SerializeField] private PCControllerSetup _PCControllerSetup; 			/// <summary>PC's Controller Setup.</summary>
	[SerializeField] private XBoxControllerSetup _XBoxControllerSetup; 		/// <summary>XBox's Controller Setup.</summary>
	[SerializeField] private N3DSControllerSetup _N3DSControllerSetup; 		/// <summary>N3DS's Controller Setup.</summary>

	/// <summary>Gets and Sets PCControllerSetup property.</summary>
	public PCControllerSetup PCControllerSetup
	{
		get { return _PCControllerSetup; }
		set { _PCControllerSetup = value; }
	}

	/// <summary>Gets and Sets XBoxControllerSetup property.</summary>
	public XBoxControllerSetup XBoxControllerSetup
	{
		get { return _XBoxControllerSetup; }
		set { _XBoxControllerSetup = value; }
	}

	/// <summary>Gets and Sets N3DSControllerSetup property.</summary>
	public N3DSControllerSetup N3DSControllerSetup
	{
		get { return _N3DSControllerSetup; }
		set { _N3DSControllerSetup = value; }
	}

	/// <summary>InputMapping's Constructor.</summary>
	public InputMapping()
	{
		PCControllerSetup = new PCControllerSetup();
		XBoxControllerSetup = new XBoxControllerSetup();
		N3DSControllerSetup = new N3DSControllerSetup();
	}
}
}