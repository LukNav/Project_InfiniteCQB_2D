using UnityEngine;

public class BTRotateToTarget : BTNode
{
    private NPCController _npcController;
    private FieldOfView _fov;
    
    private Vector3 _lastRotationDirection;
    private float _elapsedRotationTime = 0f;
    private AnimationCurve _rotationSpeedCurve;
    
    public BTRotateToTarget(NPCController npcController, FieldOfView fov)
    {
        _npcController = npcController;
        _rotationSpeedCurve = npcController.rotationSpeedCurve;
        _fov = fov;
    }

    public override BTNodeStates Evaluate()
    {
        SetDirection();
        Rotate();
        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }

    private void SetDirection()
    {
        Transform target = _fov.GetTarget();

        Vector3 direction = target.position - _npcController.transform.position;
        direction = direction.normalized;

        if (_lastRotationDirection == direction 
            && _npcController.isFollowingTarget && _elapsedRotationTime < 1)    
            return;

        _npcController.isFollowingTarget = true;
        _elapsedRotationTime = 0f;
        _lastRotationDirection = direction;
    }

    private void Rotate()
    {
        if (_elapsedRotationTime > 1)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, _lastRotationDirection);
        _elapsedRotationTime += Time.deltaTime;
        _npcController.transform.rotation = Quaternion.Lerp(_npcController.transform.rotation, targetRotation, _rotationSpeedCurve.Evaluate(_elapsedRotationTime));
        
    }

}