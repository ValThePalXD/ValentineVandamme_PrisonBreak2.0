using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Animation Parameters")]
    [SerializeField]
    private Animator _animator;

     
    public void DeathAnimation()
    {
        Debug.Log("aaaa");
        _animator.SetTrigger("FallDead");
    }
}
