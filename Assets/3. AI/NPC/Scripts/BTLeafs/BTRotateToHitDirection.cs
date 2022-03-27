using System;
using UnityEngine;

public class BTRotateToHitDirection : BTNode
{
    private NPCController _npcController;
    private StatsController _statsController;
    private Vector3 _hitDirection;
    public BTRotateToHitDirection(NPCController npcController)
    {
        _npcController = npcController;
        _statsController = npcController.statsController;
    }

    public override BTNodeStates Evaluate()
    {
        SetRotationAngle();

        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }

    /// <summary>
    /// Sets Rotation angle on NPCController to the direction of target
    /// </summary>
    /// <returns>return false if angle is already set</returns>
    private void SetRotationAngle()
    {
        _hitDirection = _statsController.hitDirection;
        float angle = Mathf.Atan2(_hitDirection.x, _hitDirection.y) * Mathf.Rad2Deg;
        //if(!IsAlreadyRotatingToSameDirection())
            _npcController.rotationDirection = _hitDirection;
    }

    //private bool IsAlreadyRotatingToSameDirection()
    //{
    //    return _hitDirection == _statsController.hitDirection;
    //}
}
