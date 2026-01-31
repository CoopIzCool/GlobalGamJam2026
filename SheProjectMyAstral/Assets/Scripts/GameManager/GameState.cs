using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GameState
{
    [SerializeField] protected string Name = "DefaultStateName";
    /// <summary>
    /// Called on a state being entered, perform any startup actions here.
    /// </summary>
    public abstract void OnEnter();
    /// <summary>
    /// Called on a state being exited, perform any cleanup here.
    /// </summary>
    public abstract void OnExit();
    /// <summary>
    /// Updates this state, called every update frame.
    /// </summary>
    public abstract void UpdateGameState();
    /// <summary>
    /// Updates this state at a fixed interval, called every physics update frame.
    /// </summary>
    public abstract void FixedUpdateGameState();
}
