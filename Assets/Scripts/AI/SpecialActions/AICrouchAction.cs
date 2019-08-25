using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICrouchAction : MonoBehaviour
{
    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Crouch")
        {
            _enemyController.StartCrouch();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Crouch")
        {
            _enemyController.EndCrouch();
        }
    }

}
