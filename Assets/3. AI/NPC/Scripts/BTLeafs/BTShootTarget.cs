using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BTShootTarget : BTNode
{
    ShootingController _shootingController;
    private FieldOfView _fov;
    public BTShootTarget(ShootingController shootingController, FieldOfView fov)
    {
        _shootingController = shootingController;
        _fov = fov;
    }

    public override BTNodeStates Evaluate()
    {
        Transform target = _fov.GetTarget();
        if (target == null)
        {
            currentNodeState = BTNodeStates.FAILURE;
            return currentNodeState;
        }

        _shootingController.Fire(target);

        currentNodeState = BTNodeStates.SUCCESS;
        return currentNodeState;
    }
}