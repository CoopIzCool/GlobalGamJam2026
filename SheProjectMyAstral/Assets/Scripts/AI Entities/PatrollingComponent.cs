using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    #region Fields
    [Separator("General")]
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _acceptanceRadius;
    [Separator("Rotation")]
    [SerializeField] private bool _orientToFacePoint;
    [ConditionalField(nameof(_orientToFacePoint)), SerializeField] private float _rotationSpeed;
    private int _patrolIndex = 0;
    private bool _pausedMovement = false;
    #endregion Fields

    #region Properties
    public bool PausedMovement
    {
        set { _pausedMovement = value; }
    }
    #endregion Properties

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PatrolToPoint();
    }

    private void PatrolToPoint()
    {
        Vector3 relativePos = _patrolPoints[_patrolIndex].position - transform.position;

        if (_orientToFacePoint)
        {
            //Quaternion startRotation = transform.rotation;
            //Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            //float t = Time.fixedDeltaTime * _rotationSpeed; // Interpolation factor (0 to 1)
            //transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            float intendedZRotation = Mathf.Atan2(-relativePos.x, relativePos.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, intendedZRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed);
        }

        if (!_pausedMovement)
        {
            transform.position += relativePos.normalized * _speed * Time.fixedDeltaTime;
        }

        if (DistanceCheck())
        {
            _patrolIndex = (_patrolIndex + 1) % _patrolPoints.Length;
        }

    }


    private bool DistanceCheck()
    {
        return Vector3.Distance(transform.position, _patrolPoints[_patrolIndex].position) <= _acceptanceRadius;
    }
}
