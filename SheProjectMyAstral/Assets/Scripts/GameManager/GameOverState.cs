using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameOverState : GameState
{
    public override void FixedUpdateGameState()
    {

    }

    public override void OnEnter()
    {
        //Put up Game Over Screen
        Debug.Log("YOU LOSE");
    }

    public override void OnExit()
    {

    }

    public override void UpdateGameState()
    {

    }
}
