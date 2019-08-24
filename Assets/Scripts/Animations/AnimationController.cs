﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController 
{
    private Animator _animator;
    private int _inputX = Animator.StringToHash("InputX");
    private int _inputY = Animator.StringToHash("InputY");
    private int _jump = Animator.StringToHash("Jump");

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
    }
}