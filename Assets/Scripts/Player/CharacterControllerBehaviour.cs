using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerBehaviour : MonoBehaviour
{
    [Header("Game won?")]
    [SerializeField]
    private bool gameWon = false;

    [SerializeField]
    private bool gameLost = false;



    [Header("Animation Parameters")]
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float InputX;

    [SerializeField]
    private float InputY;


    [Header("Animation AI")]
    [SerializeField]
    private Animator _animatorEnemy;

    [SerializeField]
    private Animator _animatorEnemy2;


    [Header("Climbing Parameters")]

    [SerializeField]
    private GameObject EndPos;

    [SerializeField]
    private GameObject StartPos;

    [Header("Locomotion Parameters")]
    [SerializeField]
    private float _mass = 66.7f; // the average weight of an adult woman in Belgium is 66.7 kg

    [SerializeField]
    private float _acceleration = 2; // [m/s^2]

    [SerializeField]
    private float _dragOnGround = 30; // []

    [SerializeField]
    private float _maxWalkingSpeed = (5.0f * 1000) / (60 * 60); // setting default forwardspeed

    [SerializeField]
    private float _maxForwardSpeed = (5.0f * 1000) / (60 * 60); // the average walking speed of a human is about 5 km/h

    [SerializeField]
    private float _maxBackwardSpeed = ((5.0f/1.3f) * 1000) / (60 * 60); //the average backwards walking speed is about 1.1 times slower than forward speed, after tweaking I found 1.3 times to make the animation smoother and more realistic

   

    [Header("Dependencies")]
    [SerializeField, Tooltip("What should determine the absolute forward when a player presses forward.")]
    private Transform _absoluteForward;

    [SerializeField]
    private CharacterController _characterController;

    private Vector3 _velocity = Vector3.zero;

    private Vector3 _movement;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = this.gameObject.GetComponent<Animator>();
#if DEBUG
        Assert.IsNotNull(_animator, "Dependency Error: This component needs an Animator  to work.");
        Assert.IsNotNull(_characterController, "Dependency Error: This component needs a CharacterController to work.");
        Assert.IsNotNull(_absoluteForward, "Dependency Error: Set the Absolute Forward field.");
#endif
    }

    void Update()
    {
        if (!gameWon && !gameLost)
        {

            _movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        }
        
        

        InputY = Input.GetAxis("Vertical");
        InputX = Input.GetAxis("Horizontal");

        _animator.SetFloat("InputY", InputY);
        _animator.SetFloat("InputX", InputX);

   
        //if u walk backwards, you go slower
        if (InputY < 0f)
        {

            _maxWalkingSpeed = _maxBackwardSpeed;

        }
        else
        {

            _maxWalkingSpeed = _maxForwardSpeed;

        }


        SceneReload();

       



    }
    #region ReloadScene and dance


    private void SceneReload()
    {
        if (Input.GetButton("XButton"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "dance")
        {
            _animator.SetBool("IsDancing", true);
            gameWon = true;
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, other.gameObject.transform.position, 0.5f);

        }

        if (other.tag == "reload")
        {
            SceneManager.LoadScene(0);

        }

        if (other.tag == "praying")
        {
            _animatorEnemy.SetBool("Found", true);
            _animatorEnemy2.SetBool("Found", true);
            _animator.SetBool("IsPraying", true);
            _movement = Vector3.zero;
            gameLost = true;


        }

    }



    #endregion


    void FixedUpdate()
    {
 
            ApplyGround();
            ApplyMovement();
            ApplyGroundDrag();

            AnimatorBooleans();

            CamFollow();

            ApplyGravity();

            LimitMaximumRunningSpeed();
        
            _characterController.Move(_velocity * Time.deltaTime);
        

    }

    #region Camera

    private void CamFollow()
    {
        gameObject.transform.forward = new Vector3(_absoluteForward.transform.forward.x, gameObject.transform.forward.y, _absoluteForward.transform.forward.z);
    }

    #endregion


    #region Animator
    private void AnimatorBooleans()
    {
        _animator.SetBool("IsGrounded", _characterController.isGrounded);
       
       
    }
    #endregion


    #region CharController

    private void ApplyGround()
    {
        if (_characterController.isGrounded)
        {
          

            _velocity -= Vector3.Project(_velocity, Physics.gravity.normalized);
        }
    }

    private void ApplyGravity()
    {
        
        _velocity += Physics.gravity * Time.deltaTime; // g[m/s^2] * t[s]
       
    }

    private void ApplyMovement()
    {
        if (_characterController.isGrounded)
        {
            Vector3 xzAbsoluteForward = Vector3.Scale(_absoluteForward.forward, new Vector3(1, 0, 1));

            Quaternion forwardRotation =
                Quaternion.LookRotation(xzAbsoluteForward);

            Vector3 relativeMovement = forwardRotation * _movement;

            _velocity += relativeMovement * _mass * _acceleration * Time.deltaTime; // F(= m.a) [m/s^2] * t [s]
        }
    }

    private void ApplyGroundDrag()
    {
        if (_characterController.isGrounded)
        {
            _velocity = _velocity * (1 - Time.deltaTime * _dragOnGround);
        }
    }





    private void LimitMaximumRunningSpeed()
    {
        Vector3 yVelocity = Vector3.Scale(_velocity, new Vector3(0, 1, 0));

        Vector3 xzVelocity = Vector3.Scale(_velocity, new Vector3(1, 0, 1));
        Vector3 clampedXzVelocity = Vector3.ClampMagnitude(xzVelocity, _maxWalkingSpeed);

        _velocity = yVelocity + clampedXzVelocity;
    }

    #endregion


    #region AnimationEvents



    public void FinishClimb()
    {

        gameObject.transform.position = EndPos.transform.position;
        _animator.SetBool("IsClimbing", false);
      

    }
    
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

  
}
