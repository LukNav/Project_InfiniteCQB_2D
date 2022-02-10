public class BTTimer_Stop : BTNode
{
    Timer timer;

    public BTTimer_Stop(Timer timer)
    {
        this.timer = timer;
    }

    public override BTNodeStates Evaluate()
    {
        timer.Stop();
        return BTNodeStates.SUCCESS;
    }
}