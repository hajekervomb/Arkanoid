using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageEnemy : Enemy
{
    public override void Init()
    {
        base.Init();
        Health = 2;
    }
}
