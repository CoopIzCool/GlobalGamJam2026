using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainMenu : GameState
{
    public override void OnEnter()
    {
        Debug.Log($"Entered {name}.");
    }

    public override void OnExit()
    {
    }

    public override void FixedUpdateGameState()
    {
    }

    public override void UpdateGameState()
    {
    }
}
