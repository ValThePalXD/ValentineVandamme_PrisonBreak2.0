using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class CharacterControllerBehaviour : MonoBehaviour
{
    [Header("Game won?")]
    [SerializeField]
    private bool gameWon = false;

    private PlayerMovement _playerMovement;
    private AnimationController _animationController;

    private bool _isKicking;
    private bool _isPickingUp;

    private Vector3 _objectDirection = Vector3.zero;

    private PushObjectScript _pushObject;

    [Header("Animation Parameters")]
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _leftJoyStickX;
    [SerializeField]
    private float _leftJoyStickY;


    [Header("Camera Parameters")]
    [SerializeField]
    private float _yRotation;
    [SerializeField]
    private float _xRotation;
    [SerializeField]
    private float _yRotationSpeed;
    [SerializeField]
    private float _xRotationSpeed;
    [SerializeField]
    private CameraController _camController;

    [Header("IK Parameters")]
    [SerializeField]
    private Transform _rightHand;
    private WeaponScript _weaponScript;


    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _animationController = new AnimationController(_animator);

    }

    void Update()
    {
        SceneReload();
        if (_isKicking || _isPickingUp)
        {
            return;
        }
                  

        HandleJoyStickInput();
        MovePlayer();
        _playerMovement.RotatePlayerHorizontally(_xRotation * _xRotationSpeed * Time.deltaTime);
        _camController.RotateCamVertically(_yRotation * _yRotationSpeed * Time.deltaTime);

        _animationController.SetInputX(_leftJoyStickX);
        _animationController.SetInputY(_leftJoyStickY);

        if (Input.GetButtonDown("Jump"))
        {
            _playerMovement.Jump();
            _animationController.Jump();
        }
    }

    #region MovementPlayer

    private void MovePlayer()
    {
        _playerMovement.Movement = new Vector3(_leftJoyStickX, 0, _leftJoyStickY);
    }

    #endregion

    #region ReloadScene and dance


    private void SceneReload()
    {
        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PushObjectScript pushobject = other.GetComponent<PushObjectScript>();
        if (pushobject && Input.GetButtonDown("Push") && !_isKicking)
        {
            _pushObject = pushobject;
            KickObject(other.transform);
            return;
        }

        WeaponScript weapon = other.GetComponent<WeaponScript>();
        if (weapon && Input.GetButtonDown("Interact") && !_isPickingUp)
        {
            _weaponScript = weapon;
            _isPickingUp = true;
            _animationController.pickUpWeaponIK.WeaponScript = weapon;
            _animationController.Pickup();
            weapon.DisablePickUp();
            _playerMovement.Stop();
            
        }

        if (other.tag == "reload")
        {
            SceneManager.LoadScene(0);

        }

        //if (other.tag == "praying")
        //{
        //    _animatorEnemy.SetBool("Found", true);
        //    _animatorEnemy2.SetBool("Found", true);
        //    _animator.SetBool("IsPraying", true);
        //    _movement = Vector3.zero;
        //    gameLost = true;


        //}

    }



    #endregion







   
    public void ButtonPressed()
    {
        _animator.SetBool("IsPressing", false);
    }


    public void FinishPray()
    {
        _animator.SetBool("IsPraying", false);
        SceneManager.LoadScene(0);
    }


    #endregion

    private void HandleJoyStickInput()
    {
        _leftJoyStickY = Input.GetAxis("Vertical");
        _leftJoyStickX = Input.GetAxis("Horizontal");

        _xRotation = Input.GetAxis("HorizontalCam");
        _yRotation = Input.GetAxis("VerticalCam");
    }

    private IEnumerator RotateToObstacle()
    {
        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(_objectDirection)) > 2f)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, _objectDirection, 0.04f, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            yield return true;
        }
        DoKick();
    }

    private void DoKick()
    {
        _animationController.Kick();
    }

    public void KickTouch()
    {
        _pushObject.PushObject(_objectDirection);
        
    }

    public void StopKicking()
    {
        _isKicking = false;
    }

    private void KickObject(Transform kickObject)
    {
        _isKicking = true;
        _playerMovement.Stop();
        GetObjectDirection(kickObject.position);
        StartCoroutine(RotateToObstacle());

    }

    private void GetObjectDirection(Vector3 objectPosition)
    {
        _objectDirection = objectPosition - transform.position;
        if (Mathf.Abs(_objectDirection.x) > Mathf.Abs(_objectDirection.z))
        {
            _objectDirection.z = 0;
            _objectDirection.x /= Mathf.Abs(_objectDirection.x);
        }
        else
        {
            _objectDirection.z /= Mathf.Abs(_objectDirection.z);
            _objectDirection.x = 0;
        }
        _objectDirection.y = 0;
    }



    private void PickUpWeapon()
    {
        _weaponScript.transform.parent = _rightHand;
        _weaponScript.SetPositionAndRotationInHand();
    }

    public void StopPickingUp()
    {
        _isPickingUp = false;
    }

}
