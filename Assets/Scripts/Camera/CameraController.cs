using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{

    [Header("Camera Parameters")]   
    [SerializeField]
    private Transform _cameraAnchor;
    [SerializeField]
    private Transform _camTransForm;

    private float _camVerticalRotation = 0;
       
    [Header("Camera Clamp Parameters")]
    [SerializeField]
    private float _minYAngle;
    [SerializeField]
    private float _maxYAngle;
       

    #region Camera

    public void RotateCamVertically(float rotation)
    {
        _camVerticalRotation += rotation;
        _camVerticalRotation = Mathf.Clamp(_camVerticalRotation, _minYAngle, _maxYAngle);
        Vector3 tempRotation = _cameraAnchor.eulerAngles;
        tempRotation.x = _camVerticalRotation;
        _cameraAnchor.eulerAngles = tempRotation;
    }

    #endregion           

}


