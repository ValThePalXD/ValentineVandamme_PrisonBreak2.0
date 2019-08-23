using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectScript : MonoBehaviour
{

    //private Rigidbody _rigidBody;
    private bool _isPushing;

    void Start()
    {
        //_rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isPushing)
        {

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Player") & Input.GetKey("space"))
        {
          
            _isPushing = true;
            //_rigidBody.AddForce(this.gameObject.transform.forward * 0.5f, ForceMode.Force);
            other.gameObject.transform.rotation = this.gameObject.transform.rotation;
            this.gameObject.transform.parent = other.gameObject.transform;
            
            //other.gameObject.transform.position = this.gameObject.transform.position;
        }

      

    }

}
