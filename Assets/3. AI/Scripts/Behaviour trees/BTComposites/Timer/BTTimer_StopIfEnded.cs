public class BTTimer_StopIfEnded : BTNode
{
    Timer timer;

    public BTTimer_StopIfEnded(Timer timer)
    {
        this.timer = timer;
    }

    public override BTNodeStates Evaluate()
    {
        timer.Stop();
        return BTNodeStates.SUCCESS;
    }
}