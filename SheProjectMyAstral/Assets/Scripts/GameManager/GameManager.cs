using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager: Designed to know (or figure out) whatever state the entire game is in. 
/// It leverages things such as the BoilerPlate to control scenes, but otherwise sorts through levels.
/// </summary>
/// 
public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameState currentState;                        //State of the game we are currently in.
    [SerializeField] public Dictionary<string, GameState> stateDictionary;  //All possible states are stored in here.

    /// <summary>
    /// Sets up the state dictionary to be usable, if current state is not set beforehand we give it a default state of "main".
    /// </summary>
    private void Awake()
    {
        if (stateDictionary != null)
        {
            stateDictionary = new Dictionary<string, GameState>();
        }

        if (currentState == null && stateDictionary != null)
        {
            currentState = stateDictionary["Main"];
        }
        else if (currentState == null)
        {
            Debug.LogError("No game states detected at all! This is really bad!");
        }
    }

    /// <summary>
    /// Calls necessary closing and opening methods for switching between states, 
    /// string parameter represents the name assigned to the state in the above dictionary.
    /// </summary>
    /// <param name="StateName"></param>
    public void TransitionTo(string StateName)
    {
        GameState nextState = stateDictionary[StateName];

        currentState.OnExit();
        currentState = nextState;
        currentState.OnEnter();
    }

    /// <summary>
    /// Updates the current state.
    /// </summary>
    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateGameState();
        }
    }

    /// <summary>
    /// Updates the current state's physics.
    /// </summary>
    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdateGameState();
        }
    }
}