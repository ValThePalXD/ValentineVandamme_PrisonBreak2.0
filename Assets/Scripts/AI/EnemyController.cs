using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Animation Parameters")]
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CharacterControllerBehaviour _character;

    [SerializeField]
    private NavMeshAgent _agent;

    private int _fallDead = Animator.StringToHash("FallDead");
    private int _isCrouching = Animator.StringToHash("IsCrouching");
    private int _seeing = Animator.StringToHash("Seeing");

    public void Seeing()
    {
        _animator.SetTrigger(_seeing);
    }

    public void DeathAnimation()
    {    
        _animator.SetTrigger(_fallDead);
    }  

    public void StartCrouch()
    {
        _agent.speed = 1.2f;
        _animator.SetBool(_isCrouching, true);
    }

    public void EndCrouch()
    {
        _agent.speed = 2f;
        _animator.SetBool(_isCrouching, false);
    }

}
