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
    #region States:
    //State Fields:
    [SerializeField] private MainMenu MENU_STATE;
    [SerializeField] private MainMenu OTHER_MENU_TEST_STATE;
    [SerializeField] private NextLevelState NEXT_LEVEL_STATE;
    [SerializeField] private GameOverState GAME_OVER_STATE;
    

    /// <summary>
    /// Sets up the dictionary to be ready to find relevant states. 
    /// Above states **MUST BE ADDED HERE** in order to be searched for and transitioned to later!
    /// </summary>
    private void SetupDictionary()
    {
        stateDictionary.Add("MainMenu", MENU_STATE);
        stateDictionary.Add("OtherMenu", OTHER_MENU_TEST_STATE);
        stateDictionary.Add("NextLevel", NEXT_LEVEL_STATE);
        stateDictionary.Add("GameOver", GAME_OVER_STATE);
    }
    #endregion

    [SerializeField] private GameState currentState;                        //State of the game we are currently in.
    [SerializeField] public Dictionary<string, GameState> stateDictionary;  //All possible states are stored in here after Awake().

    public GameState CurrentState { get { return currentState; } }          //Returns the currentState
    

    /// <summary>
    /// Sets up the state dictionary to be usable, if current state is not set beforehand we give it a default state of "main".
    /// </summary>
    //private void Awake()
    //{

    //}

    private void Start()
    {
        print("I am awake");
        if (stateDictionary == null)
        {
            stateDictionary = new Dictionary<string, GameState>();
            SetupDictionary();
        }

        if (currentState == null && stateDictionary != null)
        {
            currentState = stateDictionary["MainMenu"];
        }
        else if (currentState == null)
        {
            Debug.LogError("No game states detected at all! This is really bad!");
        }




        //TESTING BELOW:

        if (stateDictionary["MainMenu"] != null)
        {
            Debug.Log("MainMenu detected!");
        }
        if (stateDictionary["OtherMenu"] != null)
        {
            Debug.Log("OtherMenu detected!");
        }
        //if (stateDictionary["GameOver"] != null)
        //{
        //    Debug.Log("Game Over detected!");
        //}

        Debug.Log("Current state is: " + currentState.Name);

        Debug.Log("Transition from current to 'OtherMenu'.");
        TransitionTo("OtherMenu");

        Debug.Log("Transition from current to 'OtherMenu' again...");
        TransitionTo("OtherMenu");

        Debug.Log("Transition from current to 'MainMenu'.");
        TransitionTo("MainMenu");

        //Debug.Log("Transition from current to 'MadeUpFakeState'");
        //TransitionTo("MadeUpFakeState");
    }

    /// <summary>
    /// Calls necessary closing and opening methods for switching between states, 
    /// string parameter represents the name assigned to the state in the above dictionary.
    /// Most likely, is called by the states themselves but could be called through other things as well.
    /// </summary>
    /// <param name="StateName"></param>
    public void TransitionTo(string StateName)
    {
        GameState nextState;
        if ( (nextState = stateDictionary[StateName]) == null)
        {
            Debug.LogError($"{StateName} is not a valid state, Transition failed!");
            return;
        }
        else if (nextState == currentState)
        {
            Debug.LogError($"{StateName} is the same as the current state, Transition failed!");
            return;
        }

        print($"{StateName} state entered");

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