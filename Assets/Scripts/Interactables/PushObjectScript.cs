using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushObjectScript : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _forceMultiplier;
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
  
    public void PushObject(Vector3 direction)
    {
        _rigidBody.AddForce(direction * _forceMultiplier, ForceMode.Impulse);
    }    

}
