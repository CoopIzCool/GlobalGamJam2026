using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameOverState : GameState
{
    //private GameObject GameOverCanvas;
    public override void FixedUpdateGameState()
    {

    }

    public override void OnEnter()
    {
        //Put up Game Over Screen
        EnteredEvent.Invoke();
        Debug.Log("YOU LOSE");
        //GameOverCanvas.SetActive(false);
    }

    public override void OnExit()
    {
        ExitEvent.Invoke();
       //GameOverCanvas.SetActive(false);
    }

    public override void UpdateGameState()
    {

    }
}
