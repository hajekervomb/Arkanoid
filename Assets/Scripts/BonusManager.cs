using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public GameObject bonus;
        

    private void OnDestroy()
    {
        bonus = Instantiate(bonus, transform.position, transform.rotation);       
    }
}
