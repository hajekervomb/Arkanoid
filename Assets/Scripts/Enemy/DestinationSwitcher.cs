using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyState
{
    MovingToDestinationPoint = 0,
    Wandering = 1,
}

public class DestinationSwitcher : MonoBehaviour
{
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
        destinationPoint = GetRandomDestinationPoint();
        DestinationPointNode = GetGraphNodeByPosition(destinationPoint);
        CheckPath();
    }

    private void SetEnemyState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.MovingToDestinationPoint:
                aiPath.destination = destinationPoint;
                break;

            case EnemyState.Wandering:
                var randomDestination = MyPathUtilities.GetValidRandomNode().position;

                // Генерируем рандомную точку назначения до тех пор, пока не находим валидную (для текущего положения врага)
                while (!PathUtilities.IsPathPossible(EnemyNode, GetGraphNodeByPosition((Vector3) randomDestination)))
                {
                    randomDestination = MyPathUtilities.GetValidRandomNode().position;
                }

                aiPath.destination = (Vector3) randomDestination;
                break;
        }
    }

    private void CheckPath()
    {
        if (DestinationPointNode == null)
        {
            Debug.LogError("Destination point is not valid!");
            return;
        }
        
        // Сначала проверям основной пункт назначения
        if (PathUtilities.IsPathPossible(EnemyNode, DestinationPointNode))
        {
            SetEnemyState(EnemyState.MovingToDestinationPoint);
        }
        else {
            
            SetEnemyState(EnemyState.Wandering);
        }
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

        float xCoord = Random.Range(-screenBounds.x / 2, screenBounds.x / 2);
        float yCoord = -screenBounds.y;

        return new Vector3(xCoord, yCoord, 0);
    }

    // TODO - выяснить, почему иногда враги считают точкой назначения рандомную точку в состоянии Wandering
    public bool IsEnemyReachedMainDestinationPoint()
    {
        return destinationPoint == aiPath.destination;
    }

    private void OnDisable()
    {
        BlocksManager.Instance.BlockDestroyed -= CheckPath;
        aiPath.TargetReachedEvent -= CheckPath;
    }
}