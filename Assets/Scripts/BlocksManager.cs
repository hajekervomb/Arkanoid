using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlocksManager : MonoBehaviour
{
    #region Singleton


    public static BlocksManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public Sprite[] redSprites;
    public Sprite[] blueSprites;
    public Sprite[] greenSprites;
    public Sprite[] yellowSprites;
    public Sprite[] pinkSprites;

    // TODO - Добавить в мастер ветку!
    public event Action BlockDestroyed;

    public void OnBlockDestroyed()
    {
        BlockDestroyed?.Invoke();
    }
}
