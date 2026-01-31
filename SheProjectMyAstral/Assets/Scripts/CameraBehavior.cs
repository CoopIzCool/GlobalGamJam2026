using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    #region Fields
    public Transform player;
    public Vector3 offset;
    public float followSpeed;
    #endregion
    #region Methods


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    /// <summary>
    /// Called once per frame; updates the Camera object.
    /// </summary>
    void Update()
    {
        // Move the camera to the Player's position
        transform.position = Vector3.Lerp(transform.position, player.position + offset, followSpeed);
    }
    #endregion
}
