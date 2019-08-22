using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{

    [Header("Camera Parameters")]
    [SerializeField]
    private Transform _lookAt;

    [SerializeField]
    private Transform _camTransForm;



    [Header("Camera Clamp Parameters")]
    [SerializeField]
    private float _minYAngle;
    [SerializeField]
    private float _maxYAngle;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _currentY;
    [SerializeField]
    private float _currentX;







    //max angle almost 90 but not 90 because then u cant turn left and right anymore
    //private const float Y_ANGLE_MIN = 0.0f;
    //private const float Y_ANGLE_MAX = 89.0f;











    void Start()
    {

        //camera rotation movement
           MoveCameraStart();

#if DEBUG
       
        Assert.IsNotNull(_lookAt, "Dependency Error: This component needs a transform to look at.");
        Assert.IsNotNull(_camTransForm, "Dependency Error: This component needs camera transform.");
#endif



    }


    void Update()
    {     
       
        
     MoveCameraUpdate();    


    }
    


    #region Camera

    private void MoveCameraStart()
    {
        _camTransForm = transform;

    }






    private void MoveCameraUpdate()
    {
        _currentX -= Input.GetAxis("HorizontalCam");
        _currentY += Input.GetAxis("VerticalCam");



        _currentY = Mathf.Clamp(_currentY, _minYAngle, _maxYAngle);
        Vector3 dir = new Vector3(0, 0, -_distance);

        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);






        _camTransForm.position = _lookAt.position + rotation * dir;




        _camTransForm.LookAt(_lookAt.position);


    }

    #endregion








}






//[Header("Camera Parameters")]
//    [SerializeField]
//    private Camera _mainCamera;

//    [SerializeField]
//    private Transform _camTransform;

//    [SerializeField]
//    private Transform _cameraStartRootTransform;

//    [SerializeField]
//    private Transform _cameraStartDefaultPosition;
    

//    [Space]
//    [Header("Camera Clamping Parameters")]
//    [SerializeField] private float _camRotation;
//    [SerializeField] private float _minCamAngle;
//    [SerializeField] private float _maxCamAngle;

//    public Camera PlayerCamera { get => _mainCamera; }
//    public Transform CameraRoot { get => _cameraStartRootTransform; }
//    public Vector3 CameraPosition { get => _camTransform.position; }
//    public Quaternion CameraRotation { get => _camTransform.rotation; }


//    private Transform _cameraDefaultPosition;
//    private Transform _cameraAimPosition;

//    public bool IsAiming {private get; set; }


//    // Use this for initialization
//    void Start () {
//        _cameraDefaultPosition = _cameraStartDefaultPosition;
     
//	}
	
//	// Update is called once per frame
//	void Update () {

//        if (IsAiming)
//        {
//            Vector3 position = Vector3.Lerp(_camTransform.position, _cameraAimPosition.position, .2f);
//            Quaternion rotation = Quaternion.Lerp(_camTransform.rotation, _cameraAimPosition.rotation, .2f);

//            _camTransform.SetPositionAndRotation(position, rotation);
//        }
//        else
//        {
//            Vector3 position = Vector3.Lerp(_camTransform.position, _cameraDefaultPosition.position, .2f);
//            Quaternion rotation = Quaternion.Lerp(_camTransform.rotation, _cameraDefaultPosition.rotation, .2f);

//            _camTransform.SetPositionAndRotation(position, rotation);
//        }            

//    }

//    public void RotateVertically(float angle)
//    {
//        _camRotation += angle;

//        _camRotation = Mathf.Clamp(_camRotation, _minCamAngle, _maxCamAngle);

//        _cameraStartRootTransform.eulerAngles = new Vector3(_camRotation, _cameraStartRootTransform.eulerAngles.y, _cameraStartRootTransform.eulerAngles.z);
//    }

//    public void SetCameraAnchorAndPositions(Transform cameraAnchor, Transform defaultPosition, Transform aimPosition)
//    {
//        _camTransform.parent = cameraAnchor;
//        _cameraDefaultPosition = defaultPosition;
//        _cameraAimPosition = aimPosition;
//    }

//    public void ResetCameraAnchorAndPositions()
//    {
//        _camTransform.parent = _cameraStartRootTransform;
//        _cameraDefaultPosition = _cameraStartDefaultPosition;
        
//    }

//}
