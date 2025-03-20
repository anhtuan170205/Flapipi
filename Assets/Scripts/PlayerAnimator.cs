using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Player.OnPlayerDied += Player_OnPlayerDied;
    }

    private void Player_OnPlayerDied()
    {
        animator.SetBool("isDied", true);
    }
    private void OnDestroy()
    {
        Player.OnPlayerDied -= Player_OnPlayerDied;
    }
}
