using UnityEngine;

public class BTDebugMessage : BTNode
{
    string message;
    public BTDebugMessage(string message)
    {
        this.message = message;
    }

    public override BTNodeStates Evaluate()
    {
        Debug.Log(message);
        return BTNodeStates.SUCCESS;
    }
}