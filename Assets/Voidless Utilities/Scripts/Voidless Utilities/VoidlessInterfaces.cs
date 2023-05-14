using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Type;

namespace VoidlessUtilities
{
public static class VoidlessInterfaces
{
	/// <summary>Changes IFiniteStateMachine implementer's state. Following FSM's procedures</summary>
	/// <param name="_fsm">IFiniteStateMacine implementer.</param>
	/// <param name="_state">New State to enter.</param>
	public static void ChangeState<T>(this IFiniteStateMachine<T> _fsm, T _state)
	{
		_fsm.previousState = _fsm.state;
		_fsm.ExitState(_fsm.state);
		_fsm.EnterState(_fsm.state = _state);
	}

	/// <summary>Returns to previous State, if there is.</summary>
	/// <param name="_fsm">IFiniteStateMachine implementer.</param>
	public static void ReturnToPreviousState<T>(this IFiniteStateMachine<T> _fsm)
	{
		T newState = _fsm.previousState;
		
		_fsm.previousState = _fsm.state;
		_fsm.ExitState(_fsm.state);
		_fsm.EnterState(_fsm.state = newState);
	}

	/// <summary>Utility function to subscribe object implementing IInputControllerHandler to InputController's events.</summary>
	/// <param name="_controllerHandler">IInputControllerHandler object to subscribe to events.</param>
	public static void SubscribeToInputControllerEvents(this IInputControllerHandler _controllerHandler)
	{
		InputController.onInputReceived += _controllerHandler.OnInputReceived;
		InputController.onRightAxesChange += _controllerHandler.OnRightAxesChange;
		InputController.onLeftAxesChange += _controllerHandler.OnLeftAxesChange;
		InputController.onRightTriggerAxisChange += _controllerHandler.OnRightTriggerAxisChange;
		InputController.onLeftTriggerAxisChange += _controllerHandler.OnLeftTriggerAxisChange;
		InputController.onDPadAxesChanges += _controllerHandler.OnDPadAxesChanges;
	}

	/// <summary>Utility function to unsubscribe object implementing IInputControllerHandler to InputController's events.</summary>
	/// <param name="_controllerHandler">IInputControllerHandler object to unsubscribe to events.</param>
	public static void UnsubscribeToInputControllerEvents(this IInputControllerHandler _controllerHandler)
	{
		InputController.onInputReceived -= _controllerHandler.OnInputReceived;
		InputController.onRightAxesChange -= _controllerHandler.OnRightAxesChange;
		InputController.onLeftAxesChange -= _controllerHandler.OnLeftAxesChange;
		InputController.onRightTriggerAxisChange -= _controllerHandler.OnRightTriggerAxisChange;
		InputController.onLeftTriggerAxisChange -= _controllerHandler.OnLeftTriggerAxisChange;
		InputController.onDPadAxesChanges -= _controllerHandler.OnDPadAxesChanges;
	}

	/// <summary>Subscribes to FOVSight's events.</summary>
	/// <param name="_FOVListener">IFOVListener implementer to subscribe.</param>
	/// <param name="_FOVSight">Sight to subscribe to.</param>
	public static void SubscribeToFOV(this IFOVListener _FOVListener, FOVSight _FOVSight)
	{
		_FOVSight.onTriggeredWithGameObject += _FOVListener.OnGameObjectCollision;
	}

	/// <summary>Unsubscribes to FOVSight's events.</summary>
	/// <param name="_FOVListener">IFOVListener implementer to unsubscribe.</param>
	/// <param name="_FOVSight">Sight to unsubscribe to.</param>
	public static void UnsubscribeToFOV(this IFOVListener _FOVListener, FOVSight _FOVSight)
	{
		_FOVSight.onTriggeredWithGameObject += _FOVListener.OnGameObjectCollision;
	}

	/// <summary>Updates IRayConvertible implementer's data from Ray.</summary>
	/// <param name="_ray">Ray to pass data to IRayConvertible implementer.</param>
	/// <param name="_direction">IRayConvertible implementer to pass data from Ray.</param>
	public static void UpdateToRayConvertible<T>(this Ray _ray, ref T _rayImplementer) where T : IRayConvertible 
	{
		_rayImplementer.origin = _ray.origin;
		_rayImplementer.direction = _ray.direction;
	}

	/// <summary>Updates IRayConvertible2D implementer's data from Ray2D.</summary>
	/// <param name="_ray">Ray2D to pass data to IRayConvertible2D implementer.</param>
	/// <param name="_direction">IRayConvertible2D implementer to pass data from Ray.</param>
	public static void UpdateToRayConvertible2D<T>(this Ray2D _ray, ref T _rayImplementer) where T : IRayConvertible2D 
	{
		_rayImplementer.origin = _ray.origin;
		_rayImplementer.direction = _ray.direction;
	}

	/// <summary>Creates Ray from IRayConvertible implementer.</summary>
	/// <param name="_rayImplementer">IRayConvertible implementer.</param>
	/// <returns>Ray from interface's data.</returns>
	public static Ray ToRay<T>(this T _rayImplementer) where T : IRayConvertible
	{
		return new Ray(_rayImplementer.origin, _rayImplementer.direction);
	}

	/// <summary>Creates Ray from IRayConvertible2D implementer.</summary>
	/// <param name="_rayImplementer">IRayConvertible2D implementer.</param>
	/// <returns>Ray from interface's data.</returns>
	public static Ray2D ToRay2D<T>(this T _rayImplementer) where T : IRayConvertible2D
	{
		return new Ray2D(_rayImplementer.origin, _rayImplementer.direction);
	}

