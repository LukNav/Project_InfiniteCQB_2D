using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToTarget : BTNode
{
    private FieldOfView _fov;
    private NavMeshAgent _agent;
    public BTMoveToTarget(FieldOfView fov, NavMeshAgent agent)
    {
        _fov = fov;
        _agent = agent;
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
        _agent.SetDestination(target.position);
    }
}
