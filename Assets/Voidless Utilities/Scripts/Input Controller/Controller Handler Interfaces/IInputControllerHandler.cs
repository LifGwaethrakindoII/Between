using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IInputControllerHandler : IJoystickAxesHandler, ITriggerAxesHandler, IInputReceiveHandler, IDPadAxesHandler
{
	
}
}