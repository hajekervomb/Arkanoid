using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MyAIPath : AIPath
{
    public event Action TargetReachedEvent;

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        TargetReachedEvent?.Invoke();
    }
}