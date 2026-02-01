using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls animation, and applies it to the enemy's location.
/// </summary>
public class GuardAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] PatrollingComponent patrollingComponent;
    [SerializeField] GameObject animationHolder;
    [SerializeField] GameObject enemy;

    private void Start()
    {
        if (animator == null || patrollingComponent == null)
        {
            Destroy(this);
            return;
        }

        if (patrollingComponent.isActiveAndEnabled)
        {
            animator.SetBool("IsMoving", true);
        }
    }

    private void Update()
    {
        //Move the animation holder to the correct position.
        animationHolder.transform.position = enemy.transform.position;

        //If the rotation of the enemy is below 180, keep rotation the same, otherwise flip it.
        sr.flipX = enemy.transform.rotation.eulerAngles.z > 180;
    }
}
