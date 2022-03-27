using UnityEngine;

public class BTRotateToScanningAngle : BTNode
{
    private IBTScanningController _aiController;

    public BTRotateToScanningAngle(IBTScanningController aiController)
    {
        _aiController = aiController;
    }

    public override BTNodeStates Evaluate()
    {
        bool isStillRotating = Mathf.Abs(_aiController.scanningRotationAngle - _aiController.rotation.eulerAngles.z) > 0.005;
        
        if (isStillRotating)
            RotateToAngle(_aiController.scanningRotationAngle);
        else
            currentNodeState = BTNodeStates.SUCCESS;

        return currentNodeState;
    }

    private void RotateToAngle(float angle)
    {

        if (angle >= 180f) angle = 180f - angle;
        if (angle <= -180f) angle = -180f + angle;

        Quaternion targetRotation =
            _aiController.rotation *
            Quaternion.Euler(0f, 0f, Mathf.Clamp(angle, -_aiController.scanningRotationSpeed * Time.deltaTime, _aiController.scanningRotationSpeed * Time.deltaTime));

        _aiController.rotation = targetRotation;
    }


}