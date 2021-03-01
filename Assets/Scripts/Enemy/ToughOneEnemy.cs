using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughOneEnemy : Enemy
{
    public override void Init()
    {
        base.Init();
        Health = 200;
    }
}
