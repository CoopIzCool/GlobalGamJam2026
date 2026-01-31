using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI projectionTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// sets the value of the projection timer on the HUD.
    /// </summary>
    /// <param name="projectionTimer">the projection timer from player.</param>
    public void SetProjectionTimer(float timer, float timerLength)
    {
        float timeleft = timerLength - timer;
        projectionTimer.text = timeleft.ToString("F");
    }
}
