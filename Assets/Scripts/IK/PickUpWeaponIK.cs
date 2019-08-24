using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeaponIK : StateMachineBehaviour
{
    public WeaponScript WeaponScript;
    private float _IKWeightRightHand;
    private Vector3 _targetPosition;
   

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _targetPosition = WeaponScript.Handle.position;
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _IKWeightRightHand = animator.GetFloat("WeaponIKWeight");
        animator.SetIKPosition(AvatarIKGoal.RightHand, _targetPosition);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _IKWeightRightHand);
    }
}
