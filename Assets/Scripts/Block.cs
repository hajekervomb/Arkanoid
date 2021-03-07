using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;

    private SpriteRenderer sr;
    private GameManager gm;
    private BlocksManager blocksManager;

    [SerializeField] private ParticleSystem destroyEffect;

    public static Action<Block> onBrickDestroy = delegate { };


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        blocksManager = BlocksManager.Instance;
        gm = GameManager.Instance;
        //ChangeSprite();
    }

    
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        
        blockHealth -= 1;

        if (blockHealth <= 0)
        {
            blocksManager.RemainingBricks.Remove(this); //important to remove destroyed bricks
            onBrickDestroy(this);
            Destroy(gameObject);
            blocksManager.OnBlockDestroyed();
            SpawnEffect();
            GameManager.Score += 10;
        }
        else
        {
            // change sprite
                     
            ChangeSprite();
                        
        }

    }

    private void ChangeSprite()
    {
        sr.sprite = blocksManager.sprites[blockHealth - 1];
    }

    private void SpawnEffect()
    {
        Vector3 blockPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(blockPos.x, blockPos.y, blockPos.z - 0.2f);
        GameObject destroyEffectClone = Instantiate(destroyEffect.gameObject, spawnPos, Quaternion.identity);

        MainModule mm = destroyEffectClone.GetComponent<ParticleSystem>().main;
        mm.startColor = sr.color;
        Destroy(destroyEffectClone, destroyEffect.main.startLifetime.constant);
    }

    internal void Init(Transform containerTransform, Sprite sprite, Color color, int hitPoints)
    {
        // TODO: implement this
        transform.SetParent(containerTransform);
        sr.sprite = sprite;
        sr.color = color;
        blockHealth = hitPoints;
    }
       
}
