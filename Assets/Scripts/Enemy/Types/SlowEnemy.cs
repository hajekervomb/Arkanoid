using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : Enemy
{
    public override void Init()
    {
        base.Init();
        Health = 3;
    }
}