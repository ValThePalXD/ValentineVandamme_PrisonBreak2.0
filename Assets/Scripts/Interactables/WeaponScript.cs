using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private Transform _handle;
    public Transform Handle { get => _handle; }


    [SerializeField]
    private Vector3 _positionInHand;
    [SerializeField]
    private Vector3 _rotationInHand;
    [SerializeField]
    private Collider _pickUpTrigger;

    public void DisablePickUp()
    {
        _pickUpTrigger.enabled = false;
    }

    public void SetPositionAndRotationInHand()
    {
        transform.localPosition = _positionInHand;
        transform.localEulerAngles = _rotationInHand;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (transform.parent)
            SetPositionAndRotationInHand();
    }
#endif
}
