using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehavior : MonoBehaviour
{
    #region Fields
    private VisionComponent _visionComponent;
    private bool gameOverCalled = false;
    #endregion Fields

    // Start is called before the first frame update
    void Start()
    {
        _visionComponent = GetComponent<VisionComponent>();
        _visionComponent.PlayerSpottedEvent.AddListener(PlayerSpotted);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Delegate method called by the vision component. Notice how the vision component script has no idea this script exists yet they talk to eachother. Loose Coupling!
    private void PlayerSpotted()
    {
        if(gameOverCalled) return;
        //Add game over or whatever logic here.
        Debug.Log("Player Spotted");
        GameManager.Instance.TransitionTo("GameOver");
        gameOverCalled = true;
    }

}
