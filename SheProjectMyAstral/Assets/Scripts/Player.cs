using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // fields
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private GameObject projection;

    // internal fields (things like these should only be for completing stuff within player)
    private Vector2 movementDirection;
    private bool isProjecting = false; // should be replaced once there's a gamestate for it

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    /// <summary>
    /// Gets the direction of context to use in Move and other functions.
    /// </summary>
    /// <param name="context">Vector2 input from keyboard or gamepad.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    public void OnProject(InputAction.CallbackContext context)
    {
        // toggle projection
        if (isProjecting)
        {
            isProjecting = false;
            projection.SetActive(false);
        }
        else
        {
            isProjecting = true;
            projection.SetActive(true);
        }
    }

    /// <summary>
    /// Moves the player and projection around.
    /// </summary>
    private void Move()
    {
        // player movement
        if (!isProjecting)
        {
            // Move the player by its rigidbody from its previous position.
            rigidBody.MovePosition((Vector2)transform.position + ((movementDirection * playerSpeed) * Time.deltaTime));

            // keep the projection and player together when not projecting
            projection.transform.position = transform.position;
        }
        // projection movement
        else
        {
            // grab the rigidbody to make the code cleaner
            Rigidbody2D projectionBody = projection.GetComponent<Rigidbody2D>();

            // Move the projection by its rigidbody from its previous position.
            projectionBody.MovePosition((Vector2)projection.transform.position + ((movementDirection * playerSpeed) * Time.deltaTime));
        }
    }
}
