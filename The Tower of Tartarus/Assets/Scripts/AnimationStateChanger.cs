using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string currentState;

    public void ChangeAnimationState(string newState, float moving, float speed = 0){
        if(moving == 0){
            animator.speed = 0;
        }else{
            animator.speed = speed/2;
        }

        if(currentState == newState){
            return;
        }
        currentState = newState;
        animator.Play(currentState);
    }
}