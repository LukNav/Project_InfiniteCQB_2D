using UnityEngine;

public class BTRotateToRandomAngle : BTNode
{
    private NPCController _npcController;
    private bool _shouldRandomiseDirection;
    private float _angleIncrease;

    private Vector3 _lastRotationDirection;
    public BTRotateToRandomAngle(NPCController npcController, float angleIncrease = 90f, bool randomiseDirection = true)
    {
        _npcController = npcController;
        _shouldRandomiseDirection = randomiseDirection;
        _angleIncrease = angleIncrease;
        _lastRotationDirection = Vector3.zero;
    }

    public override BTNodeStates Evaluate()
    {
        SetNewRotationDirection();

        currentNodeState = BTNodeStates.SUCCESS;//return failure if the npc is not rotating
        return currentNodeState;
    }

    private void SetNewRotationDirection()
    {
        float direction = _shouldRandomiseDirection ? Random.Range(-1, 2) : 1;
        direction = direction == 0 ? 1 : direction;
        _lastRotationDirection =new Vector3(Mathf.Sin(Mathf.Deg2Rad * (_angleIncrease * direction + _npcController.transform.eulerAngles.z)), Mathf.Cos(Mathf.Deg2Rad * (_angleIncrease * direction + _npcController.transform.eulerAngles.z)), 0f).normalized;
        if(!IsAlreadyRotatingToSameDirection())
            _npcController.rotationDirection = _lastRotationDirection;
    }

    private bool IsAlreadyRotatingToSameDirection()
    {
        return _lastRotationDirection == _npcController.rotationDirection && _npcController.isRotating;
    }
}