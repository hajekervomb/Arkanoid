using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : Enemy
{
   public override void Init()
   {
      base.Init();
      Health = 100;
   }
}
