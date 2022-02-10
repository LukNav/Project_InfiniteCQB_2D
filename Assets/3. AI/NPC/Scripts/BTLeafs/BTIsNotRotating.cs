using UnityEngine;

public class BTIsNotRotating :BTNode
{
    private NPCController _npcController;

    public BTIsNotRotating(NPCController npcController)
    {
        _npcController = npcController;
    }

    public override BTNodeStates Evaluate()
    {
        currentNodeState = _npcController.isRotating ? BTNodeStates.FAILURE : BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}