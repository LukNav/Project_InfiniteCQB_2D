public class BTTimer_Restart : BTNode
{
    Timer timer;

    public BTTimer_Restart(Timer timer)
    {
        this.timer = timer;
    }

    public override BTNodeStates Evaluate()
    {
        timer.Restart();
        return BTNodeStates.SUCCESS;
    }
}