using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCanSeeTarget : BTNode
{
    FieldOfView fov;

    public BTCanSeeTarget(FieldOfView fov)
    {
        this.fov = fov;
    }

    public override BTNodeStates Evaluate()
    {
        if(fov.visibleTargets.Count > 0)
        {
            currentNodeState = BTNodeStates.SUCCESS;
            return currentNodeState;
        }

        currentNodeState = BTNodeStates.FAILURE;
        return currentNodeState;
    }
}
