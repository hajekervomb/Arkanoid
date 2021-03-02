using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyState
{
    MovingToMainTarget = 0,
    Wandering = 1,
}

public class TargetSwitcher : MonoBehaviour
{
    private GridGraph activeGrid;

    private Transform defaultTarget;

    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;
    private Transform currentTarget;
    private EnemyState currentState;

    private GraphNode EnemyNode
    {
        get
        {
            return AstarPath.active.GetNearest(this.gameObject.transform.position, NNConstraint.Default).node;
        }
    }

    private GraphNode TargetNode
    {
        get
        {
            return AstarPath.active.GetNearest(currentTarget.transform.position, NNConstraint.Default).node;
        }
    }

    private void OnEnable()
    {
        BlocksManager.Instance.BlockDestroyed += CheckPath;
    }

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        activeGrid = AstarPath.active.data.gridGraph;

        defaultTarget = GameObject.Find("Racket").transform; // временное говно
        currentTarget = defaultTarget;

        aiDestinationSetter = GetComponent<AIDestinationSetter>();

        CheckPath();
    }

    private void Update()
    {
       // TODO постоянно менять destination в wandering state?
    }

    private void SetEnemyState(EnemyState state)
    {
        currentState = state;

        switch (state)
        {
            // Идет к основной цели. Сейчас - это ракетка.
            case EnemyState.MovingToMainTarget:
                currentTarget = defaultTarget;
                aiDestinationSetter.target = currentTarget;
                break;
            
                    // "Слоняется" туда-сюда
            case EnemyState.Wandering:
                aiDestinationSetter.target = null; // таргет работает только с трансформом, а мы выбираем просто координаты в мире (без объекта)

                var randomDestination = GetRandomNode().position;

                    // Генерируем рандомную точку назначения до тех пор, пока не находим валидную (для текущего положения врага)
                while (!IsPathPossible(EnemyNode, GetGraphNodeByPosition((Vector3) randomDestination)))
                {
                    randomDestination = GetRandomNode().position;
                }

                aiPath.destination = (Vector3)randomDestination;
                
                break;
        }
    }

    private bool IsPathPossible(GraphNode node1, GraphNode node2)
    {
        return PathUtilities.IsPathPossible(node1, node2);
    }

    private void CheckPath()
    {
        // Первым делом проверям основной пункт назначения
        if (IsPathPossible(EnemyNode, TargetNode))
        {
            Debug.Log("Path is possible");
            SetEnemyState(EnemyState.MovingToMainTarget);
        }
        else
        {
            Debug.LogError("Path is blocked");
            SetEnemyState(EnemyState.Wandering);
        }
    }
    
    // TODO - взял из EnemySpawner. Решить где лучше оставить и как связать
    private GridNode GetRandomNode()
    {
        var randomNode = activeGrid.nodes[Random.Range(0, activeGrid.nodes.Length)];
        return randomNode;
    }

    // Возвращает нод, ближайший к переданным координатам
    private GraphNode GetGraphNodeByPosition(Vector3 position)
    {
        return AstarPath.active.GetNearest(position, NNConstraint.Default).node;
    }

    private void OnDisable()
    {
        BlocksManager.Instance.BlockDestroyed -= CheckPath;
    }
}