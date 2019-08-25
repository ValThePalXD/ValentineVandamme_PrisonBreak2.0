using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Animation Parameters")]
    [SerializeField]
    private Animator _animator;

    private int _fallDead = Animator.StringToHash("FallDead");
    private int _isCrouching = Animator.StringToHash("IsCrouching");

    public void DeathAnimation()
    {    
        _animator.SetTrigger(_fallDead);
    }  

    public void StartCrouch()
    {
        _animator.SetBool(_isCrouching, true);
    }

    public void EndCrouch()
    {
        _animator.SetBool(_isCrouching, false);
    }

}
