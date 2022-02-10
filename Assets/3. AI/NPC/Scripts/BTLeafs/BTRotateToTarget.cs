using UnityEngine;

public class BTRotateToTarget : BTNode
{
    private NPCController _npcController;
    private FieldOfView _fov;

    public BTRotateToTarget(NPCController npcController, FieldOfView fov)
    {
        _npcController = npcController;
        _fov = fov;
    }

    public override BTNodeStates Evaluate()
    {
        SetAngleToTarget();
        _npcController.isRotating = true;

        currentNodeState = BTNodeStates.SUCCESS;//return failure if the npc is not rotating
        return currentNodeState;
    }

    private void SetAngleToTarget()
    {
        Transform target = _fov.GetTarget();
        if (target == null)
            return;

        Vector3 direction = target.position - _npcController.transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (angle == _npcController.rotationAngle)
            return;

        _npcController.rotationAngle = angle;
    }
}