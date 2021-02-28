using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static  class MiscTools
{
    public static Vector3 GetScreenBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenVector = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(screenVector);
    }
}
