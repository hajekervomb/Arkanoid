using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletFire : MonoBehaviour
{
    
    public float fireTime = 0.05f;    
   

    // Start is called before the first frame update
    void Start()
    {              

        InvokeRepeating("Fire", fireTime, fireTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire ()
    {
        GameObject obj = NewObjectPoolerScript.current.GetPooledObject();

        if (obj == null) return;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
                
        
    }
}
