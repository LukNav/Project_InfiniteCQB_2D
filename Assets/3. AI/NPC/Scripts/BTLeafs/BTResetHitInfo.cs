using UnityEngine;

public class BTResetHitInfo : BTNode//resets all of the HIT information contained in StatsController class
{
    private StatsController _statsController;

    public BTResetHitInfo(StatsController statsController)
    {
        _statsController = statsController;
    }
    public override BTNodeStates Evaluate()
    {
        _statsController.ResetHitInfo();
        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}