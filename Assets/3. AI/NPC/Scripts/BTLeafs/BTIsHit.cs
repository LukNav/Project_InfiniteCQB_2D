using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIsHit : BTNode
{
    StatsController _statsController;

    public BTIsHit(StatsController statsController)
    {
        _statsController = statsController;
    }

    public override BTNodeStates Evaluate()
    {
        currentNodeState = _statsController.isHit ? BTNodeStates.SUCCESS : BTNodeStates.FAILURE;
        return currentNodeState;
    }
}
