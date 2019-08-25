using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    [SerializeField]
    private Collider _hitBox;

    [SerializeField]
    private Collider _capsuleCollider;

    [SerializeField]
    private AIBehaviour _behaviour;

    public void DisableHitbox()
    {
        _hitBox.enabled = false;
        _capsuleCollider.enabled = false;
        _behaviour.enabled = false;
    }
}
