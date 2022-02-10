using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTResetRotationTimer : BTNode
{
    NPCController _npcController;

    public BTResetRotationTimer(NPCController npcController)
    {
        _npcController = npcController;
    }

    public override BTNodeStates Evaluate()
    {
        _npcController.elapsedRotationTime = 0f;
        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}
