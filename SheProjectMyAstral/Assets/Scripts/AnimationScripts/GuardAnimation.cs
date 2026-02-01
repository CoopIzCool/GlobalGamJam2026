using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PatrollingComponent patrollingComponent;

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
}
