using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventHandler<EState> : Singleton<EventHandler<EState>> where EState : Enum
{
    protected UnityEvent[] events = new UnityEvent[Enum.GetNames(typeof(EState)).Length];

    /// <summary>
    /// Call this method to bind a method to an event.
    /// EX: YourEventHandler.Instance.SetUnityEventListener({ENUMERATOR}ENUM.GongNG,{METHOD DELEGATE} ActivateGongSFX, );
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="methodToAdd"></param>
    public void SetUnityEventListener(EState eventType, UnityAction methodToAdd)
    {
        int eventArrayIndex = GenericEnumToInt(eventType);
        if (events[eventArrayIndex] == null)
        {
            UnityEvent newEvent = new UnityEvent();
            events[eventArrayIndex] = newEvent;
        }
        events[eventArrayIndex].AddListener(methodToAdd);
    }

    /// <summary>
    /// Set to remove method from Event. 
    /// EX: YourEventHandler.Instance.RemoveUnityEventListener({ENUMERATOR}ENUM.GongNG,{METHOD DELEGATE} ActivateGongSFX, );
    /// </summary>
    /// <param name="eventType">Enumerator of choice</param>
    /// <param name="methodToAdd">MethodDelegate</param>
    public void RemoveUnityEventListener(EState eventType, UnityAction methodToAdd)
    {
        int eventArrayIndex = GenericEnumToInt(eventType);
        if (events[eventArrayIndex] == null)
        {
            //If this calls we realllllllllllllly fucked up
            Debug.LogError("UH OH DINGAS: Event Not Set");
        }
        else
        {
            events[eventArrayIndex].RemoveListener(methodToAdd);
        }
    }

    //Call to invoke all methods tied to this enum
    public void InvokeUnityEvent(EState eventType)
    {
        int enumIndex = GenericEnumToInt(eventType);
        if (events[enumIndex] != null)
        {
            events[enumIndex].Invoke();
        }
    }

    //Convert enum to int. The method works trust IDK how it does either. I got it from StackOverflow
    private int GenericEnumToInt(EState enumState)
    {
        Enum test = Enum.Parse(typeof(EState), enumState.ToString()) as Enum;
        return Convert.ToInt32(test); // x is the integer value of enum
    }
}
