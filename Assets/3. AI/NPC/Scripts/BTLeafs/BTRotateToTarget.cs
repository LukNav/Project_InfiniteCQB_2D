using UnityEngine;

public class BTRotateToTarget : BTNode
{
    private NPCController _npcController;
    private FieldOfView _fov;

    private float _previousAngle;
    
    public BTRotateToTarget(NPCController npcController, FieldOfView fov)
    {
        _npcController = npcController;
        _fov = fov;
    }

    public override BTNodeStates Evaluate()
    {
        Vector3 targetPos = _fov.GetTarget().position;
        float angle = CalculateAngle(targetPos);
        bool hasFinishedRoatating = Mathf.Abs(angle - _previousAngle) < 0.005;

        if (!hasFinishedRoatating)
            TurnToTarget(angle);

        _previousAngle = angle;
        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }

    private void TurnToTarget(float angle)
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