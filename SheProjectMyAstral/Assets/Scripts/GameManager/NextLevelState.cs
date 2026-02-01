using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NextLevelState : GameState
{
    public override void FixedUpdateGameState()
    {
        
    }

    public override void OnEnter()
    {
        EnteredEvent.Invoke();
    }

    public override void OnExit()
    {
        ExitEvent.Invoke();
    }

    public override void UpdateGameState()
    {
        
    }

}
