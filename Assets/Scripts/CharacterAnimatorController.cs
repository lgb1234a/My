using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    public Animator animator;
    public float d = 0.2f;

    public void SwitchAnimationState(Vector3 look, float distance)
    {
        animator.SetBool("MoveState", distance > d);
        if (distance > d) {
            animator.SetFloat("LookX", look.normalized.x);
            animator.SetFloat("LookY", look.normalized.y);
        }
    }
}
