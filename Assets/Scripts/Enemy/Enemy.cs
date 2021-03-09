using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum EnemyType
{
    Fast = 0,
    Average = 1,
    Slow = 2,
}

public class Enemy : MonoBehaviour, IEnemy
{
    private EnemySpawner enemySpawner;
    private MyAIPath myAIPath;
    private DestinationSwitcher destinationSwitcher;

    private SpriteRenderer spriteRenderer;
    public int Health { get; set; }
    public int Speed { get; set; }
    private int ScoreForKilling { get; set; }

    // TODO - объединить ивенты и методы (добавив параметр?)
    public event EventHandler<MyEventArgs> EnemyKilled;
    public event EventHandler<MyEventArgs> EnemyHitTheRacket;
    public event EventHandler<MyEventArgs> EnemyEscaped;

    private void OnEnable()
    {
        BlocksManager.Instance.BlockDestroyed += RecalculatePath;

        // Т.к. блоки инстанируются в рантайме (а не сразу лежат на сцене), нужно сканить при спавне
        RecalculatePath();
    }

    // TODO - убрать. Сейчас нужно только для тестов
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public virtual void Init(EnemyType enemyType)
    {
        enemySpawner = GetComponentInParent<EnemySpawner>();
        myAIPath = GetComponentInParent<MyAIPath>();
        myAIPath.TargetReachedEvent += CheckMainDestinationPoint;
        destinationSwitcher = GetComponentInParent<DestinationSwitcher>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        EnemyTypeSO enemyTypeSO = enemySpawner.enemyTypesSOs.Find(type => type.name == enemyType.ToString());

        Health = enemyTypeSO.health;
        Speed = enemyTypeSO.speed;
        ScoreForKilling = 30 * Health * Speed / 100;

        spriteRenderer.sprite = enemyTypeSO.sprite;
        myAIPath.maxSpeed = Speed;
    }


    // Проверка дошел ли враг именно до своего основного пункта назначения
    private void CheckMainDestinationPoint()
    {
        if (destinationSwitcher.IsEnemyReachedMainDestinationPoint())
        {
            OnEnemyEscaped();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnEnemyDied(ScoreForKilling);
        Destroy(gameObject);
    }

    private void OnEnemyDied(int points)
    {
        MyEventArgs args = new MyEventArgs {scorePoints = points};
        EnemyKilled?.Invoke(this, args);
    }

    public void DestroyEnemy()
    {
        OnEnemyDestroyed(-ScoreForKilling); //TODO - добавить отдельную отрицательную переменную для очков?
        Destroy(gameObject);
    }

    private void OnEnemyDestroyed(int points)
    {
        MyEventArgs args = new MyEventArgs {scorePoints = points};
        EnemyHitTheRacket?.Invoke(this, args);
    }
    
    private void OnEnemyEscaped()
    {
        EnemyEscaped?.Invoke(this, null);
    }

    private void RecalculatePath()
    {
        AstarPath.active.Scan();
    }

    private void OnDestroy()
    {
        BlocksManager.Instance.BlockDestroyed -= RecalculatePath;
    }
}