	/// <summary>Gets underlying type T from IGeneric implementer.</summary>
	/// <param name="_generic">IGeneric implementer.</param>
	/// <returns>Type of IGeneric implementer.</returns>
	public static Type GetGenericType<T>(this IGeneric<T> _generic)
	{
		return typeof(T);
	}

	public static B UpCast<T, B>(this IUpCaster<T> _upCaster, B _childCast) where B : class, T
	{
		if(_upCaster.BaseCast is B)
		{
			return _childCast = _upCaster.BaseCast as B;
		}
		else
		{
			Debug.LogError("[VoidlessInterfaces] Provided BaseCast type is not of type " + typeof(B).Name + ".");
			return null;
		}
	}

#region IPoolObjectExtensions:
	/// <summary>Evaluates if Slots Collection is allowed to add a new item.</summary>
	/// <param name="_slotCollection">ISlotCollection's implementer.</param>
	/// <returns>True if there is no limit set, otherwise true if the occupied slots' count is below the limit.</returns>
	public static bool RequestApproved<T>(this T _slotCollection) where T : ISlotCollection
	{
		return (_slotCollection.ignoreIfPeekedElementIsActive ? (_slotCollection.occupiedSlotsCount < _slotCollection.slotsSize) : true);
	}

	/// <summary>Gets a string representing the Pool Object.</summary>
	/// <param name="_poolObject">Pool Object to debug.</param>
	/// <returns>String representing the extended Pool Object.</returns>
	public static string ToString<T>(this T _poolObject) where T : IPoolObject
	{
		StringBuilder builder = new StringBuilder();

		builder.Append("Pool Object: ");
		builder.Append("\n");
		builder.Append("Pool Dictionary ID: ");
		builder.Append(_poolObject.poolDictionaryID.ToString());
		builder.Append("\n");
		builder.Append("Don't Destroy on Load: ");
		builder.Append(_poolObject.dontDestroyOnLoad.ToString());
		builder.Append("\n");
		builder.Append("Active: ");
		builder.Append(_poolObject.active.ToString());

		return builder.ToString();
	}
#endregion

	/// <summary>Requests Component from IComponentEntity's implementer.</summary>
	/// <param name="_componentEntity">IComponentEntity's implementer.</param>
	/// <returns>Entity's component, if it has it attached.</returns>
	public static C RequestComponent<T, C>(this T _componentEntity) where T : MonoBehaviour, IComponentEntity<C> where C : Component
	{
		if(_componentEntity.component == null) _componentEntity.component = _componentEntity.GetComponent<C>();
		return _componentEntity.component;
	}

#region IComponentsExtensions:
	/// <summary>Adds set of children to Composite interface implementer.</summary>
	/// <param name="_composite">Composite to add the children to.</param>
	/// <param name="_children">Children to add to Composite.</param>
	public static void AddChildren<T>(this IComposite<T> _composite, params IComponent<T>[] _children)
	{
		if(_composite.children == null) _composite.children = new List<IComponent<T>>(_children.Length);
		foreach(IComponent<T> child in _children)
		{
			_composite.children.Add(child);
		}
	}

	/// <summary>Adds child to Decorator interface implementer.</summary>
	/// <param name="_decorator">Decorator to add the child to.</param>
	/// <param name="_child">Child to add to Decorator.</param>
	public static void AddChild<T>(this IDecorator<T> _decorator, IComponent<T> _child)
	{
		_decorator.child = _child;
	}
#endregion

#region IAgentComponentExtensions:
	/// <summary>Adds set of children to Composite interface implementer.</summary>
	/// <param name="_composite">Composite to add the children to.</param>
	/// <param name="_children">Children to add to Composite.</param>
	public static void AddChildren<T, R>(this IAgentComposite<T, R> _composite, params IAgentComponent<T, R>[] _children) where T : IComponentAgent<T, R>
	{
		if(_composite.children == null) _composite.children = new List<IAgentComponent<T, R>>(_children.Length);
		foreach(IAgentComponent<T, R> child in _children)
		{
			_composite.children.Add(child);
		}
	}
#endregion

#region IRangeExtensions:
	/// <summary>Gets highest value from range.</summary>
	/// <param name="_range">Range to get highest value from.</param>
	/// <returns>Highest value from Range.</returns>
	public static T GetMaximumValue<T>(this IRange<T> _range) where T : IComparable<T>
	{
		switch(_range.min.CompareTo(_range.max))
		{
			case -1:
			return _range.max;

			case 0:
			case 1:
			return _range.min;
		}

		return _range.min;
	}

	/// <summary>Gets lowest value from range.</summary>
	/// <param name="_range">Range to get lowest value from.</param>
	/// <returns>Lowest value from Range.</returns>
	public static T GetMinimumValue<T>(this IRange<T> _range) where T : IComparable<T>
	{
		switch(_range.min.CompareTo(_range.max))
		{
			case -1:
			case 0:
			return _range.min;

			case 1:
			return _range.max;
		}

		return _range.min;
	}
#endregion

	/// <summary>Debugs IGUIDebuggable's object on OnGUI's body.</summary>
	/// <param name="_debuggable">IGUIDebuggable's implementer.</param>
	public static void DebugOnGUI<T>(this T _debuggable) where T : IGUIDebuggable
	{
#if UNITY_EDITOR
		if(_debuggable.debug) GUI.Box(_debuggable.GUIRect, _debuggable.ToString());
#endif
	}
}
}