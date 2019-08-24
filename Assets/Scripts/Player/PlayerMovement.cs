using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Locomotion Parameters")]
    [SerializeField]
    private float _mass = 66.7f; // the average weight of an adult woman in Belgium is 66.7 kg

    [SerializeField]
    private float _acceleration = 80; // [m/s^2]

    [SerializeField]
    private float _dragOnGround = 15; // []

    [SerializeField]
    private float _maxWalkingSpeed = (9.5f * 1000) / (60 * 60); // setting default forwardspeed

    [SerializeField]
    private float _maxForwardSpeed = (9.5f * 1000) / (60 * 60); // the average jogging speed of a human is about 9.5 km/h

    [SerializeField]
    private float _maxBackwardSpeed = ((9.5f / 1.1f) * 1000) / (60 * 60); //the average backwards jogging speed is about 1.1 times slower than forward speed

    [Header("Jump Parameters")]
     public bool _isJumping;
    [SerializeField]
    private float _jumpHeight;

    private Vector3 _velocity = Vector3.zero;

    public Vector3 Movement { get; set; }
        
    private CharacterController _characterController;

    [Header("Dependencies")]
    [SerializeField, Tooltip("What should determine the absolute forward when a player presses forward.")]
    private Transform _absoluteForward;

    public bool IsGrounded { get => _characterController.isGrounded; }

    private void Start()
    {
        
        _characterController = GetComponent<CharacterController>();
      
    }
  
    void FixedUpdate()
    {
        ApplyGround();
        ApplyMovement();
        ApplyGroundDrag();                    
        ApplyGravity();
        ApplyJump();
        LimitMaximumRunningSpeed();
        
        _characterController.Move(_velocity * Time.deltaTime);

        SlowerBackwards();       
    }



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

            Vector3 relativeMovement = forwardRotation * Movement;

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

    private void ApplyJump()
    {
        //https://en.wikipedia.org/wiki/Equations_of_motion
        //v^2 = v0^2  + 2*a(r - r0)
        //v = 0
        //v0 = ?
        //a = 9.81
        //r = 1
        //r0 = 0
        //v0 = sqrt(2 * 9.81 * 1) 
        //but => g is inverted

        if (_isJumping && _characterController.isGrounded)
        {
            _velocity += -Physics.gravity.normalized * Mathf.Sqrt(2 * Physics.gravity.magnitude * _jumpHeight);
            _isJumping = false;

        }

    }

    private void LimitMaximumRunningSpeed()
    {
        Vector3 yVelocity = Vector3.Scale(_velocity, new Vector3(0, 1, 0));

        Vector3 xzVelocity = Vector3.Scale(_velocity, new Vector3(1, 0, 1));
        Vector3 clampedXzVelocity = Vector3.ClampMagnitude(xzVelocity, _maxWalkingSpeed);

        _velocity = yVelocity + clampedXzVelocity;
    }


    private void SlowerBackwards()
    {
        if (Movement.z < 0f)
        {
            _maxWalkingSpeed = _maxBackwardSpeed;
        }
        else
        {
            _maxWalkingSpeed = _maxForwardSpeed;
        }
    }
    #endregion

    public void RotatePlayerHorizontally(float rotation)
    {
        Vector3 tempRotation = transform.eulerAngles;
        tempRotation.y += rotation;
        transform.eulerAngles = tempRotation;
    }

    public void Jump()
    {
        _isJumping = true;
    }


}
