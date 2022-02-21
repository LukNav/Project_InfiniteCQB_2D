using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToTarget : BTNode
{
    FieldOfView _fov;
    NPCController _npcController;

    public BTMoveToTarget(FieldOfView fov, NPCController npcController)
    {
        _fov = fov;
        _npcController = npcController;

    }

    public override BTNodeStates Evaluate()
    {
        Transform target = _fov.GetTarget();
        if(target == null)
        {
            currentNodeState = BTNodeStates.FAILURE;
            return currentNodeState;
        }    

        FollowTarget(target);
        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }

    private void FollowTarget(Transform target)
    {
        _npcController.FollowTarget(target.position);
    }
}
