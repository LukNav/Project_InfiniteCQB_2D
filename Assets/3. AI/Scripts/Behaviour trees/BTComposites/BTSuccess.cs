public class BTSuccess : BTNode
{
    public BTSuccess()
    {
    }

    public override BTNodeStates Evaluate()
    {
        return BTNodeStates.SUCCESS;
    }
}