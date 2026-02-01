using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // fields
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] private float projectionDistance = 5;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private GameObject projection;
    [SerializeField] private GameObject projectionRadius;
    [SerializeField] private float projectionTimeLength;
    [SerializeField] private Canvas HUD;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer projectionSprite;
    [SerializeField] private SpriteRenderer inverterSprite;
    [SerializeField] private GameObject spriteObj;

    // internal fields (things like these should only be for completing stuff within player)
    private Vector2 movementDirection;
    private float projectionTimer;
    private bool isProjecting = false; // should be replaced once there's a gamestate for it

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // FixedUpdate is where player logic is run
    private void FixedUpdate()
    {
        Move();
        CheckProjection();
        UpdateExternal();
    }
    /// <summary>
    /// Gets the direction of context to use in Move and other functions.
    /// </summary>
    /// <param name="context">Vector2 input from keyboard or gamepad.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            spriteObj.GetComponent<Animator>().SetBool("isMoving", false);
        }
        else
        {
            spriteObj.GetComponent<Animator>().SetBool("isMoving", true);
        }
    }
    /// <summary>
    /// Gets the key to toggle projection.
    /// </summary>
    /// <param name="context">button input</param>
    public void OnProject(InputAction.CallbackContext context)
    {
        SetProjection();
    }

    /// <summary>
    /// Moves the player and projection around.
    /// </summary>
    private void UpdateExternal()
    {
        if (HUD != null)
        {
            HUD.GetComponent<HUD>().SetProjectionTimer(projectionTimer, projectionTimeLength);
        }
        if (projectionSprite != null)
        {
            projectionSprite.sprite = playerSprite.sprite;
            inverterSprite.sprite = playerSprite.sprite;
        }
    }
    private void Move()
    {
        // flip the sprite if the player is going left
        if (movementDirection.x != 0)
        spriteObj.GetComponent<SpriteRenderer>().flipX = movementDirection.x < 0;

        projectionSprite.flipX = spriteObj.GetComponent<SpriteRenderer>().flipX;
        inverterSprite.flipX = spriteObj.GetComponent<SpriteRenderer>().flipX;

        // player movement
        if (!isProjecting)
        {
            // Move the player by its rigidbody from its previous position.
            rigidBody.MovePosition((Vector2)transform.position + ((movementDirection * playerSpeed) * Time.fixedDeltaTime));

            // keep the projection and player together when not projecting
            projection.transform.position = transform.position;
        }
        // projection movement
        else
        {
            // rename some variables to make the code cleaner
            Rigidbody2D projectionBody = projection.GetComponent<Rigidbody2D>();
            Vector2 playerPosition = (Vector2)transform.position;
            Vector2 projectionPosition = (Vector2)projection.transform.position;

            // Move the projection by its rigidbody from its previous position, clamped by projectionDistance
            projectionBody.MovePosition(playerPosition +    
                Vector2.ClampMagnitude((projectionPosition - playerPosition) + ((movementDirection * playerSpeed) * Time.fixedDeltaTime), 
                projectionDistance));

            // move the projection radius and resize it accordingly
            projectionRadius.transform.position = transform.position;
            projectionRadius.transform.localScale = new Vector3(projectionDistance * 2, projectionDistance * 2, 1);
        }
    }
    /// <summary>
    /// Manage anything that solely the projection relies on per-frame.
    /// </summary>
    private void CheckProjection()
    {
        // only do something if projecting
        if (isProjecting)
        {
            projectionTimer += Time.fixedDeltaTime;
            // Debug.Log(projectionTimer);
            // forcefully stop projecting after projectionTimeLength seconds
            if (projectionTimer >= projectionTimeLength)
            {
                SetProjection();
            }
        }
    }
    /// <summary>
    /// Sets the projection, and changes anything that goes with it.
    /// </summary>
    private void SetProjection()
    {
        // toggle projection
        if (isProjecting)
        {
            isProjecting = false;
            projection.SetActive(false);
            projectionRadius.SetActive(false);
            projectionTimer = 0;
        }
        else
        {
            isProjecting = true;
            projectionRadius.SetActive(true);
            projection.SetActive(true);
        }
    }
    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, projectionDistance);
    }
    #endregion
}
