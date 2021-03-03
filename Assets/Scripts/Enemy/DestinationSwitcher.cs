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

public class DestinationSwitcher : MonoBehaviour
{
    private GridGraph activeGrid;
    private MyAIPath aiPath;
    private Vector3 destinationPoint;

    private GraphNode EnemyNode
    {
        get { return AstarPath.active.GetNearest(this.gameObject.transform.position, NNConstraint.Default).node; }
    }

    private GraphNode DestinationPointNode { get; set; }

    private void OnEnable()
    {
        BlocksManager.Instance.BlockDestroyed += CheckPath;
        aiPath = GetComponent<MyAIPath>();
        aiPath.TargetReachedEvent += CheckPath;
    }

    private void Start()
    {
        activeGrid = AstarPath.active.data.gridGraph;

        destinationPoint = GetRandomDestinationPoint();
        DestinationPointNode = GetGraphNodeByPosition(destinationPoint);
        CheckPath();
    }

    private void SetEnemyState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.MovingToMainTarget:
                aiPath.destination = destinationPoint;
                break;

            case EnemyState.Wandering:
                var randomDestination = GetRandomNode().position;

                // Генерируем рандомную точку назначения до тех пор, пока не находим валидную (для текущего положения врага)
                while (!IsPathPossible(EnemyNode, GetGraphNodeByPosition((Vector3) randomDestination)))
                {
                    randomDestination = GetRandomNode().position;
                }

                aiPath.destination = (Vector3) randomDestination;
                break;
        }
    }

    private bool IsPathPossible(GraphNode node1, GraphNode node2)
    {
        return PathUtilities.IsPathPossible(node1, node2);
    }

    private void CheckPath()
    {
        if (DestinationPointNode == null)
        {
            Debug.LogError("Destination point is not valid!");
            return;
        }
        
        // Сначала проверям основной пункт назначения
        if (IsPathPossible(EnemyNode, DestinationPointNode))
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

    // Выбираем координаты чуть ниже игрового пространства. В этом пространстве должна быть активная сетка.
    // Иначе получение нода по этим координатам будет возвращать null
    private Vector3 GetRandomDestinationPoint()
    {
        Vector3 screenBounds = MiscTools.GetScreenBounds();

        float xCoord = Random.Range(-screenBounds.x, screenBounds.x);
        float yCoord = -screenBounds.y - 10;

        return new Vector3(xCoord, yCoord, 0);
    }

    private void OnDisable()
    {
        BlocksManager.Instance.BlockDestroyed -= CheckPath;
        aiPath.TargetReachedEvent -= CheckPath;
    }
}