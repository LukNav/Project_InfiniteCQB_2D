using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Runs all nodes in a sequence
/// </summary>
public class BTSelector : BTNode
{
    protected List<BTNode> nodes = new List<BTNode>();
    public BTSelector(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override BTNodeStates Evaluate()
    {
        bool childRunning = false;

        foreach(BTNode node in nodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    continue;

                case BTNodeStates.SUCCESS:
                    currentNodeState = BTNodeStates.SUCCESS;
                    return currentNodeState;

                default:
                    continue;
            }
        }
        currentNodeState = BTNodeStates.FAILURE;
        return currentNodeState;
    }
}