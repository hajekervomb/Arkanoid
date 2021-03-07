using System;
using Pathfinding;

public class MyAIPath : AIPath
{
    public event Action TargetReachedEvent;

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        TargetReachedEvent?.Invoke();
    }
}