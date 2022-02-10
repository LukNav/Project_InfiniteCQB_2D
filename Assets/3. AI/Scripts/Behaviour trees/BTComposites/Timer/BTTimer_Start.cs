public class BTTimer_Start : BTNode
{
    Timer timer;

    public BTTimer_Start(Timer timer)
    {
        this.timer = timer;
    }

    public override BTNodeStates Evaluate()
    {
        timer.StartIfNotRunning();
        return BTNodeStates.SUCCESS;
    }
}