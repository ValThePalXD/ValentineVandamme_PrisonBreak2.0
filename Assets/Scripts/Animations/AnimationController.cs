using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController 
{
    private Animator _animator;    
    private int _inputX = Animator.StringToHash("InputX");
    private int _inputY = Animator.StringToHash("InputY");
    private int _jump = Animator.StringToHash("Jump");
    private int _kick = Animator.StringToHash("Kick");
    private int _pickup = Animator.StringToHash("Pickup");
    private int _falling = Animator.StringToHash("Falling");
    private int _attack = Animator.StringToHash("Attack");
    private int _seen = Animator.StringToHash("Seen");
    private int _win = Animator.StringToHash("Won");

    public readonly PickUpWeaponIK pickUpWeaponIK;    

    public void Won()
    {
        _animator.SetTrigger(_win);
    }

    public void Seen()
    {
        _animator.SetTrigger(_seen);
    }

    public void Attacking()
    {
        _animator.SetTrigger(_attack);
    }

    public void Falling()
    {
        _animator.SetTrigger(_falling);
    }

    public void Pickup()
    {
        _animator.SetTrigger(_pickup);
    }

    public void Kick()
    {
        _animator.SetTrigger(_kick);
    }

    public void Jump()
    {
        _animator.SetTrigger(_jump);
    }

    public void SetInputX(float inputX)
    {
        _animator.SetFloat(_inputX, inputX);
    }

    public void SetInputY(float inputY)
    {
        _animator.SetFloat(_inputY, inputY);
    }

    public AnimationController(Animator animator)
    {
        _animator = animator;
        pickUpWeaponIK = _animator.GetBehaviour<PickUpWeaponIK>();
    }
}