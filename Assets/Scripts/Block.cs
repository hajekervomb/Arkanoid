using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;

    private SpriteRenderer sr;
    private BlocksManager blocksManager;

    [SerializeField] private ParticleSystem destroyEffect;
    
    
    private void Start()
    {
        blocksManager = BlocksManager.Instance;
        sr = GetComponent<SpriteRenderer>();

        ChangeSprite(gameObject);
    }

    
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        
        blockHealth -= 1;

        if (blockHealth <= 0)
        {
            Destroy(gameObject);
            blocksManager.OnBlockDestroyed();
            SpawnEffect();
        }
        else
        {
            // change sprite
            // sr.sprite = blocksManager.sprites[blockHealth - 1];
            // why collissionInfo.collider.tag didn't work and gameObject.tag works fine for switch statement?
            ChangeSprite(gameObject);
                        
        }

    }

    private void ChangeSprite(GameObject other)
    {
        switch (other.gameObject.tag)
        {
            case "Red Block":
                sr.sprite = blocksManager.redSprites[blockHealth - 1];
                break;

            case "Blue Block":
                sr.sprite = blocksManager.blueSprites[blockHealth - 1];
                break;

            case "Yellow Block":
                sr.sprite = blocksManager.yellowSprites[blockHealth - 1];
                break;

            case "Green Block":
                sr.sprite = blocksManager.greenSprites[blockHealth - 1];
                break;

            case "Pink Block":
                sr.sprite = blocksManager.pinkSprites[blockHealth - 1];
                Debug.Log("test");
                break;

        }
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
}
