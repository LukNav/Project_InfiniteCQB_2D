using UnityEngine;
class BTIsRotatingToScanningAngle : BTNode
{
    private IBTScanningController _aiController;

    public BTIsRotatingToScanningAngle(IBTScanningController aiController)
    {
        _aiController = aiController;
    }

    public override BTNodeStates Evaluate()
    {

        return _aiController.hasRotated ? BTNodeStates.FAILURE : BTNodeStates.SUCCESS;
    }
}

