using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Runs all nodes in a sequence
/// </summary>
public class BTSequence : BTNode
{
    protected List<BTNode> nodes = new List<BTNode>();
    public BTSequence(List<BTNode> nodes)
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
                    currentNodeState = BTNodeStates.FAILURE;
                    return currentNodeState;

                case BTNodeStates.SUCCESS:
                    continue;

                case BTNodeStates.RUNNING:
                    childRunning = true;
                    continue;

                default:
                    currentNodeState = BTNodeStates.SUCCESS;
                    return currentNodeState;
            }
        }
        currentNodeState = childRunning ? BTNodeStates.RUNNING : BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}