//Made by Ryan Cooper 2026

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class VisionComponent : MonoBehaviour
{
    //For the sake of the work today we should only have to do Up
    public enum SpriteDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum VisionState
    {
        //Nothing has been seen
        Default,
        //Player is in direct line of sight
        Chase,
        //Player has left line of sight
        Pursuit
    }
    #region Fields
    [SerializeField] private float visionRadius;
    [SerializeField, Range(1, 360)] private float visionAngle;
    [SerializeField] private SpriteDirection direction;
    private Transform viewingDirection;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask blockingLayer;

    [SerializeField] public bool PlayerSpotted { get; private set; }
    public VisionState currentVisionObjective { get; private set; }

    [SerializeField, Min(0.1f)] private float bufferTime;

    [SerializeField, Tooltip("No functionallity for this yet but this variable can make it so we pursuit one target first before going to another")] private bool stubborn;
    [SerializeField] private bool viewDebugLines = true;
    //No functionallity for this yet but in the future if we want to blind an AI we can use this
    private bool canSee = true;

    public UnityEvent PlayerSpottedEvent = new UnityEvent(); 

    #region Object Permanence
    [SerializeField, Range(0, 3)] private float pursuitTimer;
    [SerializeField] private float predictionUpdate;
    private float currentTimePursuiting = 0;
    public GameObject rememberedTarget { get; private set; }
    public Vector3 predictedLocation { get; private set; }


    #endregion Object Permanence

    #endregion

    #region Properties
    //I'll add setters later if needed
    public float FOV
    {
        get { return visionAngle; }
    }

    public float VisionRadius
    {
        get { return visionRadius; }
    }

    public LayerMask CollisionLayerMask
    {
        get { return playerLayer | blockingLayer; }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        currentVisionObjective = VisionState.Default;
        StartCoroutine(FOVTimeCheck());
    }

    private void FixedUpdate()
    {
        if (currentVisionObjective == VisionState.Pursuit)
        {
            CanPursuitTarget();
        }
    }

    //Run forever
    IEnumerator FOVTimeCheck()
    {
        WaitForSeconds bufferSeconds = new WaitForSeconds(bufferTime);
        while (canSee)
        {
            yield return bufferSeconds;
            GetFOV();

        }
    }


    /// <summary>
    /// If we are within range, within angle, and nothing is obstructing then the player is in sight
    /// </summary>
    private void GetFOV()
    {
        
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, visionRadius, playerLayer);
        //Don't bother anymore math unless we are within range
        if (rangeCheck.Length > 0)
        {
            Transform targetToAggress = rangeCheck[0].transform.root;
            Vector2 targetDirection = (targetToAggress.position - transform.position).normalized;
            //Don't bother raycasting unless we are within vision cone. Since we are viewing from the center we want to see whats on both sides of the vision cone so we cut the intial angle in half
            if (Vector2.Angle(GetViewingDirection(), targetDirection) < visionAngle / 2.0f)
            {
                float targetDistance = Vector2.Distance(transform.position, targetToAggress.position);
                if (!Physics2D.Raycast(transform.position, targetDirection, targetDistance, blockingLayer))
                {
                    //If we get here the enemy has spotted something, lets get say we saw them, remember what they are and reset the pursuit timer
                    PlayerSpotted = true;
                    //if (currentVisionObjective == VisionState.Pursuit)
                    //{
                    //    ReleasePursuit();
                    //}
                    //rememberedTarget = targetToAggress.gameObject;
                    //currentTimePursuiting = pursuitTimer;
                    //currentVisionObjective = VisionState.Chase;
                    PlayerSpottedEvent.Invoke();
                    return;
                }

                //PlayerOutOfSight(PlayerSpotted);
                //return;
            }
            //PlayerOutOfSight(PlayerSpotted);
            //return;
        }
        //PlayerOutOfSight(PlayerSpotted);
        //return;
    }

    private void PlayerOutOfSight(bool pursuit)
    {
        PlayerSpotted = false;

        //if (pursuit)
        //{
        //    //Debug.Log("Finding Target");
        //    //currentTimePursuiting = pursuitTimer;
        //    predictedLocation = PredictTarget();

        //}
    }

    private void CanPursuitTarget()
    {

        currentTimePursuiting -= Time.fixedDeltaTime;
        //Debug.Log(currentTimePursuiting);
        if (currentTimePursuiting <= 0)
        {
            ReleasePursuit();
        }
    }
    //Depreciated
    private void ReleasePursuit()
    {
        //Debug.Log(rememberedTarget);
        rememberedTarget = null;
        predictedLocation = Vector3.zero;
        currentVisionObjective = VisionState.Default;
    }

    private Vector3 PredictTarget()
    {

        if (rememberedTarget != null)
        {
            currentVisionObjective = VisionState.Pursuit;
            Vector2 targetVelocity = rememberedTarget.GetComponent<Rigidbody2D>().velocity;
            //targetVelocity *= Mathf.Max(1.0f, pursuitTimer);
            //I don't like casting like this but mayyyyyyyyyyybe theres a better way
            return rememberedTarget.transform.position + (Vector3)targetVelocity;
        }
        currentVisionObjective = VisionState.Default;

        return Vector3.zero;
    }



    public bool InPursuit()
    {
        return currentVisionObjective != VisionState.Default;
    }

    private Vector3 GetViewingDirection()
    {
        switch (direction)
        {
            case SpriteDirection.Up:
                return transform.up;
            case SpriteDirection.Down:
                return -transform.up;
            case SpriteDirection.Left:
                return -transform.right;
            case SpriteDirection.Right:
                return transform.right;
        }
        return transform.up;
    }

    public Vector3 PositionToGoTo()
    {

        switch (currentVisionObjective)
        {
            case VisionState.Chase:
                return rememberedTarget.transform.position;
            case VisionState.Pursuit:
                return predictedLocation;
            default:
                return Vector3.zero;
        }
    }



    #region Debugging Gizmos
    private void OnDrawGizmos()
    {
        if (viewDebugLines)
        {
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.color = Color.white;

#if UNITY_EDITOR
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, visionRadius);
#endif

            Vector3 angleEndPoint1 = GetDirectionFromAngle(transform.eulerAngles.z, -visionAngle / 2);
            Vector3 angleEndPoint2 = GetDirectionFromAngle(transform.eulerAngles.z, visionAngle / 2);


            switch (currentVisionObjective)
            {
                case VisionState.Chase:
                    Gizmos.color = Color.green;
                    break;
                case VisionState.Pursuit:
                    Gizmos.color = Color.blue;
                    break;
                default:
                    Gizmos.color = Color.red;
                    break;
            }

            Gizmos.DrawLine(transform.position, transform.position + angleEndPoint1 * visionRadius);
            Gizmos.DrawLine(transform.position, transform.position + angleEndPoint2 * visionRadius);
        }

    }

    private Vector2 GetDirectionFromAngle(float transformAngle, float angleBounds)
    {
        angleBounds -= transformAngle + GetStartingAngleInEuler();
        return new Vector2(Mathf.Sin(angleBounds * Mathf.Deg2Rad), Mathf.Cos(angleBounds * Mathf.Deg2Rad));
    }

    public float GetStartingAngleInEuler()
    {
        switch (direction)
        {
            case SpriteDirection.Up:
                return 0;
            case SpriteDirection.Down:
                return 180f;
            case SpriteDirection.Left:
                return -270f;
            case SpriteDirection.Right:
                return -90f;
        }
        return 0f;
    }

    #endregion Degbugging Gizmos
}

