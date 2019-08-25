using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    [SerializeField]
    private Collider _hitBox;

    public void DisableHitbox()
    {
        _hitBox.enabled = false;
    }
}
