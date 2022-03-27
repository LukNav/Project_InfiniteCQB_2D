using UnityEngine;

public class BTSetFollowTargetStateFalse : BTNode
{
    private NPCController _npcController;
    
    public BTSetFollowTargetStateFalse(NPCController npcController)
    {
        _npcController = npcController;
    }

    public override BTNodeStates Evaluate()
    {
        _npcController.isFollowingTarget = false;
        return BTNodeStates.SUCCESS;
    }
}