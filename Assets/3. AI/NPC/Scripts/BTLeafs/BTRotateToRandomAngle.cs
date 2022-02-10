using UnityEngine;

public class BTRotateToRandomAngle : BTNode
{
    private NPCController _npcController;
    private bool _shouldRandomiseDirection;
    private float _angleIncrease;

    private float _lastSetAngle;
    public BTRotateToRandomAngle(NPCController npcController, float angleIncrease = 90f, bool randomiseDirection = true)
    {
        _npcController = npcController;
        _shouldRandomiseDirection = randomiseDirection;
        _angleIncrease = angleIncrease;
        _lastSetAngle = -9.99f;
    }

    public override BTNodeStates Evaluate()
    {
        if (!IsAlreadyRotatingToSameDirection())
            SetNewRandomAngle();

        currentNodeState = BTNodeStates.SUCCESS;//return failure if the npc is not rotating
        return currentNodeState;
    }

    private void SetNewRandomAngle()
    {
        float direction = _shouldRandomiseDirection ? Random.Range(-1, 2) : 1;
        direction = direction == 0 ? 1 : direction;
       
        float angle = _npcController.transform.eulerAngles.z + _angleIncrease * direction;
       

        angle -= angle > 360f ? 360f : 0f;
        Debug.Log("Before: " + _lastSetAngle + " After: " + angle);
        _npcController.rotationAngle = angle;
        _npcController.isRotating = true;
        _npcController.elapsedRotationTime = 0f;

        _lastSetAngle = angle;
    }

    private bool IsAlreadyRotatingToSameDirection()
    {
        return _lastSetAngle == _npcController.rotationAngle && _npcController.isRotating;
    }
}