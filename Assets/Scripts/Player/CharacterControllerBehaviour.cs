using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class CharacterControllerBehaviour : MonoBehaviour
{
    [Header("Game won?")]
    [SerializeField]
    private bool gameWon = false;

    //[SerializeField]
    //private bool gameLost = false;

    private PlayerMovement _playerMovement;
    private AnimationController _animationController;

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


    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _animationController = new AnimationController(_animator);

    }

    void Update()
    {
        HandleJoyStickInput();

         MovePlayer();
        _playerMovement.RotatePlayerHorizontally(_xRotation * _xRotationSpeed * Time.deltaTime);
        _camController.RotateCamVertically(_yRotation * _yRotationSpeed * Time.deltaTime);
        
        _animationController.SetInputX(_leftJoyStickX);
        _animationController.SetInputY(_leftJoyStickY);     
               
        SceneReload();         
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
        if (Input.GetButton("Reset"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PushObjectScript pushobject = other.GetComponent<PushObjectScript>();
        if (pushobject && Input.GetKeyDown(KeyCode.A))
        {
            pushobject.PushObject(transform.position);
            return;
        }


        if (other.tag == "dance")
        {
            _animator.SetBool("IsDancing", true);
            gameWon = true;
            transform.position = Vector3.Lerp(this.gameObject.transform.position, other.gameObject.transform.position, 0.5f);
            return;

        }

        

        //if (other.tag == "reload")
        //{
        //    SceneManager.LoadScene(0);

        //}

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


    #region Animator
    private void AnimatorBooleans()
    {
        _animator.SetBool("IsGrounded", _playerMovement.IsGrounded);


    }
    #endregion


   


    #region AnimationEvents



    //public void FinishClimb()
    //{

    //    gameObject.transform.position = _endPos.transform.position;
    //    _animator.SetBool("IsClimbing", false);


    //}

    public void FinishPush()
    {
        _animator.SetBool("IsPushing", false);

    }

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

}
