using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // fields
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] private Rigidbody2D rigidBody;

    // internal fields
    private Vector2 movementDirection;

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
        Debug.Log(context.ReadValue<Vector2>());
        movementDirection = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Moves the player around.
    /// </summary>
    private void Move()
    {
        // Move the rigidbody from its previous position.
        rigidBody.MovePosition((Vector2) transform.position + ((movementDirection * playerSpeed) * Time.deltaTime));
    }
}
