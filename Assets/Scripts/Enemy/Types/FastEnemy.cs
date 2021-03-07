using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
   public override void Init()
   {
      base.Init();
      Health = 1;
   }
}
