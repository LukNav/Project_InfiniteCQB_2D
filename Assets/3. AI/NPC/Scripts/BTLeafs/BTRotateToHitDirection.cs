using System;
using UnityEngine;

public class BTRotateToHitDirection : BTNode
{
    private NPCController _npcController;
    private StatsController _statsController;

    private float _previousAngle = -999f;

    public BTRotateToHitDirection(NPCController npcController)
    {
        _npcController = npcController;
        _statsController = npcController.statsController;
    }

    public override BTNodeStates Evaluate()
    {
        float angle = CalculateAngle(_statsController.hitDirection+_npcController.transform.position);
        bool hasFinishedRoatating = Mathf.Abs(angle - _previousAngle) < 0.005;

        if (!hasFinishedRoatating)
            TurnToHitDirection(angle);
        else
            _statsController.ResetHitInfo();

        _previousAngle = angle;
        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }

    private void TurnToHitDirection(float angle)
    {

        if (angle >= 180f) angle = 180f - angle;
        if (angle <= -180f) angle = -180f + angle;

        Quaternion targetRotation =
            _npcController.transform.rotation *
            Quaternion.Euler(0f, 0f, Mathf.Clamp(angle, -_npcController.QuickRotationSpeed * Time.deltaTime, _npcController.QuickRotationSpeed * Time.deltaTime));

        //if ((turretControllerObject.yawLimit < 360f) && (turretControllerObject.yawLimit > 0f))
        //    yawSegment.rotation = Quaternion.RotateTowards(yawSegment.parent.rotation * yawSegmentStartRotation, targetRotation, turretControllerObject.yawLimit);
        /*else */
        _npcController.transform.rotation = targetRotation;
        
    }

    private float CalculateAngle(Vector3 target)
    {
        float angle = 0f;
        Vector3 targetRelative = _npcController.transform.InverseTransformPoint(target);
        angle = Mathf.Atan2(targetRelative.y, targetRelative.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }
}
