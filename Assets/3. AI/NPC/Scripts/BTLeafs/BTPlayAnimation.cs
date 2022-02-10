using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class BTPlayAnimation : BTNode
{
    Animator _animator;
    string _animationName;

    public BTPlayAnimation(Animator animator, string animationName)
    {
        _animator = animator;
        _animationName = animationName;
    }

    public override BTNodeStates Evaluate()
    {
        _animator.Play(_animationName);

        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}
