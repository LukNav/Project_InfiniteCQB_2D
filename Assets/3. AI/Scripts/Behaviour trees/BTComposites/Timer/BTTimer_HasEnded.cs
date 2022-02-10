using UnityEngine;
public class BTTimer_HasEnded : BTNode
{
    Timer timer;

    public BTTimer_HasEnded(Timer timer)
    {
        this.timer = timer;
    }

    public override BTNodeStates Evaluate()
    {
        if (timer.HasEnded)
        {
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}