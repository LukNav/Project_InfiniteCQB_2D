using UnityEngine;
class BTSetScanningAngle : BTNode
{
    private IBTScanningController _aiController;
    private bool _shouldRandomiseDirection;
    private float _angleIncrease;

    public BTSetScanningAngle(IBTScanningController aiController, float maximumAngleChange = 180f, bool randomiseDirection = true)
    {
        _aiController = aiController;
        _shouldRandomiseDirection = randomiseDirection;
        _angleIncrease = maximumAngleChange;
    }

    public override BTNodeStates Evaluate()
    {
        float direction = _shouldRandomiseDirection ? Random.Range(-1f, 1f) /*>= 0 ? 1 : -1 -- use this to remove 0 from equation*/ : 1f;
        float angle = _aiController.rotation.eulerAngles.z + _angleIncrease * direction;
        _aiController.scanningRotationAngle = angle;
        return BTNodeStates.SUCCESS;
    }
}

