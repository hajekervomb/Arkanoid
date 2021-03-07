using Pathfinding;
using UnityEngine;

public static class MyPathUtilities
{
    private static readonly GridGraph ActiveGrid = AstarPath.active.data.gridGraph;

    public static GridNode GetValidRandomNode()
    {
        GridNode randomNode;
      
        while (true)
        {
            randomNode = ActiveGrid.nodes[Random.Range(0, ActiveGrid.nodes.Length)];

            if (randomNode.Walkable)
                break;
        }

        Debug.LogError($"Is nod traversable: {randomNode.Walkable}");
        return randomNode;
    }
}
