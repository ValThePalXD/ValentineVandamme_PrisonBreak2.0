using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionsController : MonoBehaviour
{
    private Animator _animator;

    private int _fallDead = Animator.StringToHash("FallDead");

    private void Start()
    {
        _animator = GetComponent<Animator>();  
    }

    public void Death()
    {
        
        _animator.SetTrigger(_fallDead);
    }

    public EnemyActionsController(Animator animator)
    {
        _animator = animator;
    }
}
