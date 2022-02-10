public class BTFailure : BTNode
{
    public BTFailure()
    {
    }

    public override BTNodeStates Evaluate()
    {
        return BTNodeStates.FAILURE;
    }
}