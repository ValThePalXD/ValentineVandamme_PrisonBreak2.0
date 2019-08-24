using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushObjectScript : MonoBehaviour
{

    private Rigidbody _rigidBody;
    private bool _isPushing;

    [SerializeField]
    private float _forceMultiplier;
    

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
  

    public void PushObject(Vector3 pushorigin)
    {
        Vector3 direction = transform.position - pushorigin;
        if (direction.x > direction.z)
        {
            direction.z = 0;
            direction.x = 1;
        }
        else
        {
            direction.z = 1;
            direction.x = 0;
        }
        direction.y = 0;

        _rigidBody.AddForce(direction * _forceMultiplier, ForceMode.Impulse);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if ((other.tag == "Player"))
    //    {
    //        PushObject();

    //        _isPushing = true;
    //        //_rigidBody.AddForce(this.gameObject.transform.forward * 0.5f, ForceMode.Force);
    //        //other.gameObject.transform.rotation = this.gameObject.transform.rotation;
    //        //this.gameObject.transform.parent = other.gameObject.transform;
    //        //other.gameObject.transform.position = this.gameObject.transform.position;
    //    }



    //}

}
